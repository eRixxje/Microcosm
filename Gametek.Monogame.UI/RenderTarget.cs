using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame
{
    public abstract class RenderTarget
    {
        protected SpriteBatchEx spriteBatch;
        //public ControlList Controls;

        public bool Enabled { get; set; }

        public RenderTarget(bool IsEnabled = false)
        {
            this.Enabled = IsEnabled;
        }

        public virtual void Initialize()
        {
            //Controls = new ControlList();
        }
        public virtual void LoadContent()
        {
            spriteBatch = new SpriteBatchEx(RenderManager.GraphicsDevice);

            SetupControls();
        }
        public virtual void UnloadContent()
        {

        }
        public virtual void Update(GameTime gameTime)
        {
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
