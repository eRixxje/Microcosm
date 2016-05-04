using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Managers
{
    public static class FontManager
    {
        public static Dictionary<string, SpriteFont> fontlist = new Dictionary<string, SpriteFont>();

        public static SpriteFont ControlFont
        {
            get { return fontlist["ControlFont"]; }
        }

        public static void LoadContent(ContentManager ContentManager)
        {
            // Fallback font
            fontlist.Add("ControlFont", ContentManager.Load<SpriteFont>("Fonts\\ControlFont"));
        }

        public static void Add(string key, SpriteFont Value)
        {
            if(!fontlist.ContainsKey(key))
                fontlist.Add(key, Value);
        }

        public static SpriteFont Get(string key)
        {
            if (fontlist.ContainsKey(key))
                return fontlist[key];

            return fontlist["ControlFont"];
        }
    }
}
