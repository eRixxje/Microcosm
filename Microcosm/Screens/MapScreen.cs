using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame;
using Gametek.Monogame.Screen;
using Gametek.Monogame.Input;
using Gametek.Monogame.Helper;

namespace Microcosm.Screens
{
    public sealed class MapScreen : Screen
    {
        private Axes axes = new Axes();
        private bool drawAxes;

        private SpriteBatch _spriteBatch;

        private Camera camera;
        private Matrix world = Matrix.Identity;
        private BasicEffect basicEffect;
        private Tilemap map;

        private VertexBuffer mapVertexBuffer;
        private IndexBuffer mapIndexBuffer;

        private string mouseposition;

        private cube Cube;

        public MapScreen()
        {
            IsVisible = true;
            camera = new Camera(new Vector3(20, 22, 20), new Vector3(3, 0, 3));

            KeyListener.KeyPressed += KeyListener_KeyPressed;
            KeyListener.KeyTyped += KeyListener_KeyTyped;

            MouseListener.MouseDrag += MouseListener_MouseDrag;
        }

        private void MouseListener_MouseDrag(object sender, MouseEventArgs e)
        {
            // Drag
            if(e.Button == MouseButton.Right)
            {
                //camera.Position -= InputManager.GetMouseDragDelta(projection, view);
                //camera.Target -= InputManager.GetMouseDragDelta(projection, view);
            }

            //if (InputManager.MouseZoom == ScrollDirection.ZoomIn && Position.Y < maxZoom)
            //    Move(Movement.ZoomIn);
            //if (InputManager.MouseZoom == ScrollDirection.ZoomOut && Position.Y > minZoom)
            //    Move(Movement.ZoomOut);
        }

        private void KeyListener_KeyTyped(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Microsoft.Xna.Framework.Input.Keys.F1:
                    map.drawGrid = !map.drawGrid;
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F2:
                    drawAxes = !drawAxes;
                    break;
            }
        }

        private void KeyListener_KeyPressed(object sender, KeyboardEventArgs e)
        {
            // Keyboard input
            //if (InputManager.IsKeyDown(Keys.W) && InputManager.IsKeyDown(Keys.LeftShift))
            //    Move(Movement.Up);
            //if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.LeftShift))
            //    Move(Movement.Down);

            switch (e.Key)
            {
                case Microsoft.Xna.Framework.Input.Keys.W:
                    camera.Move(Movement.Forward);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.S:
                    camera.Move(Movement.Back);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.A:
                    camera.Move(Movement.Left);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D:
                    camera.Move(Movement.Right);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Z:
                    camera.Move(Movement.ArcLeft);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.X:
                    camera.Move(Movement.ArcRight);
                    break;
            }
        }

        public override void LoadContent()
        {
            axes.LoadContent();

            _spriteBatch = new SpriteBatch(GameEngine.GraphicsDeviceManager.GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDeviceManager.GraphicsDevice);
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

            map = new Tilemap(20, 20, 1f, 897876876);

            mapVertexBuffer = new VertexBuffer(GraphicsDeviceManager.GraphicsDevice, 
                VertexPositionColorNormal.vertexDeclaration, map.vertices.Length, BufferUsage.WriteOnly);
            mapIndexBuffer = new IndexBuffer(GraphicsDeviceManager.GraphicsDevice,
                IndexElementSize.ThirtyTwoBits, map.indices.Length, BufferUsage.WriteOnly);

            Cube = new cube();
        }
        public override void UnloadContent()
        {
            KeyListener.KeyPressed -= KeyListener_KeyPressed;

            //ScreenManager.game.Content.Unload();
        }
        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);

            //Vector3 pos = GeometryHelper.WorldPosition(InputManager.MousePosition, camera.projection, camera.view);
            //mouseposition = string.Format("{0} {1}", Math.Floor(pos.X), Math.Floor(pos.Z));
        }
        public override void Draw(GameTime gameTime)
        {
            GameEngine.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);

            basicEffect.World = world;
            basicEffect.View = camera.view;
            basicEffect.Projection = camera.projection;

            _spriteBatch.Begin();

            if (drawAxes)
                axes.Draw(gameTime, camera.view, camera.projection);


            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                if (map.IsDirty)
                {
                    mapIndexBuffer.SetData(map.indices);
                    mapVertexBuffer.SetData(map.vertices);
                    map.IsDirty = false;
                }

                GraphicsDeviceManager.GraphicsDevice.Indices = mapIndexBuffer;
                GraphicsDeviceManager.GraphicsDevice.SetVertexBuffer(mapVertexBuffer);
                GraphicsDeviceManager.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, map.vertices.Length / 2);

                if (map.drawGrid)
                    GraphicsDeviceManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, map.grid, 0, map.grid.Length / 2);
            }

            Cube.Draw(camera, new Vector3(30, 0, 30));

           

            camera.Draw(gameTime, _spriteBatch);

            _spriteBatch.DrawString(FontManager.ControlFont, "FPS: " + (1000 / gameTime.ElapsedGameTime.Milliseconds), 
                new Vector2(10, 10), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.DrawString(FontManager.ControlFont, "CAM: " + camera.cameraPositionString, 
                new Vector2(10, 40), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.DrawString(FontManager.ControlFont, "Target: " + camera.cameraTargetString,
                new Vector2(10, 70), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.DrawString(FontManager.ControlFont, "Mouse: " + mouseposition,
                new Vector2(10, 100), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();
        }
    }
}