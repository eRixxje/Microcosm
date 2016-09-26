using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm.Screens
{
    public sealed class MapScreen : RenderTarget
    {
        Galaxy galaxy;

        private Texture2D hblock;
        private Texture2D vblock;

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
            vblock = Geometry.Rectangle(new Vector2(3, 20), Theme.BEIGE_LIGHT);
            hblock = Geometry.Rectangle(new Vector2(20, 3), Theme.BEIGE_LIGHT);

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
            galaxy.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawStringShadowed(AssetManager.ControlFont, string.Format("Mouse: {0}, {1}", InputManager.MousePosition.X, InputManager.MousePosition.Y), new Vector2(10, 10), Color.White, Color.Black);
            spriteBatch.DrawStringShadowed(AssetManager.ControlFont, string.Format("{0} : {1}", galaxy.Camera.ViewPort, galaxy.render.Count), new Vector2(10, 20), Color.White, Color.Black);

            // Map Borders
            spriteBatch.Draw(Theme.GRID_LINE, new Rectangle(50, 50, 1, 613), Color.White);
            spriteBatch.Draw(Theme.GRID_LINE, new Rectangle(50, 50, 1021, 1), Color.White);
            spriteBatch.Draw(Theme.GRID_LINE, new Rectangle(1071, 50, 1, 613), Color.White);
            spriteBatch.Draw(Theme.GRID_LINE, new Rectangle(50, 663, 1022, 1), Color.White);

            // Map Anchors
            spriteBatch.Draw(vblock, new Vector2(49, 49));
            spriteBatch.Draw(hblock, new Vector2(49, 49));
            spriteBatch.Draw(vblock, new Vector2(1070, 49));
            spriteBatch.Draw(hblock, new Vector2(1053, 49));
            spriteBatch.Draw(vblock, new Vector2(49, 645));
            spriteBatch.Draw(hblock, new Vector2(49, 662));
            spriteBatch.Draw(vblock, new Vector2(1070, 645));
            spriteBatch.Draw(hblock, new Vector2(1053, 662));

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
