using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Microcosm
{
    public sealed class Grid
    {
        List<Rectangle> gridlist = new List<Rectangle>();

        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int CellSize { get; private set; }
        public int OffSet { get; private set; }
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Grid(int Columns, int Rows, int CellSize, int OffSet)
        {
            this.Columns  = Columns;
            this.Rows     = Rows;
            this.CellSize = CellSize + 1;
            this.OffSet   = OffSet;

            // Calculate
            Width  = (Columns * CellSize) + (Columns + 1);
            Height = (Rows * CellSize) + (Rows + 1);
        }

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            BuildGrid();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Rectangle rect in gridlist)
            {  
                //if (i % 2 != 0)
                spriteBatch.Draw(Theme.GRID_LINE, rect, Color.White);
            }
        }

        private void BuildGrid()
        {
            for (int x = 0; x <= Columns; x++)
            {
                Rectangle lineX = new Rectangle((x * CellSize) + OffSet, OffSet, 1, Height);
                gridlist.Add(lineX);
            }

            for (int y = 0; y <= Rows; y++)
            {
                Rectangle lineY = new Rectangle(OffSet, (y * (CellSize)) + OffSet, Width, 1);
                gridlist.Add(lineY);
            }
        }
    }
}
