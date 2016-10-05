using Microsoft.Xna.Framework;

namespace Gametek.Monogame.Primitives
{
    public struct Circle
    {
        public Vector2 Center => Position + new Vector2(Radius);

        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }
        public bool Intersects(Circle other)
        {
            var distance = (other.Center - Center).Length();
            return (distance < (other.Radius + Radius));
        }
    }
}
