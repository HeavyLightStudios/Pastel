using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pastel.Core
{
    interface IScene
    {
        // Initialize the game settings here      
        void Initialize();
      
        // Load all content here
        void LoadContent(ContentManager content);
       
        // Unload any content here
        void UnloadContent();

        // Updates the game
        void Update(GameTime gameTime);

        // Draws the game
        void Draw(SpriteBatch spriteBatch);
    }
}