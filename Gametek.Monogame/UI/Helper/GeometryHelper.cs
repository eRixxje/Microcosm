using Gametek.Monogame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gametek.Monogame.UI.Helper
{
    public static class GeometryHelper
    {
        public static Texture2D GetRectangle(Vector2 size, Color BackgroundColor)
        {
            Texture2D rect = new Texture2D(ScreenManager.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[(int)size.X * (int)size.Y];

            for (int i = 0; i < data.Length; ++i) data[i] = BackgroundColor;
            rect.SetData(data);

            return rect;
        }

        public static Vector3 WorldPosition(Point MouseLocation, Matrix projection, Matrix view)
        {
            Vector3 nearPoint = ScreenManager.Viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 0.0f), projection, view, Matrix.Identity);
            Vector3 farPoint = ScreenManager.Viewport.Unproject(new Vector3(MouseLocation.X, MouseLocation.Y, 1.0f), projection, view, Matrix.Identity);
            Vector3 direction = Vector3.Normalize(farPoint - nearPoint);

            return (nearPoint - direction * (nearPoint.Y / direction.Y));
        }

        //public static Vector3 SelectedVector3(Point mouseLocation, Matrix projection, Matrix view)
        //{
        //    Vector3 nearPoint = ScreenManager.Viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 0.0f), projection, view, Matrix.Identity);
        //    Vector3 farPoint = ScreenManager.Viewport.Unproject(new Vector3(mouseLocation.X, mouseLocation.Y, 1.0f), projection, view, Matrix.Identity);
        //    Vector3 direction = Vector3.Normalize(farPoint - nearPoint);

        //    Plane p = new Plane(0, -1, 0, 0);
        //    Ray MouseRay = new Ray(nearPoint, direction);

        //    // calculate distance of intersection point from r.origin
        //    float denominator = Vector3.Dot(p.Normal, MouseRay.Direction);
        //    float numerator = Vector3.Dot(p.Normal, MouseRay.Position) + p.D;
        //    float t = -(numerator / denominator);

        //    Vector3 pickedPosition = nearPoint + direction * t;

        //    return pickedPosition;
        //}
    }
}
