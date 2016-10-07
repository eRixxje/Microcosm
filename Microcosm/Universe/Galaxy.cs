using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Microcosm.Universe
{
    public sealed class Galaxy
    {
        private bool isInitialized = false;

        public AsteroidList Asteroids { get; private set; }

        public void Initialize()
        {
            Asteroids = new AsteroidList();
            
            for (int i = 0; i < 10; i++)
            {
                Asteroid a = new Asteroid(Rand.NextVector2(2000), Asteroid.GetDirection(), Asteroid.GetSize());
                Asteroids.Add(a);
            }

            isInitialized = true;
        }
        public void Update(GameTime gameTime)
        {
            if (!isInitialized)
                return;

            for (int i = 0; i < Asteroids.Count; i++)
            {
                Asteroids[i].Update(gameTime);

                // Collisions [Optimisation needed]
                for (int j = 0; j < Asteroids.Count; j++)
                {
                    if (Asteroids[i] != Asteroids[j] && (!Asteroids[i].IsCollided && !Asteroids[j].IsCollided))
                    {
                        if (Asteroids[i].Bounds.Intersects(Asteroids[j].Bounds))
                        {
                            Asteroids[i].IsCollided = true;
                            Asteroids[j].IsCollided = true;
                        }
                    }
                }

                // Remove Dead asteroids
                if (Asteroids[i].IsCollided)
                    Asteroids.Remove(Asteroids[i]); 
            }
        }

        //public Asteroid GetAsteroidAt(Vector2 Position)
        //{
        //    return Asteroids.GetAtPosition(Position);
        //}
    }
}