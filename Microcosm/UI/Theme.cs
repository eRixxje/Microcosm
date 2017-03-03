using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.UI
{
    public class Theme
    {
        // Colors
        public static Color BEIGE_LIGHT;
        public static Color BEIGE_MEDIUM;
        public static Color BEIGE_DARK;
        public static Color BLUE_LIGHT;
        public static Color BLUE_MEDIUM;
        public static Color BLUE_DARK;
        public static Color BACKGROUND;
                
        // Grid Textures
        public static Texture2D GRID_LINE;
        public static Texture2D GRID_LINE1;

        // Graph Textures
        public static Texture2D BAR_EMPTY;
        public static Texture2D BAR_FULL;

        // Asteroid Type Textures
        public static Texture2D ASTEROID_S;
        public static Texture2D ASTEROID_M;
        public static Texture2D ASTEROID_L;

        public static void LoadContent()
        {
            BEIGE_LIGHT  = new Color(245, 219, 168);
            BEIGE_MEDIUM = new Color(90, 91, 77);
            BEIGE_DARK   = new Color(46, 56, 51);
            BLUE_LIGHT   = new Color(100, 203, 228);
            BLUE_MEDIUM  = new Color(21, 76, 81);
            BLUE_DARK    = new Color(20, 56, 60);
            BACKGROUND   = new Color(20, 36, 37);

            GRID_LINE   = Geometry.Pixel(RenderManager.GraphicsDevice, BEIGE_MEDIUM);
            GRID_LINE1  = Geometry.Pixel(RenderManager.GraphicsDevice, BEIGE_DARK);

            BAR_EMPTY = Geometry.Border(RenderManager.GraphicsDevice, new Vector2(14, 4), 1, BEIGE_MEDIUM);
            BAR_FULL = Geometry.Rectangle(RenderManager.GraphicsDevice, new Vector2(14, 4), BEIGE_LIGHT);

            ASTEROID_S = AssetManager.GetTexture("textures\\ast_size_1");   // 22x22
            ASTEROID_M = AssetManager.GetTexture("textures\\ast_size_2");   // 18x18
            ASTEROID_L = AssetManager.GetTexture("textures\\ast_size_3");   // 14x14
        }
    }
}
