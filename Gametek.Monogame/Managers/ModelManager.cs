using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Managers
{
    public static class ModelManager
    {
        private static ContentManager contentmanager;

        public static Dictionary<string, Model> modellist = new Dictionary<string, Model>();
                
        public static void Initialize(ContentManager ContentManager)
        {
            contentmanager = ContentManager;
        }

        public static void Add(string key)
        {
            if (!modellist.ContainsKey(key))
            {
                Model model = contentmanager.Load<Model>(key);
                modellist.Add(key, model);
            }
        }

        public static Model Get(string key)
        {
            if (modellist.ContainsKey(key))
                return modellist[key];

            return null;
        }
    }
}
