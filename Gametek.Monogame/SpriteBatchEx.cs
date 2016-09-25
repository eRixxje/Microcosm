using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame
{
    public class SpriteBatchEx : SpriteBatch
    {
        public SpriteBatchEx(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }

        public void DrawStringShadowed(SpriteFont spriteFont, string text, Vector2 position, Color color, Color shadow)
        {
            Vector2 offset = new Vector2(1, 1);

            DrawString(spriteFont, text, Vector2.Add(position, offset), shadow);
            DrawString(spriteFont, text, position, color);
        }
    }
}
