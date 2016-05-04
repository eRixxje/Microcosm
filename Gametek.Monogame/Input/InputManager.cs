using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gametek.Monogame.Input
{
    public enum ScrollDirection
    {
        None,
        ZoomIn,
        ZoomOut
    }

    public static class InputManager
    {
        private static MouseState cMouse, pMouse;
        private static KeyboardState cKey, pKey;

        private static Texture2D _cursor;
        public static Texture2D Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        public static int ScrollWheelDelta
        {
            get { return cMouse.ScrollWheelValue - pMouse.ScrollWheelValue; }
        }

        public static ScrollDirection MouseZoom
        {
            get
            {
                if (cMouse.ScrollWheelValue < pMouse.ScrollWheelValue)
                    return ScrollDirection.ZoomIn;
                if (cMouse.ScrollWheelValue > pMouse.ScrollWheelValue)
                    return ScrollDirection.ZoomOut;

                return ScrollDirection.None;
            }
        }
        
        public static Vector2 MousePosition
        {
            get { return new Vector2(cMouse.X, cMouse.Y); }
        }

        public static void LoadContent(ContentManager content)
        {
            _cursor = content.Load<Texture2D>("Cursors\\cursors");
        }

        public static void Update()
        {
            pMouse = cMouse;
            cMouse = Mouse.GetState();

            pKey = cKey;
            cKey = Keyboard.GetState();
        }
        public static void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle      = new Rectangle(52,0,26,26);
            Rectangle destinationRectangle = new Rectangle((int)MousePosition.X, (int)MousePosition.Y, 26, 26);

            ScreenManager.spriteBatch.Draw(Cursor, destinationRectangle, sourceRectangle, Color.White);

            //spriteBatch.Draw(Cursor, MousePosition, null, Color.White, 0f, Vector2.Zero, .1f, SpriteEffects.None, 0f);
        }

        public static bool IsKeyDown(Keys key)
        {
            return cKey.IsKeyDown(key);
        }
        public static bool IsKeyPress(Keys key)
        {
            return cKey.IsKeyDown(key) && !pKey.IsKeyDown(key);
        }

        public static Vector3 GetMouseDragDelta(Matrix projection, Matrix view)
        {
            Vector3 res = new Vector3();

            if (cMouse.RightButton == ButtonState.Pressed)
                res = SelectedVector3(cMouse.Position, projection, view) - SelectedVector3(pMouse.Position, projection, view);

            return res;
        }

        public static Vector3 SelectedVector3(Matrix projection, Matrix view)
        {
            return SelectedVector3(cMouse.Position, projection, view);
        }

        public static Vector3 WorldPosition(Vector2 MouseLocation, Viewport viewport, Matrix proj, Matrix view, Matrix world)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 0.0f), proj, view, world);
            Vector3 farPoint = viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 1.0f), proj, view, world);
            Vector3 direction = Vector3.Normalize(farPoint - nearPoint);

            return (nearPoint - direction * (nearPoint.Y / direction.Y));
        }

        public static Vector3 SelectedVector3(Point mouseLocation, Matrix projection, Matrix view)
        {
            Vector3 nearPoint = ScreenManager.Viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 0.0f), projection, view, Matrix.Identity);
            Vector3 farPoint = ScreenManager.Viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 1.0f), projection, view, Matrix.Identity);
            Vector3 direction = Vector3.Normalize(farPoint - nearPoint);

            Plane p = new Plane(0, -1, 0, 0);
            Ray MouseRay = new Ray(nearPoint, direction);

            // calculate distance of intersection point from r.origin
            float denominator = Vector3.Dot(p.Normal, MouseRay.Direction);
            float numerator = Vector3.Dot(p.Normal, MouseRay.Position) + p.D;
            float t = -(numerator / denominator);

            Vector3 pickedPosition = nearPoint + direction * t;

            return pickedPosition;
        }
    }
}
