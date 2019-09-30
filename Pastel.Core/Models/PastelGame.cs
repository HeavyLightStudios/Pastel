using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Pastel.Core.Platform.Graphics;
using Pastel.Core.Platform.Input;
using Pastel.Core.Platform.Window;
using Veldrid;

namespace Pastel.Core.Models
{
    public abstract class PastelGame
    {
        private static readonly Lazy<PastelWindow> pastelWindow = new Lazy<PastelWindow>(() =>
        {
            return new PastelWindow();
        });

        private static Lazy<List<PastelObject>> gameObjects =
            new Lazy<List<PastelObject>>(() => new List<PastelObject>());

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

        private Stack<PastelScene> _sceneManager = new Stack<PastelScene>();

        protected InputManager _inputManager = new InputManager();

        public static PastelWindow PastelWindow => pastelWindow.Value;
        public static List<PastelObject> GameObjects => gameObjects.Value;
        public static GraphicsDevice GraphicsDevice => graphicsDevice.Value;
        public static CommandList CommandList => commandList.Value;


        private void Draw()
        {
            _sceneManager.Peek().Draw();
        }

        public virtual void Run()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

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

        public virtual void Update(float deltaTime)
        {
            _sceneManager.Peek().Update(deltaTime);
        }

        public void AddScene(PastelScene scene)
        {
            _sceneManager.Push(scene);
        }

        public void RemoveScene()
        {
            _sceneManager.Pop();
        }

        public void ReplaceScene(PastelScene scene)
        {
            while(_sceneManager.Count > 0)
            {
                _sceneManager.Pop();
            }

            _sceneManager.Push(scene);
        }

        public virtual void Dispose()
        {
            foreach (var pastelObject in GameObjects) pastelObject.Dispose();
            GraphicsDevice.Dispose();
        }
    }
}