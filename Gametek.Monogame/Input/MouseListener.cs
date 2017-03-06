using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Gametek.Monogame.Input
{
    public static class MouseListener
    {
        private static GameTime _gameTime;

        private static MouseState _currentState;
        private static MouseState _previousState;

        private static bool _dragging;
        
        private static bool _hasDoubleClicked;
        private static MouseEventArgs _mouseDownArgs;
        private static MouseEventArgs _previousClickArgs;
        

        public static int DoubleClickMilliseconds = 500;
        public static int DragThreshold = 2;

        /// <summary>
        ///     Returns true if the mouse has moved between the current and previous frames.
        /// </summary>
        /// <value><c>true</c> if the mouse has moved; otherwise, <c>false</c>.</value>
        public static bool HasMouseMoved => (_previousState.X != _currentState.X) || (_previousState.Y != _currentState.Y);

        public static event EventHandler<MouseEventArgs> MouseDown;
        public static event EventHandler<MouseEventArgs> MouseUp;
        public static event EventHandler<MouseEventArgs> MouseClicked;
        public static event EventHandler<MouseEventArgs> MouseDoubleClicked;
        public static event EventHandler<MouseEventArgs> MouseMoved;
        public static event EventHandler<MouseEventArgs> MouseWheelMoved;
        public static event EventHandler<MouseEventArgs> MouseDragStart;
        public static event EventHandler<MouseEventArgs> MouseDrag;
        public static event EventHandler<MouseEventArgs> MouseDragEnd;

        private static void CheckButtonPressed(Func<MouseState, ButtonState> getButtonState, MouseButton button)
        {
            if ((getButtonState(_currentState) == ButtonState.Pressed) &&
                (getButtonState(_previousState) == ButtonState.Released))
            {
                var args = new MouseEventArgs(_gameTime.TotalGameTime, _previousState, _currentState, button);

                MouseDown?.Invoke(null, args);
                _mouseDownArgs = args;

                if (_previousClickArgs != null)
                {
                    // If the last click was recent
                    var clickMilliseconds = (args.Time - _previousClickArgs.Time).TotalMilliseconds;

                    if (clickMilliseconds <= DoubleClickMilliseconds)
                    {
                        MouseDoubleClicked?.Invoke(null, args);
                        _hasDoubleClicked = true;
                    }

                    _previousClickArgs = null;
                }
            }
        }

        private static void CheckButtonReleased(Func<MouseState, ButtonState> getButtonState, MouseButton button)
        {
            if ((getButtonState(_currentState) == ButtonState.Released) &&
                (getButtonState(_previousState) == ButtonState.Pressed))
            {
                var args = new MouseEventArgs(_gameTime.TotalGameTime, _previousState, _currentState, button);

                if (_mouseDownArgs.Button == args.Button)
                {
                    var clickMovement = DistanceBetween(args.Position, _mouseDownArgs.Position);

                    // If the mouse hasn't moved much between mouse down and mouse up
                    if (clickMovement < DragThreshold)
                    {
                        if (!_hasDoubleClicked)
                            MouseClicked?.Invoke(null, args);
                    }
                    else // If the mouse has moved between mouse down and mouse up
                    {
                        MouseDragEnd?.Invoke(null, args);
                        _dragging = false;
                    }
                }

                MouseUp?.Invoke(null, args);

                _hasDoubleClicked = false;
                _previousClickArgs = args;
            }
        }

        private static void CheckMouseDragged(Func<MouseState, ButtonState> getButtonState, MouseButton button)
        {
            if ((getButtonState(_currentState) == ButtonState.Pressed) &&
                (getButtonState(_previousState) == ButtonState.Pressed))
            {
                var args = new MouseEventArgs(_gameTime.TotalGameTime, _previousState, _currentState, button);

                if (_mouseDownArgs.Button == args.Button)
                {
                    if (_dragging)
                        MouseDrag?.Invoke(null, args);
                    else
                    {
                        // Only start to drag based on DragThreshold
                        var clickMovement = DistanceBetween(args.Position, _mouseDownArgs.Position);

                        if (clickMovement > DragThreshold)
                        {
                            _dragging = true;
                            MouseDragStart?.Invoke(null, args);
                        }
                    }
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            _currentState = Mouse.GetState();

            CheckButtonPressed(s => s.LeftButton, MouseButton.Left);
            CheckButtonPressed(s => s.MiddleButton, MouseButton.Middle);
            CheckButtonPressed(s => s.RightButton, MouseButton.Right);
            CheckButtonPressed(s => s.XButton1, MouseButton.XButton1);
            CheckButtonPressed(s => s.XButton2, MouseButton.XButton2);

            CheckButtonReleased(s => s.LeftButton, MouseButton.Left);
            CheckButtonReleased(s => s.MiddleButton, MouseButton.Middle);
            CheckButtonReleased(s => s.RightButton, MouseButton.Right);
            CheckButtonReleased(s => s.XButton1, MouseButton.XButton1);
            CheckButtonReleased(s => s.XButton2, MouseButton.XButton2);

            // Check for any sort of mouse movement.
            if (HasMouseMoved)
            {
                MouseMoved?.Invoke(null,
                    new MouseEventArgs(gameTime.TotalGameTime, _previousState, _currentState));

                CheckMouseDragged(s => s.LeftButton, MouseButton.Left);
                CheckMouseDragged(s => s.MiddleButton, MouseButton.Middle);
                CheckMouseDragged(s => s.RightButton, MouseButton.Right);
                CheckMouseDragged(s => s.XButton1, MouseButton.XButton1);
                CheckMouseDragged(s => s.XButton2, MouseButton.XButton2);
            }

            // Handle mouse wheel events.
            if (_previousState.ScrollWheelValue != _currentState.ScrollWheelValue)
            {
                MouseWheelMoved?.Invoke(null,
                    new MouseEventArgs(gameTime.TotalGameTime, _previousState, _currentState));
            }

            _previousState = _currentState;
        }

        private static int DistanceBetween(Point a, Point b)
        {
            return System.Math.Abs(a.X - b.X) + System.Math.Abs(a.Y - b.Y);
        }
    }
}
