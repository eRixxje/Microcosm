using System;
using System.Collections.Generic;
using Gametek.Monogame;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame.Manager;
using System.Linq;
using Microcosm.Universe;

namespace Microcosm.UI
{
    public sealed class GalaxyGrid : UIElement
    {
        public int CellSize { get; private set; }
        public Viewport Viewport { get; private set; }

        List<Rectangle> gridlist;

        public GridCam Camera;

        private Texture2D hblock;
        private Texture2D vblock;
        private Texture2D Border;

        public GalaxyGrid(Vector2 position, Vector2 size, int cellSize)
        {
            Position = position;
            Size     = size;
            CellSize = cellSize;
            Viewport = new Viewport((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

            Camera = new GridCam(Viewport);
        }

        public void LoadContent()
        {
            vblock = Geometry.Rectangle(RenderManager.GraphicsDevice, new Vector2(3, 20), Theme.BEIGE_LIGHT);
            hblock = Geometry.Rectangle(RenderManager.GraphicsDevice, new Vector2(20, 3), Theme.BEIGE_LIGHT);
            Border = Geometry.Border(RenderManager.GraphicsDevice, Size, 1, Theme.BEIGE_MEDIUM);
        }

        public override void Update(GameTime gameTime)
        {
            BuildGrid();

            Vector2 worldPosition = GridCam.ScreenToWorld(Microcosm.Input.MousePosition.ToVector2(), Camera.GetViewMatrix());
            for (int i = 0; i < Microcosm.Galaxy.Asteroids.Count; i++)
            {
                // Selection
                if (Microcosm.Galaxy.Asteroids[i].Bounds.Contains(worldPosition))
                {
                    if (Microcosm.Input.IsMouseClicked(MouseButton.LeftButton))
                        Microcosm.Galaxy.Asteroids[i].IsSelected = true;
                    else
                        Microcosm.Galaxy.Asteroids[i].IsSelected = false;
                }

                // Build RenderList
                Vector2 astPosition = GridCam.WorldToScreen(Microcosm.Galaxy.Asteroids[i].Position, Camera.GetViewMatrix());
                if (Camera.ViewPort.Bounds.Contains(astPosition))
                    Microcosm.Galaxy.Asteroids[i].IsVisible = true;
                else
                    Microcosm.Galaxy.Asteroids[i].IsVisible = false;
            }

            if (Microcosm.Input.IsMouseClicked(MouseButton.RightButton))
            {
                Asteroid na = new Asteroid(GridCam.ScreenToWorld(Microcosm.Input.MousePosition.ToVector2(), Camera.GetViewMatrix()), Asteroid.GetDirection(), Asteroid.GetSize());
                na.LoadContent();
                Microcosm.Galaxy.Asteroids.Add(na);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Grid
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());
            foreach (Rectangle rect in gridlist)
            {
                spriteBatch.Draw(Theme.GRID_LINE, rect, Color.White);
                //spriteBatch.DrawString(AssetManager.ControlFont, string.Format("{0}", rect.X), rect.Location.ToVector2(), Color.White);
            }
            
            // Galaxy
            foreach (var a in Microcosm.Galaxy.Asteroids.Where(a => a.IsVisible))
            {
                a.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            // Control Decorations
            spriteBatch.Begin();
            
            // Map Borders
            spriteBatch.Draw(Border, Position);

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

            base.Draw(gameTime, spriteBatch);
        }

        public void Move(Vector2 direction)
        {
            Camera.DoMove(direction);
        }
        public void Zoom(float amount)
        {
            Camera.DoZoom(amount);
        }

        private void BuildGrid()
        {
            gridlist = new List<Rectangle>();

            int Cols = Viewport.Width / CellSize;
            int Rows = Viewport.Height / CellSize;

            int ratioX = (int)Math.Ceiling(Camera.Position.X / CellSize);
            int ratioY = (int)Math.Ceiling(Camera.Position.Y / CellSize);

            for (int x = 0; x < Cols; x++)
            {
                Rectangle lineX = new Rectangle(
                                (int)Position.X + ((x + ratioX) * CellSize), // X
                                (int)Position.Y + (int)Camera.Position.Y,    // Y
                                1,                                      // Width
                                (int)Size.Y);                                // Height

                gridlist.Add(lineX);
            }

            for (int y = 0; y < Rows; y++)
            {
                Rectangle lineY = new Rectangle(
                                (int)Position.X + (int)Camera.Position.X,      // X
                                ((y + ratioY) * CellSize) + (int)Position.Y,   // Y
                                (int)Size.X,                                   // Width
                                1);                                       // Height
                gridlist.Add(lineY);
            }
        }
    }
}
