using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Manager
{
    public static class AssetManager
    {
        private static ContentManager _contentManager;

        private static Dictionary<string, object> assetList = new Dictionary<string, object>();
        public static SpriteFont ControlFont
        {
            get { return (SpriteFont) assetList["ControlFont"]; }
        }

        public static void Initialize(ContentManager Content)
        {
            _contentManager = Content;

            assetList.Add("ControlFont", GetFont("fonts\\microcosm"));
        }

        public static Texture2D GetTexture(string key)
        {
            return _contentManager.Load<Texture2D>(key);
        }

        public static SpriteFont GetFont(string key)
        {
            return _contentManager.Load<SpriteFont>(key);
        }
    }
}
