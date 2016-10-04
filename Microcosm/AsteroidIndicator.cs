using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public class AsteroidIndicator : UIElement
    {
        private Asteroid Asteroid;

        public AsteroidIndicator(Asteroid asteroid)
        {
            Asteroid = asteroid;
        }

        public override void Update(GameTime gameTime)
        {
            Position = new Vector2(Asteroid.Position.X + 30, Asteroid.Position.Y);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Power
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X, Position.Y + 10), null, Color.White);

            // Air
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 18, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 18, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 18, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 18, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 18, Position.Y + 10), null, Color.White);

            // Food
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 36, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 36, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 36, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 36, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 36, Position.Y + 10), null, Color.White);

            // Air
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 54, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 54, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(Theme.BAR_EMPTY, new Vector2(Position.X + 54, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 54, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(Theme.BAR_FULL, new Vector2(Position.X + 54, Position.Y + 10), null, Color.White);

            // Name
            spriteBatch.DrawString(AssetManager.ControlFont, Asteroid.Name, new Vector2(Position.X, Position.Y + 20), Theme.BEIGE_LIGHT, Color.Black);

            // Pos
            spriteBatch.DrawString(AssetManager.ControlFont, Asteroid.PositionString, new Vector2(Position.X, Position.Y + 30), Theme.BEIGE_LIGHT, Color.Black);

            base.Draw(gameTime, spriteBatch);
        }
    }
}
