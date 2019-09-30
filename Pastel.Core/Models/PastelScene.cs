using System;
using System.Collections.Generic;
using Pastel.Core.Platform.Input;
using Veldrid;

namespace Pastel.Core.Models
{
    public class PastelScene
    {
        protected List<PastelObject> SceneObjects = new List<PastelObject>();
        protected RgbaFloat BackgroundColour = RgbaFloat.Black;

        public void Update(float deltaTime)
        {
            SceneUpdate();
            foreach (var gameObject in PastelGame.GameObjects) gameObject.Update(deltaTime);
            foreach (var sceneObject in SceneObjects) sceneObject.Update(deltaTime);
        }

        public void Draw()
        {
            PastelGame.CommandList.Begin();
            PastelGame.CommandList.SetFramebuffer(PastelGame.GraphicsDevice.SwapchainFramebuffer);
            PastelGame.CommandList.ClearColorTarget(0, BackgroundColour);

            foreach (var gameObject in PastelGame.GameObjects) gameObject.Draw();
            foreach (var sceneObject in SceneObjects) sceneObject.Draw();

            PastelGame.CommandList.End();
            PastelGame.GraphicsDevice.SubmitCommands(PastelGame.CommandList);
            PastelGame.GraphicsDevice.WaitForIdle();
            PastelGame.GraphicsDevice.SwapBuffers();
        }

        public virtual void SceneUpdate() { }
    }
}
