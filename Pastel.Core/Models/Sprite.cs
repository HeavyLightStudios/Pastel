using System;
using System.IO;
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.ImageSharp;
using Veldrid.SPIRV;

namespace Pastel.Core.Models
{
    public class Sprite: PastelObject
    {
        
        private Shader[]? _shaders;
        protected readonly GraphicsDevice GraphicsDevice;

        protected Pipeline? Pipeline;
        protected Vector2 Position;

        protected Sprite()
        {
            GraphicsDevice = PastelGame.GraphicsDevice;
            Initialize();
        }

        private void Initialize()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            var vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate,
                    VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate,
                    VertexElementFormat.Float4));

            var vertexShaderDesc = new ShaderDescription(
                ShaderStages.Vertex,
                ReadEmbeddedAssetBytes("Pastel.Core.Shaders.Sprite-vertex.glsl"),
                "main");
            var fragmentShaderDesc = new ShaderDescription(
                ShaderStages.Fragment,
                ReadEmbeddedAssetBytes("Pastel.Core.Shaders.Sprite-fragment.glsl"),
                "main");
            _shaders = factory.CreateFromSpirv(vertexShaderDesc, fragmentShaderDesc);

            var pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;

            pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
                true,
                true,
                ComparisonKind.LessEqual);

            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                FaceCullMode.Back,
                PolygonFillMode.Solid,
                FrontFace.Clockwise,
                true,
                false);

            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            pipelineDescription.ResourceLayouts = Array.Empty<ResourceLayout>();

            pipelineDescription.ShaderSet = new ShaderSetDescription(
                new[] { vertexLayout },
                _shaders);

            pipelineDescription.Outputs = PastelGame.GraphicsDevice.SwapchainFramebuffer.OutputDescription;
            Pipeline = factory.CreateGraphicsPipeline(pipelineDescription);

        }

        public static Stream OpenEmbeddedAssetStream(string name, Type t) => t.Assembly.GetManifestResourceStream(name);

        public byte[] ReadEmbeddedAssetBytes(string name)
        {
            using (Stream stream = OpenEmbeddedAssetStream(name, typeof(Sprite)))
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
