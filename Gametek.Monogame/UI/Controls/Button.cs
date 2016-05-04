using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame.UI.Helper;
using Gametek.Monogame.Managers;

namespace Gametek.Monogame.UI.Controls
{
    public class Button : Base.Control
    {
        private Vector2 TextLocation = Vector2.Zero;
        private Vector2 TextMargin = new Vector2(1F, 1F);

        private string _text;
        private string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                if (!string.IsNullOrEmpty(_text))
                {
                    Vector2 textSize = Vector2.Add(FontManager.ControlFont.MeasureString(value), TextMargin);
                    TextLocation = (Vector2.Subtract(Size, textSize) / 2);
                }
            }
        }
        
        private Texture2D backRectangle;
        private Color backColor;

        public Button(string ButtonText, Color BackgroundColor, int x, int y, int w, int h) : base(x, y, w, h)
        {
            Text      = ButtonText;
            backColor = BackgroundColor;
            IsVisible = true;

            // Request background texture
            backRectangle = GeometryHelper.GetRectangle(Size, backColor);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!IsVisible) { return; }
            
            ScreenManager.spriteBatch.Draw(backRectangle, Location, Color.White);

            if (!string.IsNullOrEmpty(Text))
                ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, Text, Location + TextLocation, Color.Black);
        }
    }
}
