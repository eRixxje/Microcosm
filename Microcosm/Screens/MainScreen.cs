using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.Screens
{
    public class MainScreen : RenderTarget
    {
        private Texture2D _background;

        public MainScreen(string name, bool isEnabled) : base(name, isEnabled)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            _background= AssetManager.GetTexture("textures//splash");
            base.LoadContent();
        }
        public override void UnloadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void HandleInput()
        {
            if (InputManager.IsKeyPress(Microsoft.Xna.Framework.Input.Keys.S))
                StartGame();
        }
        public override void SetupControls()
        {
            
        }
        public override void UpdateControls()
        {
            
        }

        private void StartGame()
        {
            // Create a Galaxy and wait for it to be done.
            Galaxy.Initialize();

            // Switch to Galaxy Screen
            RenderManager.Switch(Name, "GalaxyScreen");
        }
    }
}
