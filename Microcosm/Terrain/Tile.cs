using Gametek.Monogame;
using Microsoft.Xna.Framework;

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

        public VertexPositionColorNormal[] Vertices = new VertexPositionColorNormal[4];
        public int[] Indices = new int[6];

        public Tile(Vector3 Position, float Size, double Value)
        {
            this.Position = Position;
            this.Value    = (int)MathHelper.Clamp((float)Value * 10F, 0F, 10F);

            SetupVertices();

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Position += (Position*Size);
                Vertices[i].Color    = Tilemap.GetColor(this.Value);
                Vertices[i].Normal   = Vector3.Up;
            }
        }

        public void SetupVertices()
        {
            /*
             *  0 ---- 1
             *  |      |
             *  |      | 
             *  |      |
             *  2------3
             */
             
            Vertices[0].Position = new Vector3(0, 0, 0);
            Vertices[1].Position = new Vector3(1, 0, 0);
            Vertices[2].Position = new Vector3(0, 0, 1);
            Vertices[3].Position = new Vector3(1, 0, 1);

            Indices[0] = 2; Indices[1] = 0; Indices[2] = 1;
            Indices[3] = 1; Indices[4] = 3; Indices[5] = 2;
        }
    }
}
