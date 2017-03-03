using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Content
{
    public static class TextureLoader
    {
        private static ContentManager _contentManager;
        private static object _lockObject = new object();

        //public static Dictionary<string, Texture2D> assetList = new Dictionary<string, Texture2D>();

        public static void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        //public static void Add(string key, Texture2D Value)
        //{
        //    lock (_lockObject)
        //    {
        //        if (!assetList.ContainsKey(key))
        //            assetList.Add(key, Value);
        //    }
        //}

        public static Texture2D Get(string key)
        {
            lock (_lockObject)
            {
                return _contentManager.Load<Texture2D>(key);
                //if (assetList.ContainsKey(key))
                //    return assetList[key];

                //return null;
            }
        }
    }
}
