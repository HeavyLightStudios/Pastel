using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;
using Pastel.Core.Platform.Graphics;
using Pastel.Core.Platform.Input;
using Pastel.Core.Platform.Window;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Core.Models
{
    public class PastelGame
    {
        private static Lazy<PastelWindow> pastelWindow = new Lazy<PastelWindow>(() =>
        {
            return new PastelWindow();
        });
        private static Lazy<List<PastelObject>> pastelObjects = new Lazy<List<PastelObject>>(() => new List<PastelObject>());
        private static Lazy<GraphicsDevice> graphicsDevice = new Lazy<GraphicsDevice>(() =>
        {
            var pastelGD = new GraphicDevice();
            return pastelGD.Create(pastelWindow.Value);
        });
        private static Lazy<CommandList> commandList = new Lazy<CommandList>(() =>
        {
            var factory = GraphicsDevice.ResourceFactory;
            return factory.CreateCommandList();
        });

        public static List<PastelObject> PastelObjects => pastelObjects.Value;
        public static GraphicsDevice GraphicsDevice => graphicsDevice.Value;
        public static CommandList CommandList => commandList.Value;



        private InputManager _inputManager;


        public PastelGame()
        {
        }



        private void Draw()
        {
            CommandList.Begin();
            CommandList.SetFramebuffer(PastelGame.GraphicsDevice.SwapchainFramebuffer);
            CommandList.ClearColorTarget(0, RgbaFloat.Black);
            
            foreach (var pastelObject in PastelObjects)
            {
                pastelObject.Draw();
            }
                
            CommandList.End(); 
            GraphicsDevice.SubmitCommands(CommandList);
            GraphicsDevice.WaitForIdle();
            GraphicsDevice.SwapBuffers();
        }

        public void Run()
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
           
            _inputManager = new InputManager();

            Task.Run(() =>
            {
                var timer = Stopwatch.StartNew();
                var startTime = timer.ElapsedMilliseconds;
                float totalElapsedTime = 0;
                var msPerUpdate = TimeSpan.FromMilliseconds(10).Milliseconds;

                while (true)
                {
                    var currentTime = timer.ElapsedMilliseconds;
                    var elapsedTime = currentTime - startTime;
                    
                    startTime = currentTime;
                    totalElapsedTime += elapsedTime;

                    while (totalElapsedTime >= msPerUpdate)
                    {
                        Update(totalElapsedTime / msPerUpdate);
                        totalElapsedTime -= msPerUpdate;
                    }
                    
                    Draw();

                    token.ThrowIfCancellationRequested();
                }
            }, token);

        }

        private static void Update(float deltaTime)
        {
            foreach(var pastelObject in PastelObjects)
            {
                pastelObject.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach(var pastelObject in PastelObjects)
            {
                pastelObject.Dispose();
            }
            GraphicsDevice.Dispose();
        }
    }
}
