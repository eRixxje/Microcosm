using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.Screens
{
    public sealed class MapScreen : RenderTarget
    {
        //Galaxy galaxy;

        GalaxyGrid grid;

        

        public MapScreen() : base(true)
        {
            //galaxy = new Galaxy();
            grid = new GalaxyGrid(new Point(50,50), new Point(1000, 600), 50);
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
            //galaxy.Update(gameTime);

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //galaxy.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawStringShadowed(AssetManager.ControlFont, string.Format("Mouse: {0}, {1}", InputManager.MousePosition.X, InputManager.MousePosition.Y), new Vector2(10, 10), Color.White, Color.Black);
            //spriteBatch.DrawStringShadowed(AssetManager.ControlFont, string.Format("{0} : {1}", galaxy.Camera.ViewPort, galaxy.render.Count), new Vector2(10, 20), Color.White, Color.Black);

            grid.Draw(gameTime, spriteBatch);

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
