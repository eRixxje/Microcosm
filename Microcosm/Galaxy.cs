using System.Collections.Generic;
using Gametek.Monogame.Manager;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;

namespace Microcosm
{
    public sealed class Galaxy
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public List<Asteroid> render;

        public void LoadContent()
        {
            for (int i = 0; i < 10; i++)
            {
                Asteroid a = new Asteroid(new Vector2(Rand.Next(0, 2000), Rand.Next(0, 2000)), Asteroid.GetDirection(), Asteroid.GetSize());
                a.LoadContent();

                asteroids.Add(a);
            }
        }

        public void Update(GameTime gameTime, GridCam Camera)
        {
            render = new List<Asteroid>();

            Vector2 worldPosition = Camera.ScreenToWorld(InputManager.MousePosition.ToVector2());
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update(gameTime);

                // Selection
                if (asteroids[i].Bounds.Contains(worldPosition))
                    asteroids[i].SetSelected(true);
                else
                    asteroids[i].SetSelected(false);

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

                // Renderlist
                Vector2 astPosition = Camera.WorldToScreen(asteroids[i].Position);
                if (Camera.ViewPort.Bounds.Contains(astPosition))
                {
                    render.Add(asteroids[i]);
                }
            }

            // Remove Dead asteroids
            for (int r = 0; r < asteroids.Count; r++)
            {
                if (asteroids[r].IsCollided)
                    asteroids.Remove(asteroids[r]);
            }

            if (InputManager.IsMouseClicked(MouseButton.RightButton))
            {
                Asteroid na = new Asteroid(Camera.ScreenToWorld(InputManager.MousePosition.ToVector2()), Asteroid.GetDirection(), Asteroid.GetSize());
                na.LoadContent();
                asteroids.Add(na);
            }

        } 
    }
}