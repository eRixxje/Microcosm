using Gametek.Monogame;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Microcosm
{
    public class Tilemap
    {
        private float   TileSize;
        private int     Width, Length;

        private List<Tile> map;

        public VertexPositionColorNormal[] vertices;
        public int[] indices;

        public bool IsDirty
        {
            get;
            set;
        }
        
        public Tilemap(int Width, int Length, float TileSize, double Seed)
        {
            this.Width    = Width;      // Along Z-Axis
            this.Length   = Length;     // Along X-Axis
            this.TileSize = TileSize;

            map      = new List<Tile>();
            vertices = new VertexPositionColorNormal[Length * Width * 4];
            indices  = new int[Length * Width * 6];

            GenerateMap(Seed);
            //SetUpGrid();
        }

        private void GenerateMap(double Seed)
        {
            for (int z = 0; z < Width; z++)
            {
                for (int x = 0; x < Length; x++)
                {
                    double res = Gametek.Monogame.Math.Noise.GetNoise(z * 0.03, x * 0.03, Seed);
                    map.Add(new Tile(new Vector3(x, 0, z), TileSize, res));
                }
            }

            int vOffset = 0;
            int iOffset = 0;

            foreach(var t in map)
            {
                t.Vertices.CopyTo(vertices, vOffset);
                
                indices[iOffset + 0] = t.Indices[0] + vOffset;
                indices[iOffset + 1] = t.Indices[1] + vOffset;
                indices[iOffset + 2] = t.Indices[2] + vOffset;
                indices[iOffset + 3] = t.Indices[3] + vOffset;
                indices[iOffset + 4] = t.Indices[4] + vOffset;
                indices[iOffset + 5] = t.Indices[5] + vOffset;

                vOffset += 4;
                iOffset += 6;
            }

            IsDirty = true;
        }

        //private void SetUpGrid()
        //{
        //    for (int r = 0; r < Length + 1; r++)
        //    {
        //        for (int c = 0; c < Width + 1; c++)
        //        {
        //            vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, r) * TileSize, Color.LightGray, Vector3.Up);
        //            verticescount++;
        //            vertices[verticescount] = new VertexPositionColorNormal(new Vector3(Width, 0, r) * TileSize, Color.LightGray, Vector3.Up);
        //            verticescount++;
        //            vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, r) * TileSize, Color.LightGray,Vector3.Up);
        //            verticescount++;
        //            vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, Length) * TileSize, Color.LightGray, Vector3.Up);

        //            //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, r) * TileSize, Color.LightGray));         // X
        //            //gridLines.Add(new VertexPositionColor(new Vector3(Width, 0, r) * TileSize, Color.LightGray));     // X

        //            //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, r) * TileSize, Color.LightGray));         // Z
        //            //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, Length) * TileSize, Color.LightGray));    // Z
        //        }
        //    }
        //}

        //public void SetTile(Vector2 Position)
        //{
        //    //Point res = VectorToTilePos(Position);

        //    //if(res.Y >= 0 && res.Y <= Length && res.X >= 0 && res.X <= Width)
        //    //    map[res.Y, res.X] = 2;

        //    //SetUpVertices();
        //}

        //private Vector3 VectorToTilePos(Vector3 Position)
        //{
        //    var tmp = Position / TileSize;

        //    int X = (int)Math.Floor(tmp.X);
        //    int Z = (int)Math.Floor(tmp.Z);

        //    //Debug.WriteLine("Tile {0} {1}", X, Z);

        //    return new Vector3(X,0,Z);
        //}

        //public static Color GetColor(int colorid)
        //{
        //    switch (colorid)
        //    {
        //        case 0:
        //            return Color.Black;
        //        case 1:
        //            return Color.Gray;
        //        default:
        //            return Color.Purple;
        //    }
        //}

        public static Color GetColor(int colorid)
        {
            switch (colorid)
            {
                case 0:
                case 1:
                    return Color.DarkGreen;
                case 2:
                case 3:
                case 4:
                case 5:
                    return Color.Green;
                case 6:
                    return Color.Blue;
                case 7:
                    return Color.MediumBlue;
                default:
                    return Color.DarkBlue;
            }
        }
    }
}
