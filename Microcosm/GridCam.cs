using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Microcosm
{
    public class GridCam
    {
        public readonly Viewport ViewPort;

        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public float Zoom { get; private set; }
        public Vector2 Origin { get; private set; }

        private float _minimumZoom;
        public float MinimumZoom
        {
            get { return _minimumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MinimumZoom must be greater than zero");

                if (Zoom < value)
                    Zoom = MinimumZoom;

                _minimumZoom = value;
            }
        }

        private float _maximumZoom = float.MaxValue;
        public float MaximumZoom
        {
            get { return _maximumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MaximumZoom must be greater than zero");

                if (Zoom > value)
                    Zoom = value;

                _maximumZoom = value;
            }
        }

        public GridCam(Viewport viewport)
        {
            ViewPort = viewport;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        public void DoMove(Vector2 direction)
        {
            Position += Vector2.Transform(direction, Matrix.CreateRotationZ(-Rotation));
        }

        public void DoRotate(float deltaRadians)
        {
            Rotation += deltaRadians;
        }

        public void DoZoom(float deltaZoom)
        {
            ClampZoom(Zoom + deltaZoom);
        }

        //public void ZoomIn(float deltaZoom)
        //{
        //    ClampZoom(Zoom + deltaZoom);
        //}

        //public void ZoomOut(float deltaZoom)
        //{
        //    ClampZoom(Zoom - deltaZoom);
        //}

        private void ClampZoom(float value)
        {
            if (value < MinimumZoom)
                Zoom = MinimumZoom;
            else if (value > MaximumZoom)
                Zoom = MaximumZoom;
            else
                Zoom = value;

        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, GetViewMatrix());
        }
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(GetViewMatrix()));
        }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}
