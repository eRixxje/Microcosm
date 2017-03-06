using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Gametek.Monogame.Input
{
    public static class KeyListener
    {
        private static bool _isInitial;
        private static TimeSpan _lastPressTime;

        private static Keys _previousKey;
        private static KeyboardState _previousState;

        public static int InitialDelay = 0;
        public static int RepeatDelay = 50;

        public static event EventHandler<KeyboardEventArgs> KeyTyped;
        public static event EventHandler<KeyboardEventArgs> KeyPressed;
        public static event EventHandler<KeyboardEventArgs> KeyReleased;

        private static void RaisePressedEvents(GameTime gameTime, KeyboardState currentState)
        {
            if (!currentState.IsKeyDown(Keys.LeftAlt) && !currentState.IsKeyDown(Keys.RightAlt))
            {
                var pressedKeys = Enum.GetValues(typeof(Keys))
                    .Cast<Keys>()
                    .Where(key => currentState.IsKeyDown(key) && _previousState.IsKeyUp(key));

                foreach (var key in pressedKeys)
                {
                    var args = new KeyboardEventArgs(key, currentState);

                    KeyPressed?.Invoke(null, args);

                    //if (args.Character.HasValue)
                        KeyTyped?.Invoke(null, args);

                    _previousKey = key;
                    _lastPressTime = gameTime.TotalGameTime;
                    _isInitial = true;
                }
            }
        }

        private static void RaiseReleasedEvents(KeyboardState currentState)
        {
            var releasedKeys = Enum.GetValues(typeof(Keys))
                .Cast<Keys>()
                .Where(key => currentState.IsKeyUp(key) && _previousState.IsKeyDown(key));

            foreach (var key in releasedKeys)
                KeyReleased?.Invoke(null, new KeyboardEventArgs(key, currentState));
        }

        private static void RaiseRepeatEvents(GameTime gameTime, KeyboardState currentState)
        {
            var elapsedTime = (gameTime.TotalGameTime - _lastPressTime).TotalMilliseconds;

            if (currentState.IsKeyDown(_previousKey) &&
                (_isInitial && elapsedTime > InitialDelay || !_isInitial && elapsedTime > RepeatDelay))
            {
                var args = new KeyboardEventArgs(_previousKey, currentState);

                KeyPressed?.Invoke(null, args);

                //if (args.Character.HasValue)
                    KeyTyped?.Invoke(null, args);

                _lastPressTime = gameTime.TotalGameTime;
                _isInitial = false;
            }
        }


        public static void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();

            RaisePressedEvents(gameTime, currentState);
            RaiseReleasedEvents(currentState);
            RaiseRepeatEvents(gameTime, currentState);

            _previousState = currentState;
        }
    }
}
