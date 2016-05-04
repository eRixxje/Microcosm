using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.Helper
{
    public class Axes
    {
        BasicEffect basicEffect;
        VertexPositionColor[] lines;

        public Axes()
        {          

        }

        private void SetLineVerticles()
        {
            lines = new VertexPositionColor[6];

            lines[0] = new VertexPositionColor(new Vector3(-20, 0, 0), Color.Red);      // X
            lines[1] = new VertexPositionColor(new Vector3(20, 0, 0), Color.Red);       // X

            lines[2] = new VertexPositionColor(new Vector3(0, -20, 0), Color.Green);    // Y
            lines[3] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Green);     // Y

            lines[4] = new VertexPositionColor(new Vector3(0, 0, -20), Color.Blue);     // Z
            lines[5] = new VertexPositionColor(new Vector3(0, 0, 20), Color.Blue);      // Z
        }

        public void LoadContent()
        {
            basicEffect = new BasicEffect(ScreenManager.GraphicsDevice);

            SetLineVerticles();
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            basicEffect.World = Matrix.Identity;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = true;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                ScreenManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lines, 0, 3);
            }
        }
    }
}
