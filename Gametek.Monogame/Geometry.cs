using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame
{
    public class Geometry
    {
        public static Texture2D Pixel(Color Color)
        {
            Texture2D pixel = new Texture2D(RenderManager.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color });

            return pixel;
        }
        public static Texture2D Rectangle(Vector2 size, Color BackgroundColor)
        {
            Texture2D rect = new Texture2D(RenderManager.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[(int)size.X * (int)size.Y];

            for (int i = 0; i < data.Length; ++i) data[i] = BackgroundColor;
            rect.SetData(data);

            return rect;
        }

        public static Texture2D Rectangle(Rectangle size, Color BackgroundColor)
        {
            Texture2D rect = new Texture2D(RenderManager.GraphicsDevice, size.Width, size.Height);
            Color[] data = new Color[size.Width * size.Height];

            for (int i = 0; i < data.Length; ++i) data[i] = BackgroundColor;
            rect.SetData(data);

            return rect;
        }

        public static Texture2D Circle(int radius, Color color, float sharpness)
        {
            int diameter = radius * 2;
            Texture2D circleTexture = new Texture2D(RenderManager.GraphicsDevice, diameter, diameter, false, SurfaceFormat.Color);
            Color[] colorData = new Color[circleTexture.Width * circleTexture.Height];
            Vector2 center = new Vector2(radius);

            for (int colIndex = 0; colIndex < circleTexture.Width; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < circleTexture.Height; rowIndex++)
                {
                    Vector2 position = new Vector2(colIndex, rowIndex);
                    float distance = Vector2.Distance(center, position);

                    // hermite iterpolation
                    float x = distance / diameter;
                    float edge0 = (radius * sharpness) / (float)diameter;
                    float edge1 = radius / (float)diameter;
                    float temp = MathHelper.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
                    float result = temp * temp * (3.0f - 2.0f * temp);

                    colorData[rowIndex * circleTexture.Width + colIndex] = color * (1f - result);
                }
            }
            circleTexture.SetData<Color>(colorData);

            return circleTexture;
        }

        public static Texture2D Border(Vector2 size, int borderWidth, Color borderColor)
        {
            Texture2D rect = new Texture2D(RenderManager.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[rect.Width * rect.Height];

            for (int x = 0; x < rect.Width; x++)
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int i = 0; i < borderWidth; i++)
                    {
                        if (x == i || y == i || x == rect.Width - 1 - i || y == rect.Height - 1 - i)
                        {
                            data[x + y * rect.Width] = borderColor;
                            break;
                        }
                    }
                }
            }

            rect.SetData(data);

            return rect;
        }

    }
}
