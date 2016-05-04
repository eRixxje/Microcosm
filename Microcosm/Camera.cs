using Gametek.Monogame.Helper;
using Gametek.Monogame.Input;
using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
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

        // Camera
        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }
        public Vector3 Target
        {
            get { return Position + Direction; }
        }
        
        // Fixed 
        private const float panSpeed  = 0.5F;
        private const float zoomSpeed = 0.5F;

        private bool drawAxes;

        public Camera(Vector3 Position, Vector3 Target)
        {
            this.Position  = Position;
            this.Direction = Vector3.Normalize(Target - Position);

            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ScreenManager.game.GraphicsDevice.DisplayMode.AspectRatio, .3f, 1000f);
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
            if (InputManager.IsKeyDown(Keys.W))
                Position -= Vector3.Cross(Vector3.Up, Vector3.Cross(Vector3.Up, Direction)) * panSpeed;
            if (InputManager.IsKeyDown(Keys.S))
                Position += Vector3.Cross(Vector3.Up, Vector3.Cross(Vector3.Up, Direction)) * panSpeed;
            if (InputManager.IsKeyDown(Keys.A))
                Position += Vector3.Cross(Vector3.Up, Direction) * panSpeed;
            if (InputManager.IsKeyDown(Keys.D))
                Position -= Vector3.Cross(Vector3.Up, Direction) * panSpeed;
            
            // Mouse input
            if (InputManager.MouseZoom == ScrollDirection.ZoomIn)
                Position -= Direction * zoomSpeed;
            else if (InputManager.MouseZoom == ScrollDirection.ZoomOut)
                Position += Direction * zoomSpeed;

            // Drag Screen
            Position -= InputManager.GetMouseDragDelta(projection, view);

            // Create View
            view = Matrix.CreateLookAt(Position, Target, Vector3.Up);
        }

        public void Draw(GameTime gameTime)
        {
            if(drawAxes)
                axes.Draw(gameTime, view, projection);
        }
    }
}
