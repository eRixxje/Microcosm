using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microcosm.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Microcosm : GameBase
    {
        //MainScreen _main;
        MapScreen _map;

        Cursor cursor;

        public Microcosm() : base(1280, 720, false)
        {
            cursor = new Cursor();
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
            // TODO: use this.Content to load your game content here
            cursor.LoadContent();

            //_main = new MainScreen();
            _map = new MapScreen();

            RenderManager.Add(_map);
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
            
            if (InputManager.IsKeyDown(Keys.Escape))
                Exit();

            if (InputManager.IsKeyPress(Keys.F11))
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
