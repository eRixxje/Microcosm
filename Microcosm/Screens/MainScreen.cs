using Microsoft.Xna.Framework;
using Gametek.Monogame.UI.Controls;
using Gametek.Monogame;

namespace Microcosm.Screens
{
    public sealed class MainScreen : GameScreen
    {
        Button b;

        public MainScreen(bool IsActive) : base(IsActive)
        {

        }

        public override void LoadContent()
        {
            b = new Button("Dit is een test", Color.Gray, 30, 30, 300, 100);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            b.Draw(gameTime);
        }
    }
}
