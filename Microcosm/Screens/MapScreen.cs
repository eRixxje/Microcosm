using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;

namespace Microcosm.Screens
{
    public sealed class MapScreen : RenderTarget
    {
        GalaxyGrid grid;
        //Galaxy galaxy;
        
        public MapScreen() : base(true)
        {
            //galaxy = new Galaxy();

            grid = new GalaxyGrid(new Vector2(50,50), new Vector2(1000, 600), 50);
            //grid.Galaxy = galaxy;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            grid.LoadContent();

            //galaxy.LoadContent();

            base.LoadContent();
        }
        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            grid.Update(gameTime);

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            grid.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawString(AssetManager.ControlFont, string.Format("Mouse: {0}, {1}", InputManager.MousePosition.X, InputManager.MousePosition.Y), new Vector2(10, 10), Color.White, Color.Black);
            spriteBatch.DrawString(AssetManager.ControlFont, string.Format("{0}", grid.Camera.Position), new Vector2(10, 20), Color.White, Color.Black);            

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
