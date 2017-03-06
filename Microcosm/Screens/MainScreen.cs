using Microsoft.Xna.Framework;
using Gametek.Monogame.Screen;
using System;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame.UI.Controls;
using Gametek.Monogame;

namespace Microcosm.Screens
{
    public sealed class MainScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        Button b;

        public MainScreen()
        {
            IsVisible = true;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            //var graphicsDeviceService = (IGraphicsDeviceService)_serviceProvider.GetService(typeof(IGraphicsDeviceService));
            _spriteBatch = new SpriteBatch(GameEngine.GraphicsDeviceManager.GraphicsDevice);

            b = new Button("Dit is een test", Color.Gray, 30, 30, 100, 50);
            System.Diagnostics.Debug.WriteLine("MainScreen::LoadContent()");
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            b.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }
    }
}
