using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Microcosm
{
    public class Tilemap
    {
        private float   TileSize;
        private int     Width, Length;
        private int[,]  map;

        public VertexPositionColorNormal[] vertices;
        public int verticescount, tileVertices, gridVertices;

        //public List<VertexPositionColorNormal> tileList;
        //public List<VertexPositionColor> gridLines;

        public bool IsDirty
        {
            get;
            set;
        }
        
        public Tilemap(int Width, int Length, float TileSize, double Seed)
        {
            this.Width    = Width;
            this.Length   = Length;
            this.TileSize = TileSize;
            
            map = new int[Length, Width];

            tileVertices = Length * Width * 6;
            gridVertices = Length * Width * 4;

            vertices = new VertexPositionColorNormal[tileVertices + gridVertices];

            GenerateMap(Seed);
            SetUpGrid();
        }

        private void GenerateMap(double Seed)
        {
            for (int r = 0; r < Length; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    double res = Gametek.Monogame.Math.Noise.GetNoise(r * 0.03, c * 0.03, Seed);
                    //map[r, c] = (res > .3F) ? 1 : 0;
                    map[r, c] = (int)MathHelper.Clamp((float)res * 10F, 0F, 10F);

                    Tile t = new Tile(new Vector3(c, 0, r), TileSize, map[r,c]);
                    foreach(var v in t.Vertices)
                    {
                        vertices[verticescount] = v;
                        verticescount++;
                    }
                }
            }           

            IsDirty = true;
        }

        private void SetUpGrid()
        {
            for (int r = 0; r < Length + 1; r++)
            {
                for (int c = 0; c < Width + 1; c++)
                {
                    vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, r) * TileSize, Color.LightGray, Vector3.Up);
                    verticescount++;
                    vertices[verticescount] = new VertexPositionColorNormal(new Vector3(Width, 0, r) * TileSize, Color.LightGray, Vector3.Up);
                    verticescount++;
                    vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, r) * TileSize, Color.LightGray,Vector3.Up);
                    verticescount++;
                    vertices[verticescount] = new VertexPositionColorNormal(new Vector3(c, 0, Length) * TileSize, Color.LightGray, Vector3.Up);

                    //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, r) * TileSize, Color.LightGray));         // X
                    //gridLines.Add(new VertexPositionColor(new Vector3(Width, 0, r) * TileSize, Color.LightGray));     // X

                    //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, r) * TileSize, Color.LightGray));         // Z
                    //gridLines.Add(new VertexPositionColor(new Vector3(c, 0, Length) * TileSize, Color.LightGray));    // Z
                }
            }
        }

        public void SetTile(Vector2 Position)
        {
            //Point res = VectorToTilePos(Position);

            //if(res.Y >= 0 && res.Y <= Length && res.X >= 0 && res.X <= Width)
            //    map[res.Y, res.X] = 2;

            //SetUpVertices();
        }

        private Vector3 VectorToTilePos(Vector3 Position)
        {
            var tmp = Position / TileSize;

            int X = (int)Math.Floor(tmp.X);
            int Z = (int)Math.Floor(tmp.Z);

            //Debug.WriteLine("Tile {0} {1}", X, Z);

            return new Vector3(X,0,Z);
        }

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
