using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gametek.Monogame;
using Gametek.Monogame.Managers;
using Gametek.Monogame.Input;

namespace Microcosm.Screens
{
    public sealed class MapScreen : GameScreen
    {
        private Camera camera;       
        private Matrix world = Matrix.Identity;
        private BasicEffect basicEffect;
        private Tilemap map;
        private VertexBuffer mapVertexBuffer;
        //private IndexBuffer mapIndexBuffer;

        private bool drawGrid;

        public MapScreen(bool IsActive) : base(IsActive)
        {
            camera = new Camera(new Vector3(10, 12, 10), new Vector3(0, 0, 0));
        }

        public override void LoadContent()
        {
            camera.LoadContent();

            basicEffect = new BasicEffect(ScreenManager.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;

            // Lighting
            basicEffect.LightingEnabled = false;
            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            basicEffect.DirectionalLight0.Direction = new Vector3(0, 1, 0);
            basicEffect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);
            basicEffect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
            basicEffect.EmissiveColor = new Vector3(1, 0, 0);

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rasterizerState;

            map = new Tilemap(100, 100, .5f, 112334);

            mapVertexBuffer = new VertexBuffer(ScreenManager.GraphicsDevice, 
                VertexPositionColorNormal.vertexDeclaration, map.vertices.Length, BufferUsage.WriteOnly);
            //mapIndexBuffer = new IndexBuffer(ScreenManager.GraphicsDevice,
            //    typeof(int), map.verticescount, BufferUsage.WriteOnly);
        }
        public override void UnloadContent()
        {
            //ScreenManager.game.Content.Unload();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            if (InputManager.IsKeyPress(Keys.F1))
                drawGrid = !drawGrid;
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
                    mapVertexBuffer.SetData(map.vertices);
                    //mapIndexBuffer.SetData(verticescount);


                    //ScreenManager.GraphicsDevice.Indices = mapIndexBuffer;
                    ScreenManager.GraphicsDevice.SetVertexBuffer(mapVertexBuffer);
                    map.IsDirty = false;
                }

                //ScreenManager.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, map.verticescount);

                ScreenManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, mapVertexBuffer.VertexCount/3);
                ScreenManager.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, map.tileVertices, map.gridVertices/2);

                //ScreenManager.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, cityVertexBuffer.VertexCount, linecount);
                //ScreenManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, map.tileList.ToArray(), 0, map.tileList.Count/3, 
                //    VertexPositionColorNormal.vertexDeclaration);
                
                //if (drawGrid)
                //    ScreenManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, map.gridLines.ToArray(), 0, map.gridLines.Count / 2);
            }

            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "FPS: " + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(10, 10), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            ScreenManager.spriteBatch.DrawString(FontManager.ControlFont, "CAM: " + camera.cameraPositionString, new Vector2(10, 30), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
