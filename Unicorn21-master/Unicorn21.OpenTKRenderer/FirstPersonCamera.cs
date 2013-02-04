using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.GameObjects;

using Unicorn21.Geometry;

using OpenTK.Graphics.OpenGL;

using OpenTK;

namespace Unicorn21.OpenTKRenderer
{
    public class FirstPersonCamera:Camera
    {

        public FirstPersonCamera(ref Player p)
            : base(ref p)
        {
        }

        public override void SetupCamera()
        {
            //GL.Viewport(0, 0, 640, 480);

            GL.MatrixMode(MatrixMode.Projection);

            var id = Matrix4d.Identity;
            GL.LoadMatrix(ref id);

            var projection = Matrix4d.Perspective(45, 4 / 3, .45, 350);
            GL.LoadMatrix(ref projection);

        }

        public override void UseCamera()
        {
            var p = _player;

            GL.MatrixMode(MatrixMode.Modelview);
            var clear = Matrix4d.Identity;


            GL.LoadMatrix(ref clear);

            var look = Matrix4d.LookAt(p.Position.X, 
                p.Z + Player.HeadHeight, 
                p.Position.Y,
                
                p.Position.X + Math.Cos(p.Angle * Math.PI / 180), 
                p.Z + Player.HeadHeight + Math.Sin(p.LookAngle * Math.PI / 180), 
                p.Position.Y + Math.Sin(p.Angle * Math.PI / 180),

                0, 1, 0);

            GL.LoadMatrix(ref look);

            float[] position = { (float)p.X, (float)p.Z, (float)p.Y };
            GL.Light(LightName.Light0, LightParameter.Position, position);
        }


    }
}
