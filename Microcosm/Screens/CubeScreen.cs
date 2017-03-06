using Microsoft.Xna.Framework;
using Gametek.Monogame.Screen;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame;

namespace Microcosm.Screens
{
    public sealed class CubeScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private Camera camera;
        private cube Cube;

        public CubeScreen()
        {
            IsVisible = true;
            camera = new Camera(new Vector3(20, 22, 20), new Vector3(3, 0, 3));
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GameEngine.GraphicsDeviceManager.GraphicsDevice);

            Cube = new cube();
            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            camera.Draw(gameTime, _spriteBatch);
            Cube.Draw(camera, new Vector3(0,0,0));

            _spriteBatch.End();
        }
    }
}