﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Gametek.Monogame.Input
{
    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(TimeSpan time, MouseState previousState, MouseState currentState, MouseButton button = MouseButton.None)
        {
            PreviousState = previousState;
            CurrentState = currentState;
            //Position = viewportAdapter?.PointToScreen(currentState.X, currentState.Y)
            //           ?? new Point(currentState.X, currentState.Y);
            Position = new Point(currentState.X, currentState.Y);
            Button = button;
            ScrollWheelValue = currentState.ScrollWheelValue;
            ScrollWheelDelta = currentState.ScrollWheelValue - previousState.ScrollWheelValue;
            Time = time;
        }

        public TimeSpan Time { get; private set; }

        public MouseState PreviousState { get; }
        public MouseState CurrentState { get; }
        public Point Position { get; private set; }
        public MouseButton Button { get; private set; }
        public int ScrollWheelValue { get; private set; }
        public int ScrollWheelDelta { get; private set; }

        public Vector2 DistanceMoved => CurrentState.Position.ToVector2() - PreviousState.Position.ToVector2();
    }

}
