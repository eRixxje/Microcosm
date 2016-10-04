using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gametek.Monogame.Manager
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

        public static void Update(GameTime gameTime)
        {
            pMouse = cMouse;
            cMouse = Mouse.GetState();

            pKey = cKey;
            cKey = Keyboard.GetState();

            
        }

        public static bool IsKeyDown(Keys key)
        {
            return cKey.IsKeyDown(key) && pKey.IsKeyDown(key);
        }
        public static bool IsKeyPress(Keys key)
        {
            //System.Diagnostics.Debug.WriteLine("{0}", cKey.IsKeyDown(Keys.F11) && pKey.IsKeyUp(Keys.F11));

            return cKey.IsKeyDown(key) && !pKey.IsKeyDown(key);
        }

        public static bool IsMouseDown(MouseButton Button)
        {
            switch (Button)
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

        public static bool IsMouseClicked(MouseButton Button)
        {
            switch (Button)
            {
                case MouseButton.LeftButton:
                    return (cMouse.LeftButton == ButtonState.Pressed && pMouse.LeftButton != ButtonState.Pressed);
                case MouseButton.RightButton:
                    return (cMouse.RightButton == ButtonState.Pressed && pMouse.RightButton != ButtonState.Pressed);
                case MouseButton.MiddleButton:
                    return (cMouse.MiddleButton == ButtonState.Pressed && pMouse.MiddleButton != ButtonState.Pressed);
                case MouseButton.ExtraButton1:
                    return (cMouse.XButton1 == ButtonState.Pressed && pMouse.XButton1 != ButtonState.Pressed);
                case MouseButton.ExtraButton2:
                    return (cMouse.XButton2 == ButtonState.Pressed && pMouse.XButton2 != ButtonState.Pressed);
                default:
                    return false;
            }
        }
    }
}
