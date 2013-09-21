using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
using Colonies.Client.Core;

namespace Colonies.Client.UI.Widgets
{
    class ModelDisplayWidget : Widget
    {
        private Model myModel = AssetManager.GetInstance().getAsset<Model>("sporpion rotated");
        //private Model myModel = AssetManager.GetInstance().getAsset<Model>("GrassCube");
        private Matrix[] transforms;
        private float rotation = 0.01f;
        private Vector3 camPos = new Vector3(0.0f, -10.0f, 5.0f);
        private Vector3 translation = new Vector3(0, 0, 0);
        private Matrix  translationAndScale;
        private int flag = 0;
        private float increment = (float)Math.PI / 2.0f;
        private Camera camera;

        public ModelDisplayWidget(SpriteBatch spriteBatch, string bg, int p_3, int p_4, int p_5, int p_6, Anchor anchor_2) : base(spriteBatch, bg, p_3, p_4, p_5, p_6, anchor_2)
        {
            camera = new Camera();

            BoundingSphere sphere = new BoundingSphere();
            float scale;
            /*Debug.WriteLine("Hey" + (
                (body.Left - (SettingsManager.GetInstance().ResolutionX/2.0f))
                / SettingsManager.GetInstance().ResolutionX));*/
            //translationAndScale = Matrix.Multiply(Matrix.CreateTranslation(translation), Matrix.CreateScale(new Vector3(0.5f,0.5f,0.5f)));

            //BoundingSphere will give us the size of the model, so we can scale accordingly
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                if (sphere.Radius == 0)
                    sphere = mesh.BoundingSphere;
                else
                    sphere = BoundingSphere.
                             CreateMerged(sphere, mesh.BoundingSphere);
            }
            Debug.WriteLine(sphere.Radius);
            scale = 2.0f / sphere.Radius;
            translationAndScale = Matrix.Multiply(Matrix.CreateTranslation(translation), Matrix.CreateScale(new Vector3(scale, scale, scale)));

        }

        public override void DrawWidget(){

            transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            if (flag == 1)
            rotation = (rotation + 0.00001f);// % (float)(Math.PI / 2);

            foreach (ModelMesh mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    //effect.World = Matrix.CreateScale(new Vector3(0.5f,0.5f,0.5f));
                    effect.World = Matrix.Multiply(Matrix.CreateRotationZ(rotation), translationAndScale);
                    //effect.World = Matrix.CreateTranslation(translation);
                    //Debug.WriteLine(effect.World);
                    //temp = Vector3.Transform(temp, Matrix.CreateRotationZ(rotation));
                    //Debug.WriteLine(Matrix.CreateRotationZ(rotation));
                    //effect.View = Matrix.CreateLookAt(temp, position , new Vector3(0,0,1));
                    //effect.View = Matrix.CreateLookAt(temp, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
                    effect.View = Matrix.CreateLookAt(camPos, new Vector3(0, 0, 0), Vector3.Up);
                    effect.Projection = camera.Projection;
                }

                mesh.Draw();
            }

            
        }

        public override void HandleWidgetInput()
        {
            flag = 1;
            Debug.WriteLine("Before: " + Matrix.Multiply(Matrix.CreateRotationZ(rotation), Matrix.CreateTranslation(translation)));
            rotation += increment;
            
            Vector3.Add(camPos, translation);
           /* camPos = Vector3.Transform(Vector3.Add(camPos, translation), new Matrix(1.0f, 1.0f, 0.0f, 0.0f,
                                                                                    -1.0f, 0.0f, 0.0f,0.0f,
                                                                                    0.0f, 0.0f, 1.0f,0.0f,
                                                                                    0.0f, 0.0f, 0.0f,1.0f));*/
            Debug.WriteLine("After: " + Matrix.Add(Matrix.CreateRotationZ(rotation), Matrix.CreateTranslation(translation)));
            /*if (Math.Abs(rotation) > (Math.PI / 2))
            {
                up = Vector3.Down;
                increment = increment * -1;
            }*/
        }

    }
}
