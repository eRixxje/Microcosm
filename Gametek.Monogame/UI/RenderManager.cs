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

        private static Dictionary<string, RenderTarget> screens = new Dictionary<string, RenderTarget>();

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return _graphicsDeviceManager.GraphicsDevice;
            }
        }

        public static void Add(RenderTarget Screen)
        {
            if (!screens.ContainsKey(Screen.Name))
            {
                Screen.Initialize();
                Screen.LoadContent();
                screens.Add(Screen.Name, Screen);
            }
        }
        public static void Remove(RenderTarget Screen)
        {
            if (screens.ContainsKey(Screen.Name))
            {
                Screen.UnloadContent();
                screens.Remove(Screen.Name);
            }
        }

        public static void Switch(string fromName, string toName)
        {
            screens[toName].IsEnabled = true;
            screens[fromName].IsEnabled = false;
        }

        public static void Initialize(GraphicsDeviceManager device)
        {
            _graphicsDeviceManager = device;
            _startupWidth = device.PreferredBackBufferWidth;
            _startupHeight = device.PreferredBackBufferHeight;
        }

        public static void Update(GameTime gameTime)
        {
            foreach(KeyValuePair<string, RenderTarget> screen in screens)
            {
                if (screen.Value.IsEnabled)
                    screen.Value.Update(gameTime);
            }
        }
        public static void Draw(GameTime gameTime)
        {
            foreach (KeyValuePair<string, RenderTarget> screen in screens)
            {
                if (screen.Value.IsEnabled)
                    screen.Value.Draw(gameTime);
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
