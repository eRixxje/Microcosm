using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.UI
{
    public class Cursor
    {
        private SpriteBatch spriteBatch;

        public static readonly Rectangle POINTER = new Rectangle(0, 0, 26, 26);
        public static readonly Rectangle HAND    = new Rectangle(26, 0, 26, 26);
        public static readonly Rectangle NESW    = new Rectangle(52, 0, 26, 26);
        public static readonly Rectangle ZOOMIN  = new Rectangle(78, 0, 26, 26);
        public static readonly Rectangle ZOOMOUT = new Rectangle(104, 0, 26, 26);

        private Texture2D cursorTexture;
        private Rectangle cursorSource = POINTER;
        private Rectangle cursorDest = new Rectangle(0, 0, 16, 16);

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(RenderManager.GraphicsDevice);
            cursorTexture = AssetManager.GetTexture("cursors\\cursors");
        }

        public void SetCursor(Rectangle Pointer)
        {
            cursorSource = Pointer;
        }

        public void Update(GameTime gameTime)
        {
            cursorDest.X = InputManager.MousePosition.X;
            cursorDest.Y = InputManager.MousePosition.Y;

            // Set Cursor Rect.
            //if (InputManager.IsMouseDown(MouseButton.RightButton))
            //    cursorSource = NESW;
            //else
            //    cursorSource = POINTER;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cursorTexture, cursorDest, cursorSource, Color.White);
            spriteBatch.End();
        }
    }

}
