using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;

namespace Gametek.Monogame
{
    public class GameEngine : Game
    {
        public GameEngine(int Width, int Height, bool fullScreen)
        {
            ScreenManager.Initialize(this, fullScreen, Width, Height);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            //System.Diagnostics.Debug.WriteLine("GameEngine::LoadContent()");

            ScreenManager.LoadContent();
            FontManager.LoadContent(Content);
            InputManager.LoadContent(Content);
            ModelManager.LoadContent(Content);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine("GameEngine::Update()");

            InputManager.Update();
            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine("GameEngine::Draw()");

            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.spriteBatch.Begin();
            ScreenManager.Draw(gameTime);
            InputManager.Draw(gameTime);
            ScreenManager.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
