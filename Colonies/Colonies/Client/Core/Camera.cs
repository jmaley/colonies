// Title: camera.cs
// Author: Joe Maley
// Date: 6-16-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Colonies.Client.Core
{
    class Camera
    {
        private InputManager inputManager;

        private Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        private float rotation = 0.0f;
        private float zoom = 20.0f;

        private Matrix view = Matrix.Identity;
        private Matrix projection = Matrix.Identity;

        private BoundingFrustum frustum;

        public Vector3 Position { set { position = value; } get { return position; } }
        public float Rotation { set { rotation = value; } get { return rotation; } }
        public float Zoom { set { zoom = value; } get { return zoom; } }

        public Matrix View { get { return view; } }
        public Matrix Projection { get { return projection; } }
        public BoundingFrustum Frustum { get { return frustum; } }

        public Camera()
        {
            inputManager = InputManager.GetInstance();

            frustum = new BoundingFrustum(view * projection);

            UpdateProjection();
        }

        public void HandleInput()
        {
            float speed = 0.1f;
            if (inputManager.IsKeyDown(Keys.Q))
            {
                rotation += 0.002f;
            }
            if (inputManager.IsKeyDown(Keys.D))
            {
                position.X += speed * (float)Math.Cos(rotation);
                position.Z -= speed * (float)Math.Sin(rotation);
            }
            if (inputManager.IsKeyDown(Keys.S))
            {
                position.Z += speed * (float)Math.Cos(rotation);
                position.X += speed * (float)Math.Sin(rotation);
            }
            if (inputManager.IsKeyDown(Keys.W))
            {
                position.Z -= speed * (float)Math.Cos(rotation);
                position.X -= speed * (float)Math.Sin(rotation);
            }
            if (inputManager.IsKeyDown(Keys.E))
            {
                rotation -= 0.002f;
            }
            if (inputManager.IsKeyDown(Keys.A))
            {
                position.X -= speed * (float)Math.Cos(rotation);
                position.Z += speed * (float)Math.Sin(rotation);
            }

            UpdateView();
        }

        private void UpdateView()
        {
            view = Matrix.CreateLookAt(Vector3.Transform(new Vector3(0, zoom, zoom), Matrix.CreateRotationY(rotation)) + position, Vector3.Zero + position, Vector3.Up);
            frustum.Matrix = view * projection;
        }

        public void UpdateProjection()
        {
            projection = Matrix.CreateOrthographic(SettingsManager.GetInstance().ResolutionX / 100.0f, SettingsManager.GetInstance().ResolutionY / 100.0f, 1.0f, 100.0f);
        }
    }
}
