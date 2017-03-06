﻿using Microsoft.Xna.Framework;

namespace Gametek.Monogame.Screen
{
    public abstract class Screen : IScreen
    {
        protected GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return GameEngine.GraphicsDeviceManager; }
        }

        public bool IsInitialized { get; private set; }
        public bool IsVisible { get; set; }

        public Screen(bool IsActive = false)
        {
            
        }

        public void Show()
        {
            if (!IsInitialized)
                Initialize();

            IsVisible = true;
        }
        public void Hide()
        {
            IsVisible = false;
        }

        public virtual void Initialize()
        {
            IsInitialized = true;
        }

        public virtual void LoadContent()
        {

        }
        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}