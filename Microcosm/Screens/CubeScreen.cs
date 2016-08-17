using Gametek.Monogame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Microcosm.Screens
{
    public sealed class CubeScreen : GameScreen
    {
        private Camera camera;
        private cube Cube;

        public CubeScreen(bool IsActive) : base(IsActive)
        {
            camera = new Camera(new Vector3(20, 22, 20), new Vector3(3, 0, 3));
        }

        public override void LoadContent()
        {
            Cube = new cube();
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            camera.Draw(gameTime);
            //Cube.Draw(camera);
        }
    }
}
