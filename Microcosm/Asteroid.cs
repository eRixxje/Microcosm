using Gametek.Monogame;
using Gametek.Monogame.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public sealed class Asteroid
    {
        public bool Selected { get; private set; }

        public string Name { get; private set; }

        public int Size { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }


        public Texture2D Texture
        {
            get
            {
                return Theme.GetAsteroidTexture(Size, Selected);
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }       

        public string PositionString
        {
            get
            {
                return string.Format("X: {0:0.0}, Y: {1:0.0}", Position.X, Position.Y);
            }
        }
        public string DirectionString
        {
            get
            {
                return string.Format("X: {0}, Y: {1}", Direction.X, Direction.Y);
            }
        }

        public byte Power { get; private set; }
        public byte Air { get; private set; }
        public byte Food { get; private set; }
        public byte Water { get; private set; }

        public Asteroid(Vector2 Position, Vector2 Direction, int Size)
        {
            this.Position  = Position;
            this.Direction = Direction;
            this.Size      = Size;

            this.Name = GetName();
        }

        public void Update(GameTime gameTime)
        {
            Position = Vector2.Add(Position, Direction);          
        }
        public void Draw(GameTime gameTime, SpriteBatchEx spriteBatch)
        { 
            spriteBatch.Draw(Texture, BoundingBox, Color.White);

            AsteroidIndicator.Draw(spriteBatch, this);
        }
        
        public void SetSelected(bool Value)
        {
            Selected = Value;
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
    }
}
