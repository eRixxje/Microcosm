using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Microcosm.Universe
{
    public sealed class AsteroidList : List<Asteroid>
    {
        public new void Add(Asteroid item)
        {
            item.LoadContent();
            base.Add(item);
        }

        public new void Remove(Asteroid item)
        {
            item.UnloadContent();
            base.Remove(item);
        }

        public Asteroid GetAtPosition(Vector2 position)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Bounds.Contains(position))
                    return this[i];
            }

            return null;
        }
    }
}
