using System;
using System.IO;
using System.Numerics;
using Veldrid;
using Veldrid.ImageSharp;
using Veldrid.SPIRV;

namespace Pastel.Core.Models
{
    public class Sprite: PastelObject
    {

        private DeviceBuffer _projectionBuffer;
        private DeviceBuffer _viewBuffer;
        private DeviceBuffer _worldBuffer;
        private Texture _surfaceTexture;
        private TextureView _surfaceTextureView;
        private Pipeline _pipeline;
        private ResourceSet _projViewSet;
        private ResourceSet _worldTextureSet;

        public Sprite()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            _projectionBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _viewBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            _worldBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            var texture = new ImageSharpTexture("character.png");
            _surfaceTexture = texture.CreateDeviceTexture(PastelGame.GraphicsDevice, PastelGame.GraphicsDevice.ResourceFactory);
            _surfaceTextureView = PastelGame.GraphicsDevice.ResourceFactory.CreateTextureView(_surfaceTexture);

            var shaderSet = new ShaderSetDescription(
                new[]
                {
                    new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                    new VertexElementDescription("TexCoords", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2))
                },
                factory.CreateFromSpirv(
                    new ShaderDescription(ShaderStages.Vertex, ReadEmbeddedAssetBytes("Pastel.Core.Shaders.Sprite-vertex.glsl"), "main"),
                    new ShaderDescription(ShaderStages.Fragment, ReadEmbeddedAssetBytes("Pastel.Core.Shaders.Sprite-fragment.glsl"), "main")));

            var projViewLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("ProjectionBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                    new ResourceLayoutElementDescription("ViewBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)));

            var worldTextureLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("WorldBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                    new ResourceLayoutElementDescription("SurfaceTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
                    new ResourceLayoutElementDescription("SurfaceSampler", ResourceKind.Sampler, ShaderStages.Fragment)));

            _pipeline = factory.CreateGraphicsPipeline(new GraphicsPipelineDescription(
                BlendStateDescription.SingleOverrideBlend,
                DepthStencilStateDescription.DepthOnlyLessEqual,
                RasterizerStateDescription.Default,
                PrimitiveTopology.TriangleList,
                shaderSet,
                new[] { projViewLayout, worldTextureLayout },
                PastelGame.GraphicsDevice.MainSwapchain.Framebuffer.OutputDescription));

            _projViewSet = factory.CreateResourceSet(new ResourceSetDescription(
                projViewLayout,
                _projectionBuffer,
                _viewBuffer));

            _worldTextureSet = factory.CreateResourceSet(new ResourceSetDescription(
                worldTextureLayout,
                _worldBuffer,
                _surfaceTextureView,
                PastelGame.GraphicsDevice.Aniso4xSampler));
        }

        public override void Draw()
        {
            PastelGame.GraphicsDevice.UpdateBuffer(_projectionBuffer, 0, Matrix4x4.CreatePerspectiveFieldOfView(
                1.0f,
                (float)PastelGame.PastelWindow.ScreenSize.Width / PastelGame.PastelWindow.ScreenSize.Height,
                0.5f,
                100f));

            PastelGame.GraphicsDevice.UpdateBuffer(_viewBuffer, 0, Matrix4x4.CreateLookAt(Vector3.UnitZ * 2.5f, Vector3.Zero, Vector3.UnitY));


            PastelGame.CommandList.SetFramebuffer(PastelGame.GraphicsDevice.MainSwapchain.Framebuffer);
            PastelGame.CommandList.ClearColorTarget(0, RgbaFloat.Black);
            PastelGame.CommandList.ClearDepthStencil(1f);
            PastelGame.CommandList.SetPipeline(_pipeline);
            PastelGame.CommandList.SetGraphicsResourceSet(0, _projViewSet);
            PastelGame.CommandList.SetGraphicsResourceSet(1, _worldTextureSet);
            PastelGame.CommandList.DrawIndexed(36, 1, 0, 0, 0);

        }

        public static Stream OpenEmbeddedAssetStream(string name, Type t) => t.Assembly.GetManifestResourceStream(name);

        public byte[] ReadEmbeddedAssetBytes(string name)
        {
            var resourcces = this.GetType().Assembly.GetManifestResourceNames();
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(name))
            {
                byte[] bytes = new byte[stream.Length];
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    stream.CopyTo(ms);
                    return bytes;
                }
            }
        }
    }
}
