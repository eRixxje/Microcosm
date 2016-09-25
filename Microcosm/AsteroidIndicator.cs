using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public class AsteroidIndicator
    {
        public static Vector2 Position { get; private set; }

        private static Texture2D ind_e = Geometry.Border(new Vector2(14, 4), 1, Theme.BEIGE_MEDIUM);
        private static Texture2D ind_f  = Geometry.Rectangle(new Vector2(14, 4), Theme.BEIGE_LIGHT);

        public static void Draw(SpriteBatchEx spriteBatch, Asteroid asteroid)
        {
            Position = new Vector2(asteroid.BoundingBox.Right + 10, asteroid.BoundingBox.Top);

            // Power
            spriteBatch.Draw(ind_e, new Vector2(Position.X, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X, Position.Y + 10), null, Color.White);

            // Air
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 18, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 18, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 18, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 18, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 18, Position.Y + 10), null, Color.White);

            // Food
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 36, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 36, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 36, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 36, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 36, Position.Y + 10), null, Color.White);

            // Air
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 54, Position.Y - 14), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 54, Position.Y - 8), null, Color.White);
            spriteBatch.Draw(ind_e, new Vector2(Position.X + 54, Position.Y - 2), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 54, Position.Y + 4), null, Color.White);
            spriteBatch.Draw(ind_f, new Vector2(Position.X + 54, Position.Y + 10), null, Color.White);

            // Name
            spriteBatch.DrawStringShadowed(AssetManager.ControlFont, asteroid.Name, new Vector2(Position.X, Position.Y + 20), Theme.BEIGE_LIGHT, Theme.BEIGE_DARK);
        }
    }
}
