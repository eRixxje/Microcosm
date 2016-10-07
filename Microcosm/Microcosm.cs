﻿using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microcosm.Screens;
using Microcosm.UI;
using Microcosm.Universe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    public class Microcosm : GameBase
    {
        private Cursor cursor;

        public static Galaxy Galaxy { get; private set; }

        public Microcosm() : base(1920, 1280, false)
        {
            cursor = new Cursor();
            Galaxy = new Galaxy();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Theme.LoadContent();
            
            cursor.LoadContent();

            RenderManager.Add(new MainScreen("MainScreen", true));
            RenderManager.Add(new MapScreen("GalaxyScreen", false));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Galaxy.Update(gameTime);

            if (Microcosm.Keyboard.IsKeyDown(Keys.Escape))
                Exit();

            if (Microcosm.Keyboard.IsKeyPress(Keys.F11))
            {
                System.Diagnostics.Debug.WriteLine("F11 Press");
                RenderManager.ToggleFullScreen();
            }

            // TODO: Add your update logic here
            cursor.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Theme.BACKGROUND);

            // TODO: Add your drawing code here
            base.Draw(gameTime);

            cursor.Draw(gameTime);
        }
    }
}
