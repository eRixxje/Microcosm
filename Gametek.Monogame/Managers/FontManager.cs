using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Screen
{
    public static class FontManager
    {
        private static ContentManager contentmanager;

        public static Dictionary<string, SpriteFont> fontlist = new Dictionary<string, SpriteFont>();

        public static SpriteFont ControlFont
        {
            get { return fontlist["ControlFont"]; }
        }

        public static void Initialize(ContentManager ContentManager)
        {
            System.Diagnostics.Debug.WriteLine("FontManager::Initialize::Start");
            contentmanager = ContentManager;
            LoadContent();
            System.Diagnostics.Debug.WriteLine("FontManager::Initialize::Done");
        }

        public static void LoadContent()
        {
            System.Diagnostics.Debug.WriteLine("FontManager::LoadContent::Start");
            // Fallback font
            fontlist.Add("ControlFont", contentmanager.Load<SpriteFont>("Fonts\\ControlFont"));
            System.Diagnostics.Debug.WriteLine("FontManager::LoadContent::Done");
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
