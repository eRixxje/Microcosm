using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microcosm
{
    public sealed class GalaxyGrid
    {
        public Point Position { get; private set; }
        public Point Size { get; private set; }
        public int CellSize { get; private set; }
        public Viewport Viewport { get; private set; }

        List<Rectangle> gridlist;

        public Galaxy Galaxy { get; private set; }

        public GridCam Camera;

        private Texture2D hblock;
        private Texture2D vblock;
        private Texture2D Border;

        public GalaxyGrid(Point position, Point size, int cellSize)
        {
            Position = position;
            Size     = size;
            CellSize = cellSize;
            Viewport = new Viewport(Position.X, Position.Y, Size.X, Size.Y);

            Camera = new GridCam(Viewport);
            Galaxy = new Galaxy();
        }

        public void LoadContent()
        {
            vblock = Geometry.Rectangle(new Vector2(3, 20), Theme.BEIGE_LIGHT);
            hblock = Geometry.Rectangle(new Vector2(20, 3), Theme.BEIGE_LIGHT);
            Border = Geometry.Border(Size.ToVector2(), 1, Theme.BEIGE_MEDIUM);

            Galaxy.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Galaxy.Update(gameTime, Camera);

            BuildGrid();

            // Move
            if (InputManager.IsKeyDown(Keys.W))
                Camera.Move(new Vector2(0, -250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.S))
                Camera.Move(new Vector2(0, 250) * deltaTime);

            if (InputManager.IsKeyDown(Keys.A))
                Camera.Move(new Vector2(-250, 0) * deltaTime);

            if (InputManager.IsKeyDown(Keys.D))
                Camera.Move(new Vector2(250, 0) * deltaTime);

            if (InputManager.MouseZoom == ScrollDirection.ZoomOut)
                Camera.ZoomOut(0.2f);
            if (InputManager.MouseZoom == ScrollDirection.ZoomIn)
                Camera.ZoomIn(0.2f);
        }

        public void Draw(GameTime gameTime, SpriteBatchEx spriteBatch)
        {
            // Grid
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());
            foreach (Rectangle rect in gridlist)
            {
                spriteBatch.Draw(Theme.GRID_LINE, rect, Color.White);
            }
            
            // Galaxy
            foreach (var a in Galaxy.render)
            {
                a.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            // Control Decorations
            spriteBatch.Begin();
            
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
            spriteBatch.End();
        }

        private void BuildGrid()
        {
            gridlist = new List<Rectangle>();

            int Cols = Viewport.Width / CellSize;
            int Rows = Viewport.Height / CellSize;

            for (int x = 0; x <= Cols; x++)
            {
                Rectangle lineX = new Rectangle(
                                (x * CellSize) + Position.X,            // X
                                Position.Y + (int)Camera.Position.Y,    // Y
                                1,                                      // Height
                                Size.Y);                                // Width

                gridlist.Add(lineX);
            }

            for (int y = 0; y <= Rows; y++)
            {
                Rectangle lineY = new Rectangle(
                                Position.X + (int)Camera.Position.X,    // X
                                (y * (CellSize)) + Position.Y,          // Y
                                Size.X,                                 // Height
                                1);                                     // Width
                gridlist.Add(lineY);
            }
        }
    }
}
