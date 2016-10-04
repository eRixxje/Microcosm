using System;
using System.Collections.Generic;
using Gametek.Monogame;
using Gametek.Monogame.Manager;
using Gametek.Monogame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Microcosm
{
    public sealed class GalaxyGrid : UIElement
    {     
        public int CellSize { get; private set; }
        public Viewport Viewport { get; private set; }

        List<Rectangle> gridlist;

        public Galaxy Galaxy { get; private set; }

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
            Galaxy = new Galaxy();
        }

        public void LoadContent()
        {
            vblock = Geometry.Rectangle(RenderManager.GraphicsDevice, new Vector2(3, 20), Theme.BEIGE_LIGHT);
            hblock = Geometry.Rectangle(RenderManager.GraphicsDevice, new Vector2(20, 3), Theme.BEIGE_LIGHT);
            Border = Geometry.Border(RenderManager.GraphicsDevice, Size, 1, Theme.BEIGE_MEDIUM);

            Galaxy.LoadContent();
        }

        public override void Update(GameTime gameTime)
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
            foreach (var a in Galaxy.render)
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
