using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Microcosm.Screens
{
    public sealed class MapScreen : RenderTarget
    {
        GalaxyGrid grid;
        //Button b;
        //TextBlock t;
        
        public MapScreen(string name, bool isEnabled) : base(name, isEnabled)
        {
            grid = new GalaxyGrid(new Vector2(50,50), new Vector2(1000, 600), 50);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            grid.LoadContent();

            base.LoadContent();
        }
        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            grid.Update(gameTime);
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
            if (InputManager.IsKeyDown(Keys.W))
                grid.Move(new Vector2(0, -250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.S))
                grid.Move(new Vector2(0, 250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.A))
                grid.Move(new Vector2(-250, 0) * deltaTime);

            if (InputManager.IsKeyDown(Keys.D))
                grid.Move(new Vector2(250, 0) * deltaTime);

            if (InputManager.MouseZoom == ScrollDirection.ZoomIn)
                grid.Zoom(-0.2f);
            if (InputManager.MouseZoom == ScrollDirection.ZoomOut)
                grid.Zoom(0.2f);
        }
        public override void SetupControls()
        {
           
        }
        public override void UpdateControls()
        {
            
        }
    }
}
