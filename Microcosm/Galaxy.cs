using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microcosm
{
    public sealed class Galaxy
    {
        private List<Asteroid> asteroids = new List<Asteroid>();
        public List<Asteroid> render;

        //private Grid _grid;
        public GridCam Camera;

        public Galaxy()
        {
            //_grid = new Grid(2000/50, 2000/50, 50, 50);

            Camera = new GridCam(new Viewport(50, 50, 1071, 661));
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            //_grid.LoadContent();

            for (int i = 0; i < 10; i++)
            {
                asteroids.Add(new Asteroid(new Vector2(Rand.Next(0, 2000), Rand.Next(0, 2000)), Asteroid.GetDirection(), Asteroid.GetSize()));
            }
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            render = new List<Asteroid>();

            // Move to cam?
            Vector2 worldPosition = Camera.ScreenToWorld(InputManager.MousePosition.ToVector2());

            foreach (var a in asteroids)
            {
                a.Update(gameTime);

                // Mouse Selection
                if (a.BoundingBox.Contains(worldPosition))
                    a.SetSelected(true);
                else
                    a.SetSelected(false);

                Vector2 astPosition = Camera.WorldToScreen(a.Position);
                if (Camera.ViewPort.Bounds.Contains(astPosition))
                {
                    //Debug.WriteLine("{0} : {1}", a.Name, astPosition);
                    render.Add(a);
                }
            }

             // Move
            if (InputManager.IsKeyDown(Keys.W))
                Camera.Move(new Vector2(0, -250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.S))
                Camera.Move(new Vector2(0, 250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.A))
                Camera.Move(new Vector2(-250, 0) * deltaTime);

            if (InputManager.IsKeyDown(Keys.D))
                Camera.Move(new Vector2(250, 0) * deltaTime);
        }
        public void Draw(GameTime gameTime, SpriteBatchEx spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());

            //_grid.Draw(gameTime, spriteBatch);
            foreach (var a in render)
            {
                a.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }   
    }
}