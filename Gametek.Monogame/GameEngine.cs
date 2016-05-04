using Gametek.Monogame.Input;
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
            // Init Managers' Content
            ScreenManager.LoadContent();
            FontManager.LoadContent(Content);
            InputManager.LoadContent(Content);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            ScreenManager.spriteBatch.Begin();
            ScreenManager.Draw(gameTime);
            InputManager.Draw(gameTime);
            ScreenManager.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
