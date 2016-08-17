using Gametek.Monogame.Helper;
using Gametek.Monogame.Managers;
using Gametek.Monogame.UI.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    public enum Movement
    {
        Left,
        Right,
        Forward,
        Back,
        Up,
        Down,
        ArcLeft,
        ArcRight,
        ZoomIn,
        ZoomOut
    }

    public sealed class Camera
    {
        private Axes axes = new Axes();

        public Matrix view { get; private set; }
        public Matrix projection { get; private set; }

        private Matrix camWorld
        {
            get { return Matrix.Invert(view); }
        }

        public string cameraPositionString
        {
            get
            {
                return string.Format("{0:0.00}, {1:0.00}, {2:0.00}", Position.X, Position.Y, Position.Z );
            }
        }
        public string cameraTargetString
        {
            get
            {
                return string.Format("{0:0.00}, {1:0.00}, {2:0.00}", Target.X, Target.Y, Target.Z);
            }
        }

        // Camera
        public Vector3 Position { get; private set; }
        public Vector3 Target { get; private set; }

        // Fixed 
        private const float panSpeed  = .08f;
        private const float zoomSpeed = .6f;
        private const float rotationSpeed = .01f;

        private const float minZoom = 5f;
        private const float maxZoom = 30f;
        
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
            if (InputManager.IsKeyPress(Keys.F2))
                drawAxes = !drawAxes;

            // Keyboard input
            if (InputManager.IsKeyDown(Keys.W) && InputManager.IsKeyDown(Keys.LeftShift))
                Move(Movement.Up);
            if (InputManager.IsKeyDown(Keys.W) && !InputManager.IsKeyDown(Keys.LeftShift))
                Move(Movement.Forward);
            if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.LeftShift))
                Move(Movement.Down);
            if (InputManager.IsKeyDown(Keys.S) && !InputManager.IsKeyDown(Keys.LeftShift))
                Move(Movement.Back);
            if (InputManager.IsKeyDown(Keys.A))
                Move(Movement.Left);
            if (InputManager.IsKeyDown(Keys.D))
                Move(Movement.Right);
            if (InputManager.IsKeyDown(Keys.Z))
                Move(Movement.ArcLeft);
            if (InputManager.IsKeyDown(Keys.X))
                Move(Movement.ArcRight);
            if (InputManager.MouseZoom == ScrollDirection.ZoomIn && Position.Y < maxZoom)
                Move(Movement.ZoomIn);
            if (InputManager.MouseZoom == ScrollDirection.ZoomOut && Position.Y > minZoom)
                Move(Movement.ZoomOut);

            // Drag
            if (InputManager.IsMouseDown(MouseButton.RightButton))
            {
                Position -= InputManager.GetMouseDragDelta(projection, view);
                Target -= InputManager.GetMouseDragDelta(projection, view);
            }

            // Update View
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

        public void Move(Movement CameraMovement)
        {
            Vector3 fwd = camWorld.Forward;
            fwd.Y = 0;

            switch (CameraMovement)
            {
                case Movement.Up:
                    Position += camWorld.Down;
                    break;
                case Movement.Down:
                    Position -= camWorld.Down;
                    break;
                case Movement.Left:
                    Position += camWorld.Left * panSpeed;
                    Target += camWorld.Left * panSpeed;
                    break;
                case Movement.Right:
                    Position -= camWorld.Left * panSpeed;
                    Target -= camWorld.Left * panSpeed;
                    break;
                case Movement.Forward:
                    Position += fwd * panSpeed;
                    Target += fwd * panSpeed;
                    break;
                case Movement.Back:
                    Position -= fwd * panSpeed;
                    Target -= fwd * panSpeed;
                    break;
                case Movement.ArcLeft:
                    Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), rotationSpeed)) + Target;
                    break;
                case Movement.ArcRight:
                    Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), -rotationSpeed)) + Target;
                    break;
                case Movement.ZoomIn:
                    Position += camWorld.Backward * zoomSpeed;
                    break;
                case Movement.ZoomOut:
                    Position += camWorld.Forward * zoomSpeed;
                    break;
            }
        }
    }
}
