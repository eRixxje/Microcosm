using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gametek.Monogame.Managers
{
    public sealed class ScreenManager
    {
        private static List<GameScreen> screens = new List<GameScreen>();

        public static Game game { get; private set; }
        public static GraphicsDeviceManager graphics { get; private set; }
        public static SpriteBatch spriteBatch { get; private set; }
        public static Viewport Viewport
        {
            get
            {
                return graphics.GraphicsDevice.Viewport;
            }
        }
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphics.GraphicsDevice;
            }
        }

        public static void Initialize(Game Game, bool FullScreen, int Width, int Height)
        {
            game = Game;

            graphics = new GraphicsDeviceManager(Game)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = Width,
                PreferredBackBufferHeight = Height
            };
        }

        public static void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public static void Add(GameScreen Screen)
        {
            if (!screens.Contains(Screen))
            {
                Screen.LoadContent();
                screens.Add(Screen);
            }
        }
        public static void Remove(GameScreen Screen)
        {
            if(screens.Contains(Screen))
            {
                Screen.UnloadContent();
                screens.Remove(Screen);
            }
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                if(screens[i].IsActive)
                    screens[i].Update(gameTime);
            }
        }
        public static void Draw(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                if(screens[i].IsActive)
                    screens[i].Draw(gameTime);
            }
        }
    }
}
