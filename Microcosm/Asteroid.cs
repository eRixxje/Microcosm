using Gametek.Monogame;
using Gametek.Monogame.Primitives;
using Gametek.Monogame.UI;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public sealed class Asteroid : UIElement
    {
        public string Name { get; private set; }
        public int SizeClass { get; private set; }
        public byte Power { get; private set; }
        public byte Air { get; private set; }
        public byte Food { get; private set; }
        public byte Water { get; private set; }

        public Vector2 Direction { get; private set; }
        public Texture2D Texture { get; private set; }

        public new Circle Bounds;

        private AsteroidIndicator indicator;

        public bool IsCollided
        {
            get;
            private set;
        }

        public Asteroid(Vector2 position, Vector2 direction, int asteroidSize)
        {
            this.Position  = position;
            this.Direction = direction;
            this.SizeClass = asteroidSize;
        }

        public void LoadContent()
        {
            this.Name    = GetName();
            this.Texture = GetAsteroidTexture(SizeClass);
            this.Bounds  = new Circle(Position, new Vector2(Texture.Width/2), Texture.Width/2);

            indicator = new AsteroidIndicator(this);
        }

        public override void Update(GameTime gameTime)
        {
            Position = Vector2.Add(Position, Direction);
            Bounds.Position = Position;

            indicator.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {           
            if (IsSelected)
                spriteBatch.Draw(Texture, Position, Theme.BLUE_MEDIUM);
            else
                spriteBatch.Draw(Texture, Position, Theme.BLUE_LIGHT);

            //spriteBatch.DrawCircle(Bounds, 12, Color.Red);
            indicator.Draw(gameTime, spriteBatch);

            base.Draw(gameTime, spriteBatch);
        }

        public void SetCollided(bool Value)
        {
            IsCollided = Value;
        }


        public static Vector2 GetDirection()
        {
            float X = Rand.NextSigned(.025f);
            float Y = Rand.NextSigned(.025f);

            return new Vector2(X, Y);
        }
        public static int GetSize()
        {
            return Rand.Next(3);
        }
        public static string GetName()
        {
            return string.Format("{0}-{1}-{2}", Rand.GetNumbers(3), Rand.GetChars(3), Rand.GetRandomNumbers(2));
        }
        public static Texture2D GetAsteroidTexture(int Size)
        {
            switch (Size)
            {
                case 0:
                    return Theme.ASTEROID_S;
                case 1:
                    return Theme.ASTEROID_M;
                case 2:
                    return Theme.ASTEROID_L;
                default:
                    return Theme.ASTEROID_S;
            }
        }
    }
}
