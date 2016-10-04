using Microsoft.Xna.Framework;

namespace Gametek.Monogame.Primitives
{
    public struct Circle
    {
        public Vector2 AbsCenter => Position + Center;

        public Vector2 Position { get; set; }
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 position, Vector2 center, float radius)
        {
            Position = position;
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return ((point - AbsCenter).Length() <= Radius);
        }

        public bool Intersects(Circle other)
        {
            var distance = (other.AbsCenter - AbsCenter).Length();
            return (distance < (other.Radius + Radius));
        }
    }

}
