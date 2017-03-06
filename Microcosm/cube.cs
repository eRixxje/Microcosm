using Gametek.Monogame.Managers;
using Gametek.Monogame.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public class cube
    {
        private Model model;
        //private BasicEffect effect;

        public cube()
        {
            ModelManager.Add("Models/cube");
            model = ModelManager.Get("Models/cube");
        }

        public void Draw(Camera cam, Vector3 Position)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
                    effect.DirectionalLight0.Direction = new Vector3(0, 0, 0);
                    effect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);
                    //effect.AmbientLightColor = new Vector3(1f, 1f, 1f);
                    //effect.EmissiveColor = new Vector3(1, 0, 0);
                    effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;

                    effect.World = GetWorldMatrix();
                    effect.View = cam.view; //Matrix.CreateLookAt(cam.Position, Vector3.Zero, Vector3.UnitZ);
                    effect.Projection = cam.projection; //Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ScreenManager.GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio, 1f, 1000f);
                }

                mesh.Draw();
            }
        }

        Matrix GetWorldMatrix()
        {
            //const float circleRadius = 8;
            //const float heightOffGround = 3;

            // this matrix moves the model "out" from the origin
            Matrix translationMatrix = Matrix.CreateScale(0.010f) * Matrix.CreateTranslation(new Vector3(0,0,0)); // Matrix.CreateTranslation(circleRadius, 0, heightOffGround);

            // this matrix rotates everything around the origin
            Matrix rotationMatrix = Matrix.CreateRotationZ(0);

            // We combine the two to have the model move in a circle:
            Matrix combined = translationMatrix * rotationMatrix;

            return combined;
        }
    }
}
