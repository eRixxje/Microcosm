using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.UI
{
    public static class RenderManager
    {
        private static int _startupWidth;
        private static int _startupHeight;

        private static GraphicsDeviceManager _graphicsDeviceManager;

        private static List<RenderTarget> screens = new List<RenderTarget>();

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return _graphicsDeviceManager.GraphicsDevice;
            }
        }

        public static void Add(RenderTarget Screen)
        {
            if (!screens.Contains(Screen))
            {
                Screen.Initialize();
                Screen.LoadContent();
                screens.Add(Screen);
            }
        }
        public static void Remove(RenderTarget Screen)
        {
            if (screens.Contains(Screen))
            {
                Screen.UnloadContent();
                screens.Remove(Screen);
            }
        }

        public static void Initialize(GraphicsDeviceManager device)
        {
            _graphicsDeviceManager = device;
            _startupWidth = device.PreferredBackBufferWidth;
            _startupHeight = device.PreferredBackBufferHeight;
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                if (screens[i].Enabled)
                    screens[i].Update(gameTime);
            }
        }
        public static void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            for (int i = 0; i < screens.Count; i++)
            {
                if (screens[i].Enabled)
                    screens[i].Draw(gameTime);
            }
        }

        public static void ToggleFullScreen()
        {
            if (_graphicsDeviceManager.IsFullScreen)
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = _startupWidth;
                _graphicsDeviceManager.PreferredBackBufferHeight = _startupHeight;
            }
            else
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }

            _graphicsDeviceManager.ToggleFullScreen();
            _graphicsDeviceManager.ApplyChanges();
        }

        public static void WindowSizeChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                //if (screens[i].Enabled && null != screens[i].Controls)
                //    screens[i].Controls.Invalidate();
            }
        }
    }
}
