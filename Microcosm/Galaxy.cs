using Gametek.Monogame;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Microcosm
{
    public sealed class Galaxy
    {
        private List<Asteroid> asteroids = new List<Asteroid>();
        private Rectangle ViewPort;

        Grid grid;

        public Galaxy()
        {
            grid = new Grid(20, 12, 50, 50);

            ViewPort = new Rectangle(grid.OffSet, grid.OffSet, grid.Width, grid.Height);
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            grid.LoadContent();

            for (int i = 0; i < 10; i++)
            {
                asteroids.Add(new Asteroid(new Vector2(Rand.Next(50, 1000), Rand.Next(50, 600)), Asteroid.GetDirection(), Asteroid.GetSize()));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(var a in asteroids)
            {
                if (a.InViewPort && ViewPort.Contains(a.Position))
                    a.Update(gameTime);
                else
                    a.InViewPort = false;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatchEx spriteBatch)
        {
            grid.Draw(gameTime, spriteBatch);

            foreach (var a in asteroids)
                a.Draw(gameTime, spriteBatch);
        }        
    }
}