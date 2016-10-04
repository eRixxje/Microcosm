using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;

namespace Gametek.Monogame
{
    public class GameBase : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;

        public GameBase(int Width, int Height, bool FullScreen)
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = FullScreen,
                PreferredBackBufferWidth = Width,
                PreferredBackBufferHeight = Height
            };

            Content.RootDirectory = "Content";
            // Position Center
            Window.Position = new Point(50, 50);
            
            // Forward Window Size Changed Events to the ScreenManager.
            Window.ClientSizeChanged += RenderManager.WindowSizeChanged;
        }

        protected override void Initialize()
        {
            RenderManager.Initialize(_graphicsDeviceManager);
            AssetManager.Initialize(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            RenderManager.Update(gameTime);
            InputManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
