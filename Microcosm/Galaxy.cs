using System.Collections.Generic;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;

namespace Microcosm
{
    public static class Galaxy
    {
        private static bool isInitialized = false;

        public static List<Asteroid> asteroids;

        public static void Initialize()
        {
            asteroids = new List<Asteroid>();

            for (int i = 0; i < 10; i++)
            {
                Asteroid a = new Asteroid(new Vector2(Rand.Next(0, 2000), Rand.Next(0, 2000)), Asteroid.GetDirection(), Asteroid.GetSize());
                a.LoadContent();

                asteroids.Add(a);
            }

            isInitialized = true;
        }
        public static void Update(GameTime gameTime)
        {
            if (!isInitialized)
                return;
            
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update(gameTime);

                // Collisions
                for (int j = 0; j < asteroids.Count; j++)
                {
                    if (asteroids[i] != asteroids[j] && (!asteroids[i].IsCollided && !asteroids[j].IsCollided))
                    {
                        if (asteroids[i].Bounds.Intersects(asteroids[j].Bounds))
                        {
                            asteroids[i].SetCollided(true);
                            asteroids[j].SetCollided(true);
                        }
                    }
                }

                // Remove Dead asteroids
                if (asteroids[i].IsCollided)
                    asteroids.Remove(asteroids[i]);
            }
        } 

        public static void Select(Asteroid asteroid)
        {
            asteroid.SetSelected(true);
        }
    }
}