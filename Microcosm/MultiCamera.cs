using Gametek.Monogame.Input;
using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    public sealed class MultiCamera
    {
        private Vector3 position;
        private Vector3 target;

        //private float yaw, pitch, roll;
        private float speed;

        public Matrix view, projection, rotation;

        public MultiCamera(Vector3 Position, Vector3 Target)
        {
            position = Position;
            target = Target;
            rotation = Matrix.Identity;

            
            UpdateViewMatrix();
            Reset();
        }

        public void LoadContent()
        {

        }
        public void UnloadContent()
        {

        }

        public void Update(GameTime gametime)
        {
            if (InputManager.IsKeyDown(Keys.W))
            {
                MoveCamera(rotation.Forward);
            }
            if (InputManager.IsKeyDown(Keys.S))
            {
                MoveCamera(-rotation.Forward);
            }
            if (InputManager.IsKeyDown(Keys.A))
            {
                MoveCamera(-rotation.Right);
            }
            if (InputManager.IsKeyDown(Keys.D))
            {
                MoveCamera(rotation.Right);
            }
            if (InputManager.IsKeyDown(Keys.E))
            {
                MoveCamera(rotation.Up);
            }
            if (InputManager.IsKeyDown(Keys.Q))
            {
                MoveCamera(-rotation.Up);
            }

            UpdateViewMatrix();
        }
        public void Draw(GameTime gametime)
        {

        }

        private void Reset()
        {
            //yaw = 0.0f;
            //pitch = 0.0f;
            //roll = 0.0f;

            speed = .3f;

            //rotation    = Matrix.Identity;
            //view        = Matrix.Identity;
            projection  = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ScreenManager.Viewport.AspectRatio, 0.3f, 1000f);
        }

        private void MoveCamera(Vector3 addedVector)
        {
            position += speed * addedVector;
        }

        private void UpdateViewMatrix()
        {
            //We'll always use this line of code to set up the View Matrix.
            view = Matrix.CreateLookAt(position, target, rotation.Up);
        }

    }
}
