using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public class Tile
    {
        public Vector3 Position
        {
            get;
            private set;
        }
        public int Value
        {
            get;
            private set;
        }

        public bool CanBuild
        {
            get { return Value > 0; }
        }

        public VertexPositionColorNormal[] Vertices = new VertexPositionColorNormal[6];

        public Tile(Vector3 Position, float Size, int Value)
        {
            this.Position = Position;
            this.Value    = Value;

            SetupVertices();

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Position += (Position*Size);
                Vertices[i].Color    = Tilemap.GetColor(Value);
                Vertices[i].Normal   = Vector3.Up;
            }
        }

        public void SetupVertices()
        {
            Vertices[0].Position = new Vector3(0, 0, 0);
            Vertices[1].Position = new Vector3(1, 0, 0);
            Vertices[2].Position = new Vector3(0, 0, 1);

            Vertices[3].Position = new Vector3(0, 0, 1);
            Vertices[4].Position = new Vector3(1, 0, 0);
            Vertices[5].Position = new Vector3(1, 0, 1);
        }
    }
}
