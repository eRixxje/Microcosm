using Gametek.Monogame.Input;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;

namespace Gametek.Monogame
{
    public class GameBase : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;

        public static KeyBoardListener Keyboard { get; private set; }
        public static MouseListener Mouse { get; private set; }

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
            Keyboard = new KeyBoardListener();
            Mouse = new MouseListener();

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

            Keyboard.Update(gameTime);
            Mouse.Update(gameTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            RenderManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
