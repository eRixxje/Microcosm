﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Gametek.Monogame.Managers;
using Gametek.Monogame;

namespace Microcosm
{
    public class Microcosm : GameEngine
    {
        //private Model model;

        public Microcosm() : base(1440, 960, false)
        {           
            Window.Title = "Microcosm Prototyping";
            Window.Position = new Point(100, 100);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // Add Screens
            //ScreenManager.Add(new Screens.MainScreen(true));
            ScreenManager.Add(new Screens.MapScreen(true));
            //ScreenManager.Add(new Screens.CubeScreen(true));
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.IsKeyDown(Keys.Escape))
                Exit();
            
            if (InputManager.IsKeyPress(Keys.F11))
            {
                ScreenManager.ToggleFullScreen();
            }
        }
    }
}
