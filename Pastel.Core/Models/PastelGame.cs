using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;
using Pastel.Core.Platform.Graphics;
using Pastel.Core.Platform.Window;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Core.Models
{
    public class PastelGame
    {
        private PastelWindow _window;
        private static GraphicsDevice _graphicsDevice;
        private static CommandList _commandList;
        private static DeviceBuffer _vertexBuffer;
        private static DeviceBuffer _indexBuffer;
        private static Shader[] _shaders;
        private static Pipeline _pipeline;
        private static VertexPositionColor[] quadVertices;
        private static ushort[] quadIndices;
        
        private const string VertexCode = @"
#version 450

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

layout(location = 0) out vec4 fsin_Color;

void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";

        private const string FragmentCode = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";

        public PastelGame()
        {
            _window = new PastelWindow();

            var pastelGD = new GraphicDevice();
            _graphicsDevice = pastelGD.Create(_window);
        }
        
        private void CreateResources()
        {
            var factory = _graphicsDevice.ResourceFactory;
            
            _vertexBuffer = factory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
            _indexBuffer = factory.CreateBuffer(new BufferDescription(4 * sizeof(ushort), BufferUsage.IndexBuffer));

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
            
            pipelineDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();
            
            pipelineDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: _shaders);
            
            pipelineDescription.Outputs = _graphicsDevice.SwapchainFramebuffer.OutputDescription;
            _pipeline = factory.CreateGraphicsPipeline(pipelineDescription);
            
            _commandList = factory.CreateCommandList();
        }

        private void Draw(RgbaFloat colour1, RgbaFloat colour2, RgbaFloat colour3, RgbaFloat colour4)
        {
            quadVertices = new []
            {
                new VertexPositionColor(new Vector2(-.75f, .75f), colour1),
                new VertexPositionColor(new Vector2(.75f, .75f), colour2),
                new VertexPositionColor(new Vector2(-.75f, -.75f), colour3),
                new VertexPositionColor(new Vector2(.75f, -.75f), colour4)
            };
            
            quadIndices = new ushort[] { 0, 1, 2, 3 };
            
            _graphicsDevice.UpdateBuffer(_vertexBuffer, 0, quadVertices);
            _graphicsDevice.UpdateBuffer(_indexBuffer, 0, quadIndices);
            
            _commandList.Begin();
            _commandList.SetFramebuffer(_graphicsDevice.SwapchainFramebuffer);
            _commandList.ClearColorTarget(0, RgbaFloat.Black);
            _commandList.SetVertexBuffer(0, _vertexBuffer);
            _commandList.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
            _commandList.SetPipeline(_pipeline);
            _commandList.DrawIndexed(
                indexCount: 4,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);
            _commandList.End();
            _graphicsDevice.SubmitCommands(_commandList);
            _graphicsDevice.SwapBuffers();
        }
        
        public void Run()
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            
            var red = 0;
            var green = 0;
            var blue = 0;
            
            CreateResources();

            var task = Task.Run(async () =>
            {
                var timer = Stopwatch.StartNew();
                var startTime = timer.ElapsedMilliseconds;
                long lag = 0;
                
                while (true)
                {
                    var currentTime = timer.ElapsedMilliseconds;
                    var elapsedTime = currentTime - startTime;
                    startTime = currentTime;
                    lag += elapsedTime;
                    
                    // add input

                    while (lag >= TimeSpan.FromMilliseconds(16).Milliseconds)
                    {
                        Update(ref red, ref green, ref blue);
                        lag -= TimeSpan.FromMilliseconds(16).Milliseconds;
                    }
                    
                    Draw(new RgbaFloat(red / 64f, green / 64f, blue / 64f, 1f), 
                        new RgbaFloat(red / 64f, blue / 64f, green / 64f, 1f), 
                        new RgbaFloat(green / 64f, blue / 64f, red / 64f, 1f), 
                        new RgbaFloat(blue / 64f, red / 64f, green / 64f, 1f));
                    
                    token.ThrowIfCancellationRequested();
                }
            }, token);
            
        }

        private static void Update(ref int red, ref int green, ref int blue)
        {
            if (red == 64)
            {
                if (green == 64)
                {
                    if (blue == 64)
                    {
                        red = 0;
                        green = 0;
                        blue = 0;
                    }
                    else
                    {
                        blue += 1;
                    }
                }
                else
                {
                    green += 1;
                }
            }
            else
            {
                red += 1;
            }
        }

        public void Dispose()
        {
            _pipeline.Dispose();
            _commandList.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _graphicsDevice.Dispose();
        }
    }
    struct VertexPositionColor
    {
        public Vector2 Position; // This is the position, in normalized device coordinates.
        public RgbaFloat Color; // This is the color of the vertex.
        public VertexPositionColor(Vector2 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }
        public const uint SizeInBytes = 24;
    }
}