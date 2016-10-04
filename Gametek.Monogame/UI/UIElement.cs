using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.UI
{
    /// <summary>
    /// Base Class for all UI Elements.
    /// </summary>
    public abstract class UIElement
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Bounds;

        public bool IsSelected { get; set; }


        public string PositionString
        {
            get
            {
                return string.Format("X: {0:0.0}, Y: {1:0.0}", Position.X, Position.Y);
            }
        }

        public void SetSelected(bool Value)
        {
            IsSelected = Value;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
