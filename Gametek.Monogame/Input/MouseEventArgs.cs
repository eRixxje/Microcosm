using System;

namespace Gametek.Monogame.Input
{
    public sealed class MouseEventArgs : EventArgs
    {
        public MouseButton Button;

        public MouseEventArgs(MouseButton button)
        {
            Button = button;
        }
    }
}
