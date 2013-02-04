using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using gl = OpenTK.Graphics.OpenGL.GL ;

namespace CogitaGameEntities
{
    public class FPSCamera
    {
        public double Near { get; set; }
        public double Far { get; set; }

        public double FOVY { get; set; }
        public double Aspect { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Pitch { get; set; }
        public double Yaw { get; set; }

        public Matrix4 CameraModelViewMatrix
        {
            get
            {
                var theta = (Math.PI / 180.0) * Pitch;
                var phi = (Math.PI / 180.0) * Yaw;

                return
                    Matrix4.CreateTranslation((float)-X, (float)-Y, (float)-Z) *
                    Matrix4.CreateRotationY((float)phi) *
                    Matrix4.CreateRotationX((float)theta);
            }
        }

        public Matrix4 CameraProjectionMatrix
        {
            get
            {
                return Matrix4.CreatePerspectiveFieldOfView(
                   (float)(Math.PI / 4.0), (float)Aspect, 
                    (float)Near, (float)Far);
            }
        }

        public void UseCamera()
        {
            gl.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            var cpm = CameraProjectionMatrix;
            gl.LoadMatrix(ref cpm);

            gl.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            var cmv = CameraModelViewMatrix;
            gl.LoadMatrix(ref cmv);

            CogitaLoggingEngine.Logger.Data(ToString());

        }

        public override string ToString()
        {
            return string.Format("Position({0},{1},{2}) Pitch({3}) Yaw({4}) Near({5}) Far({6})",
                X,Y,Z,
                Pitch, Yaw,
                Near, Far);
        }
    }
}
