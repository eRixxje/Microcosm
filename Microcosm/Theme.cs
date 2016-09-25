using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
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
                
        // Textures
        public static Texture2D GRID_LINE;
        public static Texture2D GRID_LINE1;

        private static Texture2D ASTEROID_SMALL_NORMAL;
        private static Texture2D ASTEROID_SMALL_SELECTED;
        private static Texture2D ASTEROID_MED_NORMAL;
        private static Texture2D ASTEROID_MED_SELECTED;
        private static Texture2D ASTEROID_BIG_NORMAL;
        private static Texture2D ASTEROID_BIG_SELECTED;
        private static Texture2D ASTEROID_UNKNOWN;

        public static void LoadContent()
        {
            BEIGE_LIGHT  = new Color(245, 219, 168);
            BEIGE_MEDIUM = new Color(90, 91, 77);
            BEIGE_DARK   = new Color(46, 56, 51);
            BLUE_LIGHT   = new Color(100, 203, 228);
            BLUE_MEDIUM  = new Color(21, 76, 81);
            BLUE_DARK    = new Color(20, 56, 60);
            BACKGROUND   = new Color(20, 36, 37);

            GRID_LINE   = Geometry.Pixel(BEIGE_MEDIUM);
            GRID_LINE1  = Geometry.Pixel(BEIGE_DARK);

            ASTEROID_SMALL_NORMAL   = Geometry.Circle(6, BLUE_LIGHT, .9f);
            ASTEROID_SMALL_SELECTED = Geometry.Circle(6, BLUE_MEDIUM, .9f);

            ASTEROID_MED_NORMAL   = Geometry.Circle(8, BLUE_LIGHT, .9f);
            ASTEROID_MED_SELECTED = Geometry.Circle(8, BLUE_MEDIUM, .9f);

            ASTEROID_BIG_NORMAL   = Geometry.Circle(10, BLUE_LIGHT, .9f);
            ASTEROID_BIG_SELECTED = Geometry.Circle(10, BLUE_MEDIUM, .9f);

            ASTEROID_UNKNOWN = Geometry.Circle(6, Color.Magenta, 1f);
        }

        public static Texture2D GetAsteroidTexture(int Size, bool Selected)
        {
            switch(Size)
            {
                case 0:
                    return (Selected) ? ASTEROID_SMALL_SELECTED : ASTEROID_SMALL_NORMAL;
                case 1:
                    return (Selected) ? ASTEROID_MED_SELECTED: ASTEROID_MED_NORMAL;
                case 2:
                    return (Selected) ? ASTEROID_BIG_SELECTED : ASTEROID_BIG_NORMAL;
                default:
                    return ASTEROID_UNKNOWN;                
            }
        }
    }
}
