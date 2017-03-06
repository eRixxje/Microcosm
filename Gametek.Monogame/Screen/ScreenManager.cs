using Microsoft.Xna.Framework;
using System.Linq;

namespace Gametek.Monogame.Screen
{
    public static class ScreenManager
    {
        //private static int startupWidth, startupHeight;
        private static System.Collections.Generic.List<Screen> _screens = new System.Collections.Generic.List<Screen>();


        //public static Game game { get; private set; }
        //public static GraphicsDeviceManager graphics { get; private set; }
        //public static SpriteBatch spriteBatch { get; private set; }
        //public static Viewport Viewport
        //{
        //    get
        //    {
        //        return graphics.GraphicsDevice.Viewport;
        //    }
        //}
        //public static GraphicsDevice GraphicsDevice
        //{
        //    get
        //    {
        //        return graphics.GraphicsDevice;
        //    }
        //}


        //public static void Initialize(Game Game, bool FullScreen, int Width, int Height)
        //{
        //    game = Game;

        //    startupWidth  = Width;
        //    startupHeight = Height;

        //    graphics = new GraphicsDeviceManager(Game)
        //    {
        //        IsFullScreen = FullScreen,
        //        PreferredBackBufferWidth = startupWidth,
        //        PreferredBackBufferHeight = startupHeight
        //    };
        //}

        public static void Register<T>(T screen) where T : Screen
        {
            _screens.Add(screen);
        }

        //public static void Add(Screen Screen)
        //{
        //    if (!_screens.Contains(Screen))
        //    {
        //        Screen.LoadContent();
        //        _screens.Add(Screen);
        //    }
        //}
        //public static void Remove(Screen Screen)
        //{
        //    if (screens.Contains(Screen))
        //    {
        //        Screen.UnloadContent();
        //        screens.Remove(Screen);
        //    }
        //}

        public static void Initialize()
        {
            foreach (var screen in _screens)
                screen.Initialize();
        }

        public static void LoadContent()
        {
            foreach (var screen in _screens)
                screen.LoadContent();
        }
        public static void UnloadContent()
        {
            foreach (var screen in _screens)
                screen.UnloadContent();
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var screen in _screens.Where(s => s.IsVisible))
                screen.Update(gameTime);

            //for (int i = 0; i < _screens.Count; i++)
            //{
            //    if(_screens[i].IsVisible)
            //        _screens[i].Update(gameTime);
            //}
        }
        public static void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens.Where(s => s.IsVisible))
                screen.Draw(gameTime);

            //for (int i = 0; i < _screens.Count; i++)
            //{
            //    if(_screens[i].IsVisible)
            //        _screens[i].Draw(gameTime);
            //}
        }

        //public static void ToggleFullScreen()
        //{
        //    if (graphics.IsFullScreen)
        //    {
        //        graphics.PreferredBackBufferWidth  = startupWidth;
        //        graphics.PreferredBackBufferHeight = startupHeight;
        //    }
        //    else
        //    {
        //        DisplayMode maxRes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.OrderBy(d => d.Width).Last();
        //        Console.WriteLine(maxRes);
        //        graphics.PreferredBackBufferWidth = maxRes.Width;
        //        graphics.PreferredBackBufferHeight = maxRes.Height;
        //    }

        //    graphics.ToggleFullScreen();
        //    graphics.ApplyChanges();
        //}
    }
}
