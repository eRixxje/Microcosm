using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.Screens
{
    public class MainScreen : RenderTarget
    {
        private Texture2D _background;

        public MainScreen() : base(true)
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
            
        }
        public override void SetupControls()
        {
            
        }
        public override void UpdateControls()
        {
            
        }
    }
}
