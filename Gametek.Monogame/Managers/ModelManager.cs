using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gametek.Monogame.Managers
{
    public static class ModelManager
    {
        private static ContentManager contentmanager;

        public static Dictionary<string, Model> modellist = new Dictionary<string, Model>();
                
        public static void LoadContent(ContentManager ContentManager)
        {
            contentmanager = ContentManager;

            // Fallback font
            //fontlist.Add("ControlFont", ContentManager.Load<SpriteFont>("Fonts\\ControlFont"));
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
