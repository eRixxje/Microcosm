﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gametek.Monogame;
using Gametek.Monogame.Managers;
using System;
using Gametek.Monogame.UI.Helper;

namespace Microcosm.Screens
{
    public sealed class MapScreen : GameScreen
    {
        private Camera camera;       
        private Matrix world = Matrix.Identity;
        private BasicEffect basicEffect;
        private Tilemap map;

        private VertexBuffer mapVertexBuffer;
        private IndexBuffer  mapIndexBuffer;

        private string mouseposition;

        private cube Cube;

        public MapScreen(bool IsActive) : base(IsActive)
        {
            camera = new Camera(new Vector3(20, 22, 20), new Vector3(3, 0, 3));
        }

        public override void LoadContent()
        {
            camera.LoadContent();

            basicEffect = new BasicEffect(ScreenManager.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;

            // Lighting
            basicEffect.LightingEnabled = true;
            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            basicEffect.DirectionalLight0.Direction = new Vector3(0, 0, 0);
            basicEffect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);
            basicEffect.AmbientLightColor = new Vector3(1f, 1f, 1f);
            basicEffect.EmissiveColor = new Vector3(1, 0, 0);

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //ScreenManager.GraphicsDevice.RasterizerState = rasterizerState;

            map = new Tilemap(900, 900, .25f, 112334);

            mapVertexBuffer = new VertexBuffer(ScreenManager.GraphicsDevice, 
                VertexPositionColorNormal.vertexDeclaration, map.vertices.Length, BufferUsage.WriteOnly);
            mapIndexBuffer = new IndexBuffer(ScreenManager.GraphicsDevice,
                typeof(int), map.indices.Length, BufferUsage.WriteOnly);

            Cube = new cube();
        }
        public override void UnloadContent()
        {
            //ScreenManager.game.Content.Unload();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            if (InputManager.IsKeyPress(Keys.F1))
                map.drawGrid = !map.drawGrid;

            Vector3 pos = GeometryHelper.WorldPosition(InputManager.MousePosition, camera.projection, camera.view);
            mouseposition = string.Format("{0} {1}", Math.Floor(pos.X), Math.Floor(pos.Z));
        }
        public override void Draw(GameTime gameTime)
        {
            basicEffect.World = world;
            basicEffect.View = camera.view;
            basicEffect.Projection = camera.projection;

            camera.Draw(gameTime);
            

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                if (map.IsDirty)
                {
                    mapIndexBuffer.SetData(map.indices);
                    mapVertexBuffer.SetData(map.vertices);
                    map.IsDirty = false;
                }

                ScreenManager.GraphicsDevice.Indices = mapIndexBuffer;
                ScreenManager.GraphicsDevice.SetVertexBuffer(mapVertexBuffer);
                ScreenManager.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, map.vertices.Length / 2);

                if (map.drawGrid)
                    ScreenManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, map.grid, 0, map.grid.Length / 2);
            }

            Cube.Draw(camera, new Vector3(30,0,30));

            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "FPS: " + (1000 / gameTime.ElapsedGameTime.Milliseconds), 
                new Vector2(10, 10), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "CAM: " + camera.cameraPositionString, 
                new Vector2(10, 40), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "Target: " + camera.cameraTargetString,
                new Vector2(10, 70), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "Mouse: " + mouseposition,
                new Vector2(10, 100), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
