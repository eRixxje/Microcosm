using Gametek.Monogame.Helper;
using Gametek.Monogame.Managers;
using Gametek.Monogame.UI.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    public sealed class Camera
    {
        private Axes axes = new Axes();

        public Matrix view { get; private set; }
        public Matrix projection { get; private set; }

        public string cameraPositionString
        {
            get
            {
                return string.Format("X:{0}, Y:{1}, Z:{2}", Position.X, Position.Y, Position.Z );
            }
        }
        public string cameraTargetString
        {
            get
            {
                return string.Format("X:{0}, Y:{1}, Z:{2}", Target.X, Target.Y, Target.Z);
            }
        }

        // Camera
        public Vector3 Position { get; private set; }
        public Vector3 Target { get; private set; }
         
        // Fixed 
        private const float panSpeed  = .08f;
        private const float zoomSpeed = .4f;
        private const float rotationSpeed = .01f;

        private const float minZoom = 5f;
        private const float maxZoom = 12f;
        


        private bool drawAxes;

        public Camera(Vector3 Position, Vector3 Target)
        {
            this.Position  = Position;
            this.Target    = Target;

            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ScreenManager.GraphicsDevice.Viewport.AspectRatio, .1f, 100f);
        }

        public void Initialize()
        {
            // Set mouse position and do initial get state
            Mouse.SetPosition(ScreenManager.game.Window.ClientBounds.Width / 2, ScreenManager.game.Window.ClientBounds.Height / 2);
        }

        public void LoadContent()
        {
            axes.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            Matrix camWorld = Matrix.Invert(view);
            Vector3 fwd = camWorld.Forward;
            fwd.Y = 0;

            if (InputManager.IsKeyPress(Keys.F2))
                drawAxes = !drawAxes;

            // Keyboard input
            if (InputManager.IsKeyDown(Keys.W))
            {
                Position += fwd * panSpeed;
                Target += fwd * panSpeed;
            }
            if (InputManager.IsKeyDown(Keys.S))
            {
                Position -= fwd * panSpeed;
                Target -= fwd * panSpeed;
            }
            if (InputManager.IsKeyDown(Keys.A))
            {
                Position += camWorld.Left * panSpeed;
                Target += camWorld.Left * panSpeed;
            }
            if (InputManager.IsKeyDown(Keys.D))
            {
                Position += camWorld.Right * panSpeed;
                Target += camWorld.Right * panSpeed;
            }

            // Rotate
            if (InputManager.IsKeyDown(Keys.Q))
            {
                Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), rotationSpeed)) + Target;
            }
            if (InputManager.IsKeyDown(Keys.E))
            {
                Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), -rotationSpeed)) + Target;
            }

            // Zoom
            if(InputManager.MouseZoom == ScrollDirection.ZoomIn)
            {
                Position += camWorld.Backward * zoomSpeed;
            }
            if (InputManager.MouseZoom == ScrollDirection.ZoomOut)
            {
                Position += camWorld.Forward * zoomSpeed;
            }

            // Drag
            if (InputManager.IsRightMouseDown)
            {
                Position -= InputManager.GetMouseDragDelta(projection, view);
                Target -= InputManager.GetMouseDragDelta(projection, view);
            }

            // Create View
            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }

        public void Draw(GameTime gameTime)
        {
            if(drawAxes)
                axes.Draw(gameTime, view, projection);

            Texture2D pixel = GeometryHelper.GetRectangle(new Vector2(1, 1), Color.Red);

            // Find screen equivalent of 3D location in world
            Vector3 screenLocation = ScreenManager.GraphicsDevice.Viewport.Project(Target, projection, view, Matrix.Identity);

            // Draw our pixel texture there
            ScreenManager.spriteBatch.Draw(pixel, new Vector2(screenLocation.X, screenLocation.Y), Color.Red);
        }
    }
}
