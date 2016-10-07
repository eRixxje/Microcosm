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
    }
}
