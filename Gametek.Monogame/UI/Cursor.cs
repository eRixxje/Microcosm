using Gametek.Monogame.Content;
using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.UI
{
    public sealed class Cursor
    {
        private Texture2D CursorTexture;
        private Rectangle cursorSource;
        private Rectangle cursorDest;

        public void Initialize()
        {
            cursorSource = new Rectangle(0, 0, 26, 26);
            cursorDest = new Rectangle(0, 0, 32, 32);
        }

        public void LoadContent()
        {
            CursorTexture = TextureLoader.Get("Cursors\\cursors");
        }

        public void Update(GameTime gameTime)
        {
            // Update Cursor Location
            cursorDest.X = InputManager.MousePosition.X;
            cursorDest.Y = InputManager.MousePosition.Y;

            // Set Cursor Rect.
            if (InputManager.IsMouseDown(MouseButton.RightButton))
            {
                cursorSource.X = 52;
                cursorSource.Y = 0;
            }
            else
            {
                cursorSource.X = 0;
                cursorSource.Y = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CursorTexture, cursorDest, cursorSource, Color.White);
        }
    }
}
