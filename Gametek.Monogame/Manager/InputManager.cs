﻿using Microsoft.Xna.Framework;
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

    public class InputManager
    {
        private MouseState cMouse, pMouse;
        private KeyboardState cKey, pKey;

        private double ClickTimer;
        private const double TimerDelay = 500;
        private bool _doubleClicked;

        public ScrollDirection MouseZoom
        {
            get
            {
                if (cMouse.ScrollWheelValue < pMouse.ScrollWheelValue)
                    return ScrollDirection.ZoomOut;
                if (cMouse.ScrollWheelValue > pMouse.ScrollWheelValue)
                    return ScrollDirection.ZoomIn;

                return ScrollDirection.None;
            }
        }

        public Point MousePosition
        {
            get { return cMouse.Position; }
        }

        public void Update(GameTime gameTime)
        {
            pMouse = cMouse;
            cMouse = Mouse.GetState();

            pKey = cKey;
            cKey = Keyboard.GetState();

            ClickTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (cMouse.LeftButton == ButtonState.Pressed && pMouse.LeftButton == ButtonState.Released)
            {
                if (ClickTimer < TimerDelay)
                {
                    _doubleClicked = true;
                }
                else
                {
                    _doubleClicked = false;
                }

                ClickTimer = 0;
            }

            System.Diagnostics.Debug.WriteLine("{0}", _doubleClicked);
        }

        public bool IsKeyDown(Keys key)
        {
            return cKey.IsKeyDown(key) && pKey.IsKeyDown(key);
        }
        public bool IsKeyPress(Keys key)
        {
            //System.Diagnostics.Debug.WriteLine("{0}", cKey.IsKeyDown(Keys.F11) && pKey.IsKeyUp(Keys.F11));

            return cKey.IsKeyDown(key) && !pKey.IsKeyDown(key);
        }

        public bool IsMouseDown(MouseButton Button)
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

        public bool IsMouseClicked(MouseButton Button)
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

        public bool IsMouseDoubleClicked(MouseButton Button)
        {
            switch (Button)
            {
                case MouseButton.LeftButton:
                    return _doubleClicked;
                default:
                    return false;
            }
        }
    }
}
