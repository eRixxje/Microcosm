using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.UI.Helper
{
    public static class GeometryHelper
    {
        public static Texture2D GetRectangle(Vector2 size, Color BackgroundColor)
        {
            Texture2D rect = new Texture2D(ScreenManager.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[(int)size.X * (int)size.Y];

            for (int i = 0; i < data.Length; ++i) data[i] = BackgroundColor;
            rect.SetData(data);

            return rect;
        }
    }
}
