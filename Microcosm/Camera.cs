using Gametek.Monogame;
using Gametek.Monogame.UI.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private const float minZoom = 1f;
        private const float maxZoom = 120f;

        public Camera(Vector3 Position, Vector3 Target)
        {
            this.Position  = Position;
            this.Target    = Target;

            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GameEngine.GraphicsDeviceManager.GraphicsDevice.Viewport.AspectRatio, .1f, 1000f);
        }

        public void Initialize()
        {
            // Set mouse position and do initial get state
            //Mouse.SetPosition(GameEngine.Window.ClientBounds.Width / 2, GameEngine.Window.ClientBounds.Height / 2);        
        }

        public void Update(GameTime gameTime)
        {
            // Update View
            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Texture2D pixel = GeometryHelper.GetRectangle(new Vector2(1, 1), Color.Red);

            //Find screen equivalent of 3D location in world
            Vector3 screenLocation = GameEngine.GraphicsDeviceManager.GraphicsDevice.Viewport.Project(Target, projection, view, Matrix.Identity);

            //Draw our pixel texture there
            spriteBatch.Draw(pixel, new Vector2(screenLocation.X, screenLocation.Y), Color.Red);
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