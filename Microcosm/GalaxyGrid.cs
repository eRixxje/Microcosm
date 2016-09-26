using Gametek.Monogame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public sealed class GalaxyGrid
    {
        public Point Position { get; private set; }
        public Point Size { get; private set; }
        public int CellSize { get; private set; }

        private Texture2D hblock;
        private Texture2D vblock;
        private Texture2D Border;

        public GalaxyGrid(Point position, Point size, int cellSize)
        {
            Position = position;
            Size     = size;
            CellSize = cellSize;
        }

        public void LoadContent()
        {
            vblock = Geometry.Rectangle(new Vector2(3, 20), Theme.BEIGE_LIGHT);
            hblock = Geometry.Rectangle(new Vector2(20, 3), Theme.BEIGE_LIGHT);
            Border = Geometry.Border(Size.ToVector2(), 1, Theme.BEIGE_MEDIUM);

            BuildGrid();
        }

        public void Draw(GameTime gameTime, SpriteBatchEx spriteBatch)
        {
            // Map Borders
            spriteBatch.Draw(Border, Position.ToVector2());

            // Map Anchors
            spriteBatch.Draw(vblock, new Vector2(Position.X - 1, Position.Y - 1));
            spriteBatch.Draw(hblock, new Vector2(Position.X - 1, Position.Y - 1));
            spriteBatch.Draw(vblock, new Vector2(Size.X + Position.X - 2, Position.Y - 1));
            spriteBatch.Draw(hblock, new Vector2(Size.X + Position.X - 19, Position.Y - 1));
            spriteBatch.Draw(vblock, new Vector2(Position.X - 1, Size.Y + Position.Y - 19));
            spriteBatch.Draw(hblock, new Vector2(Position.X - 1, Size.Y + Position.Y - 2));
            spriteBatch.Draw(vblock, new Vector2(Position.X + Size.X - 2, Position.Y + Size.Y - 19));
            spriteBatch.Draw(hblock, new Vector2(Position.X + Size.X - 19, Position.Y + Size.Y - 2));
        }
        private void BuildGrid()
        {

        }
    }
}
