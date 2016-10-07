using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gametek.Monogame.Input
{
    public class KeyBoardListener
    {       
        private KeyboardState cKey, pKey;
        
        public void Update(GameTime gameTime)
        {
            pKey = cKey;
            cKey = Keyboard.GetState();
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
    }
}
