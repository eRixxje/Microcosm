using Microsoft.Xna.Framework;

namespace Gametek.Monogame
{
    public class GameEngine : Game
    {
        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public static GameWindow GameWindow { get; private set; }
        
        public GameEngine(int width, int height, bool fullScreen)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = fullScreen,
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };

            Content.RootDirectory = "Content";
            GameWindow = Window;

            System.Diagnostics.Debug.WriteLine("GameEngine::CTOR");
        }
    }
}
