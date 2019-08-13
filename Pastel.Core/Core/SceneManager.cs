using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pastel.Core
{
    public class SceneManager
    {
        // Instance of Scene Manager
        private static SceneManager _instance;
        private ContentManager _content;

        // Stack of scenes
        private Stack<Scene> _scenes = new Stack<Scene>();

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                }

                return _instance;
            }
        }

        // Sets the content manager
        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        // Add new Scenes to the stack
        public void AddScene(Scene scene)
        {
            try
            {
                // Add scene to the stack
                _scenes.Push(scene);
                // Initialize the scene
                _scenes.Peek().Initialize();
                if (_content != null)
                {
                    _scenes.Peek().LoadContent(_content);
                }
            }
            catch (Exception ex)
            {
                // Log out the exception
            }
        }

        // Removes the top scene from the stack
        public void RemoveScene()
        {
            if (_scenes.Count > 0)
            {
                try
                {
                    var screen = _scenes.Peek();
                    _scenes.Pop();
                }
                catch (Exception ex)
                {
                    // Log the exception
                }
            }
        }

        // Clears all the scene from the list
        public void ClearScenes()
        {
            while (_scenes.Count > 0)
            {
                _scenes.Pop();
            }
        }

        // Removes all screens from the stack and adds a new one 
        public void ChangeScene(Scene scene)
        {
            try
            {
                ClearScenes();
                AddScene(scene);
            }
            catch (Exception ex)
            {
                // Log the exception
            }
        }

        // Updates the top screen. 
        public void Update(GameTime gameTime)
        {
            try
            {
                if (_scenes.Count > 0)
                {
                    _scenes.Peek().Update(gameTime);
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Renders the top screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (_scenes.Count > 0)
                {
                    _scenes.Peek().Draw(spriteBatch);
                }
            }
            catch (Exception ex)
            {
                // Log out the exception
            }
        }

        // Unloads the content from the screen
        public void UnloadContent()
        {
            foreach (Scene scene in _scenes)
            {
                scene.UnloadContent();
            }
        }
    }
}