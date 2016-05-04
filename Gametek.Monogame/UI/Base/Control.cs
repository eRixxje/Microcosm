using Microsoft.Xna.Framework;

namespace Gametek.Monogame.UI.Base
{
    public abstract class Control
    {
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }

        protected Vector2 Location { get; private set; }
        protected Vector2 Size { get; private set; }

        protected Control(int x, int y, int w, int h)
        {
            Location = new Vector2(x, y);
            Size     = new Vector2(w, h);
        }

        //public abstract void LoadContent(GraphicsDevice graphics);
        public abstract void Draw(GameTime gameTime);
    }
}
