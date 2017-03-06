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

        private cube Cube;

        public MapScreen()
        {
            IsVisible = true;
            camera = new Camera(new Vector3(20, 22, 20), new Vector3(3, 0, 3));

            KeyListener.KeyTyped += KeyListener_KeyTyped;

            MouseListener.MouseDrag += MouseListener_MouseDrag;
            MouseListener.MouseWheelMoved += MouseListener_MouseWheelMoved;
        }

        private void MouseListener_MouseWheelMoved(object sender, MouseEventArgs e)
        {
            if (e.ScrollWheelDelta > 0)
                camera.Zoom(CameraZoom.Out);
            else
                camera.Zoom(CameraZoom.In);
        }
        private void MouseListener_MouseDrag(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButton.Right)
                camera.Drag(e.CurrentState.Position, e.PreviousState.Position);

            if(e.Button == MouseButton.Middle)
            {
                if (e.DistanceMoved.X > 0) // || e.DistanceMoved.Y > 0)
                    camera.Arc(CameraMovement.Right);
                
                if(e.DistanceMoved.X < 0) // || e.DistanceMoved.Y < 0)
                    camera.Arc(CameraMovement.Left);
            }
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
            KeyListener.KeyTyped -= KeyListener_KeyTyped;
            MouseListener.MouseDrag -= MouseListener_MouseDrag;
            MouseListener.MouseWheelMoved -= MouseListener_MouseWheelMoved;
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
            _spriteBatch.DrawString(FontManager.ControlFont, "Mouse: " + MouseListener.Position,
                new Vector2(10, 100), Color.LightGreen, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();
        }
    }
}