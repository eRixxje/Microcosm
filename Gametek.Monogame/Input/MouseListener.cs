using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Gametek.Monogame.Input
{
    public sealed class MouseListener
    {
        private MouseState cMouse, pMouse;

        private double ClickTimer = 0;
        private const double TimerDelay = 500;

        public event EventHandler<MouseEventArgs> Clicked;
        public event EventHandler<MouseEventArgs> DoubleClicked;

        public ScrollDirection Zoom
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

        public Point Position
        {
            get { return cMouse.Position; }
        }

        bool waitForDoubleClick;
        DateTime lastButtonDown;

        public void Update(GameTime gameTime)
        {
            pMouse = cMouse;
            cMouse = Mouse.GetState();

            //int timeSinceLastClick = DateTime.Now.Subtract(lastButtonDown).Milliseconds;
            //if (cMouse.LeftButton == ButtonState.Released && pMouse.LeftButton == ButtonState.Pressed)
            //{
            //    if (timeSinceLastClick < 300 && waitForDoubleClick)
            //    {
            //        Debug.WriteLine("Double Clicked");
            //        waitForDoubleClick = false;
            //    }
            //    else
            //    {
            //        lastButtonDown = DateTime.Now;
            //        waitForDoubleClick = true;
            //    }
            //}
            //else
            //{
            //    if (timeSinceLastClick >= 300 && waitForDoubleClick)
            //    {
            //        Debug.WriteLine("Single Click");
            //        waitForDoubleClick = false;
            //    }
            //}

            ClickTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (cMouse.LeftButton == ButtonState.Released && pMouse.LeftButton == ButtonState.Pressed)
            {
                if (ClickTimer < TimerDelay)
                {
                    DoubleClicked?.Invoke(this, new MouseEventArgs(MouseButton.LeftButton));
                    //Debug.WriteLine("DoubleClicked {0}", ClickTimer);
                }
                else
                {
                    Clicked?.Invoke(this, new MouseEventArgs(MouseButton.LeftButton));
                    //Debug.WriteLine("Clicked {0}", ClickTimer);
                }

                ClickTimer = 0;
            }
        }
    }
}
