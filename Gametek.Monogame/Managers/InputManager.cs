using Gametek.Monogame.UI.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gametek.Monogame.Managers
{
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        ExtraButton1,
        ExtraButton2
    }

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

        private static Texture2D Cursor;
        private static Rectangle cursorSource = new Rectangle(0, 0, 26, 26);
        private static Rectangle cursorDest = new Rectangle(0, 0, 16, 16);

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
        
        public static Point MousePosition
        {
            get { return cMouse.Position; }
        }

        public static void LoadContent(ContentManager content)
        {
            Cursor = content.Load<Texture2D>("Cursors\\cursors");
        }

        public static void Update()
        {
            pMouse = cMouse;
            cMouse = Mouse.GetState();

            pKey = cKey;
            cKey = Keyboard.GetState();

            // Set Cursor Rect.
            if (IsMouseDown(MouseButton.RightButton))
            {
                cursorSource.X = 52; cursorSource.Y = 0;
            }
            else
                cursorSource.X = 0; cursorSource.Y = 0;
        }
        public static void Draw(GameTime gameTime)
        {
            cursorDest.X = (int)MousePosition.X;
            cursorDest.Y = (int)MousePosition.Y;

            ScreenManager.spriteBatch.Draw(Cursor, cursorDest, cursorSource, Color.White);
        }

        public static bool IsKeyDown(Keys key)
        {
            return cKey.IsKeyDown(key) && pKey.IsKeyDown(key);
        }
        public static bool IsKeyPress(Keys key)
        {
            return cKey.IsKeyDown(key) && !pKey.IsKeyDown(key);
        }

        public static bool IsMouseDown(MouseButton Button)
        {
            switch(Button)
            {
                case MouseButton.LeftButton:
                    return (cMouse.LeftButton == ButtonState.Pressed && pMouse.LeftButton == ButtonState.Pressed);
                case MouseButton.RightButton:
                    return (cMouse.RightButton == ButtonState.Pressed && pMouse.RightButton == ButtonState.Pressed);
                case MouseButton.MiddleButton:
                    return (cMouse.MiddleButton == ButtonState.Pressed && pMouse.MiddleButton == ButtonState.Pressed);
                case MouseButton.ExtraButton1:
                    return (cMouse.XButton1 == ButtonState.Pressed && pMouse.XButton1 == ButtonState.Pressed);
                case MouseButton.ExtraButton2:
                    return (cMouse.XButton2 == ButtonState.Pressed && pMouse.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }

        public static Vector3 GetMouseDragDelta(Matrix projection, Matrix view)
        {
            Vector3 res = new Vector3();

            if (IsMouseDown(MouseButton.RightButton))
                res = GeometryHelper.WorldPosition(cMouse.Position, projection, view) - GeometryHelper.WorldPosition(pMouse.Position, projection, view);

            return res;
        }
    }
}
