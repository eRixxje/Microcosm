using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame
{
    public abstract class RenderTarget
    {
        protected float deltaTime;
        protected SpriteBatch spriteBatch;
        //public ControlList Controls;

        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public RenderTarget(string screenName, bool isEnabled = false)
        {
            this.Name = screenName;
            this.IsEnabled = isEnabled;
        }

        public virtual void Initialize()
        {
            //Controls = new ControlList();
        }
        public virtual void LoadContent()
        {
            spriteBatch = new SpriteBatch(RenderManager.GraphicsDevice);

            SetupControls();
        }
        public virtual void UnloadContent()
        {

        }
        public virtual void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            HandleInput();
            UpdateControls();
            //Controls.Update(gameTime);
        }
        public virtual void Draw(GameTime gameTime)
        {
            //Controls.Draw(gameTime, spriteBatch);
        }

        public abstract void HandleInput();
        public abstract void SetupControls();
        public abstract void UpdateControls();
    }
}
