using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.Screens
{
    public sealed class MapScreen : RenderTarget
    {
        Galaxy galaxy;

        public MapScreen() : base(true)
        {
            galaxy = new Galaxy();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            galaxy.LoadContent();

            base.LoadContent();
        }
        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            galaxy.Update(gameTime);

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.DrawStringShadowed(AssetManager.ControlFont, string.Format("{0}, {1}", InputManager.MousePosition.X, InputManager.MousePosition.Y), new Vector2(10, 10), Color.White, Color.Black);
            galaxy.Draw(gameTime, spriteBatch);
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
