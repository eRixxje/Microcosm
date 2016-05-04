using Microsoft.Xna.Framework;

namespace Gametek.Monogame
{
    public abstract class GameScreen
    {
        public bool IsActive { get; set; }
        
        public GameScreen(bool IsActive = false)
        {
            this.IsActive = IsActive;
        }

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
