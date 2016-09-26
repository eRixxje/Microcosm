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
        public List<Asteroid> asteroids = new List<Asteroid>();
        public List<Asteroid> render;

        public void LoadContent()
        {
            for (int i = 0; i < 10; i++)
            {
                asteroids.Add(new Asteroid(new Vector2(Rand.Next(0, 2000), Rand.Next(0, 2000)), Asteroid.GetDirection(), Asteroid.GetSize()));
            }
        }

        public void Update(GameTime gameTime, GridCam Camera)
        {
            render = new List<Asteroid>();

            Vector2 worldPosition = Camera.ScreenToWorld(InputManager.MousePosition.ToVector2());
            foreach (var a in asteroids)
            {
                a.Update(gameTime);

                if (a.BoundingBox.Contains(worldPosition))
                    a.SetSelected(true);
                else
                    a.SetSelected(false);

                Vector2 astPosition = Camera.WorldToScreen(a.Position);
                if (Camera.ViewPort.Bounds.Contains(astPosition))
                {
                    render.Add(a);
                }
            }


        } 
    }
}