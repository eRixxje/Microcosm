using Gametek.Monogame;
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
    }

    public sealed class Camera
    {
        private bool _isDirty;

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
        private Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
            private set
            {
                if(_position != value)
                {
                    _isDirty = true;
                    _position = value;
                }
            }
        }

        private Vector3 _target;
        public Vector3 Target
        {
            get { return _target; }
            private set
            {
                if(_target != value)
                {
                    _isDirty = true;
                    _target = value;
                }
            }
        }

        // Fixed 
        private const float panSpeed  = .08f;
        private const float zoomSpeed = .6f;
        private const float rotationSpeed = .04f;

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
            Mouse.SetPosition(GameEngine.GameWindow.ClientBounds.Width / 2, GameEngine.GameWindow.ClientBounds.Height / 2);        
        }

        public void Update(GameTime gameTime)
        {
            // Update View
            if (_isDirty)
            {
                view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
                _isDirty = false;
            }
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
                
                
            }
        }
        public void Arc(Movement cameraMovement)
        {
            switch(cameraMovement)
            {
                case Movement.Left:
                    Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), rotationSpeed)) + Target;
                    break;
                case Movement.Right:
                    Position = Vector3.Transform(Position - Target, Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), -rotationSpeed)) + Target;
                    break;
            }
        }
        public void Zoom(Movement cameraMovement)
        {
            switch(cameraMovement)
            {
                case Movement.Down:
                    Position += camWorld.Backward * zoomSpeed;
                    break;
                case Movement.Up:
                    Position += camWorld.Forward * zoomSpeed;
                    break;
            }
        }
        public void Drag(Point CurrentPosition, Point PreviousPosition)
        {
            Position -= WorldPosition(CurrentPosition) - WorldPosition(PreviousPosition);
            Target -= WorldPosition(CurrentPosition) - WorldPosition(PreviousPosition);
        }

        public Vector3 WorldPosition(Point MouseLocation)
        {
            Vector3 nearPoint = GameEngine.GraphicsDeviceManager.GraphicsDevice.Viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 0.0f), projection, view, Matrix.Identity);
            Vector3 farPoint = GameEngine.GraphicsDeviceManager.GraphicsDevice.Viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 1.0f), projection, view, Matrix.Identity);
            Vector3 direction = Vector3.Normalize(farPoint - nearPoint);

            return (nearPoint - direction * (nearPoint.Y / direction.Y));
        }
    }
}