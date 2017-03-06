using Microsoft.Xna.Framework;
using Gametek.Monogame;
using Microcosm.Screens;
using Gametek.Monogame.Screen;
using Gametek.Monogame.Input;
using Gametek.Monogame.Managers;

namespace Microcosm
{
    public class Microcosm : GameEngine
    {
        public Microcosm() : base(800, 600, false)
        {           
            Window.Title = "Microcosm Prototyping";
            Window.Position = new Point(100, 100);
            IsMouseVisible = true;

            System.Diagnostics.Debug.WriteLine("Microcosm::CTOR");
        }

        protected override void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("Microcosm::Initialize::Start");

            // Static Managers
            FontManager.Initialize(Content);
            ModelManager.Initialize(Content);

            // Add Screens
            ScreenManager.Register(new MapScreen());
            //ScreenManager.Register(new CubeScreen());
            ScreenManager.Initialize();

            base.Initialize();
            System.Diagnostics.Debug.WriteLine("Microcosm::Initialize::Done");
        }

        protected override void LoadContent()
        {
            System.Diagnostics.Debug.WriteLine("Microcosm::LoadContent::Start");
            ScreenManager.LoadContent();
            base.LoadContent();
            System.Diagnostics.Debug.WriteLine("Microcosm::LoadContent::Done");
        }

        protected override void Update(GameTime gameTime)
        {
            MouseListener.Update(gameTime);
            KeyListener.Update(gameTime);
            ScreenManager.Update(gameTime);

            base.Update(gameTime);

            //if (InputManager.IsKeyDown(Keys.Escape))
            //    Exit();
            
            //if (InputManager.IsKeyPress(Keys.F11))
            //{
            //    ScreenManager.ToggleFullScreen();
            //}
        }
        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
