using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Unicorn21.GameObjects;
using Unicorn21.Geometry;

namespace NahrwallEditor
{
    public partial class FrmTextureManipulator : Form
    {
        private double xRotation;
        private double yRotation;

        double radius;

        double xCenter;
        double yCenter;
        double zCenter;

        public void SetChunk()
        {
            var chunk = AppGlobals.Instance.EditorCurrentChunk;

            xRotation = yRotation = 0;

            // you can take the programmer out of C, but you can't take the C out of the programmer;
            radius = xCenter = yCenter = zCenter = 0;

            if (chunk is Platform)
            {
                var c = (chunk as Platform);
                zCenter = (c.FloorHeight - c.CeilingHeight) / 2.0;

                xCenter = (from cx in c.Area.Points select cx.X).Average();
                yCenter = (from cy in c.Area.Points select cy.Y).Average();

                radius = xCenter;
                if (yCenter > radius) radius = yCenter;
                if (zCenter > radius) radius = zCenter;


            }

            else
                if (chunk is Corridor)
                {
                    var c = (chunk as Corridor);
                    zCenter = (c.CeilingHeight - c.FloorHeight) / 2.0;

                    xCenter = (from cx in c.Area.Points select cx.X).Average();
                    yCenter = (from cy in c.Area.Points select cy.Y).Average();

                    radius = xCenter;
                    if (yCenter > radius) radius = yCenter;
                    if (zCenter > radius) radius = zCenter;


                }

                else
                    if (chunk is Wall)
                    {
                        var c = (chunk as Wall);
                        zCenter = (AppGlobals.Instance.EditorCurrentLevel.BaseCeilingHeight - AppGlobals.Instance.EditorCurrentLevel.BaseFloorHeight) / 2.0;

                        xCenter = (from cx in c.Area.Points select cx.X).Average();
                        yCenter = (from cy in c.Area.Points select cy.Y).Average();

                        radius = xCenter;
                        if (yCenter > radius) radius = yCenter;
                        if (zCenter > radius) radius = zCenter;
                    }
            Invalidate();
        }

        public void RedrawTextureWindow()
        {
            this.glViewTexture.MakeCurrent();

            GL.ClearColor(0, 0, 0, 0);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            if (AppGlobals.Instance.EditorCurrentChunk != null)
            {

                GL.Viewport(glViewTexture.Size);

                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-radius, radius, -radius, radius, -radius, radius);


                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                GL.Translate(xCenter, yCenter, zCenter);

                GL.Rotate(xRotation, 1, 0, 0);
                GL.Rotate(xRotation, 0, 0, 1);

                GL.Scale(.75, .75, .75);

                var chunk = AppGlobals.Instance.EditorCurrentChunk;


                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);

                AppGlobals.Instance.EditorRender.DrawLevelChunk(chunk);


                GL.Disable(EnableCap.Texture2D);
                GL.Disable(EnableCap.DepthTest);
            }
            glViewTexture.SwapBuffers();

        }

        public FrmTextureManipulator()
        {
            InitializeComponent();
        }

        private void glViewTexture_Paint(object sender, PaintEventArgs e)
        {
            RedrawTextureWindow();
        }
    }
}
