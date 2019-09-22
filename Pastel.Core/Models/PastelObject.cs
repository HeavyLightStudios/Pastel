using System;
using System.Text;
using System.Numerics;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Core.Models
{
    public abstract class PastelObject
    {
        public Vector2 Position;

        protected Pipeline Pipeline;
        protected GraphicsDevice graphicsDevice;
        protected Shader[] _shaders;
        protected const string VertexCode = @"
#version 450

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

layout(location = 0) out vec4 fsin_Color;

void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";
        protected const string FragmentCode = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";

        public PastelObject()
        {
            PastelGame.PastelObjects.Add(this);
            graphicsDevice = PastelGame.GraphicsDevice;
            Initialize();
        }

        private void Initialize()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4));

            ShaderDescription vertexShaderDesc = new ShaderDescription(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(VertexCode),
                "main");
            ShaderDescription fragmentShaderDesc = new ShaderDescription(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(FragmentCode),
                "main");
            _shaders = factory.CreateFromSpirv(vertexShaderDesc, fragmentShaderDesc);

            GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;

            pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual);

            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                cullMode: FaceCullMode.Back,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: false);

            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            pipelineDescription.ResourceLayouts = Array.Empty<ResourceLayout>();

            pipelineDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: _shaders);

            pipelineDescription.Outputs = PastelGame.GraphicsDevice.SwapchainFramebuffer.OutputDescription;
            Pipeline = factory.CreateGraphicsPipeline(pipelineDescription);

        }

        public virtual void Update(float deltaTime) { }

        public virtual void Draw() { }

        public virtual void Dispose() { }
    }
}
