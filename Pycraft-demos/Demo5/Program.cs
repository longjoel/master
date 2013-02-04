using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Pycraft
{
    class Program : GameWindow
    {
        Entities.Map _map;
        Entities.MapCursor _mapCursor;
        int texture;


        public Program()
            : base(640, 480)
        {
            VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            _map = Commanders.MapCommander.CreateMap();
            _mapCursor = Commanders.MapCursorCommander.CreateMapCursorForMap(_map, 0, 0, 0);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);

            texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texture);

            Bitmap bx = new Bitmap("block.png");

            BitmapData data = bx.LockBits(new Rectangle(0, 0, bx.Width, bx.Height),
               ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bx.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


            bx.Dispose();


            System.Threading.Tasks.Task.Factory.StartNew(new Action( ()=>{
            for (int R = 0; R < 100; R += 20)
            {
                var r = Math.Sqrt(R / 5);

                for (double x = -100; x < 100; x++)
                {
                    for (double y = -100; y < 100; y++)
                    {
                        for (double z = -100; z < 100; z++)
                        {
                            var f1 = ((R - Math.Sqrt(x * x + y * y)) * (R - Math.Sqrt(x * x + y * y))) +
                                (z * z) - (r * r);

                            var f2 = ((R - Math.Sqrt(x * x + z * z)) * (R - Math.Sqrt(x * x + z * z))) +
                                (y * y) - (r * r);

                            var f3 = ((R - Math.Sqrt(z * z + y * y)) * (R - Math.Sqrt(z * z + y * y))) +
                                (x * x) - (r * r);

                            if (f1 < 2)
                            {
                                Commanders.MapCommander.SetBlock(_map, (long)x, (long)y, (long)z, 1);
                            }

                            if (f2 < 1)
                            {
                                Commanders.MapCommander.SetBlock(_map, (long)x, (long)y, (long)z, 1);
                            }

                            if (f3 < 1)
                            {
                                Commanders.MapCommander.SetBlock(_map, (long)x, (long)y, (long)z, 1);
                            }

                            System.Threading.Thread.Sleep(0);
                        }
                    }
                }
            }

            }));


            base.OnLoad(e);
        }

        double r = 0;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            var projectionMatrix = Matrix4d.Perspective(45, 1, 1, 400);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            var modelviewMatrix = Matrix4d.LookAt(-80, 90, -80,
                0, 0, 0,
                0, 1, 0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelviewMatrix);

            GL.ClearColor(Color4.Blue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            r += e.Time;
            GL.Rotate(r * 15, 0, 1, 0);
            GL.Rotate(r * 10, 1, 0, 0);

            GL.Enable(EnableCap.Texture2D);
                Commanders.MapCursorCommander.DrawCursor(_mapCursor);
                GL.Disable(EnableCap.Texture2D);
            SwapBuffers();

            this.Title = string.Format("{0:0.##} -- ", (1.0 / e.Time).ToString());

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            EventPumps.UIThreadEventPump.Instance.DoWork();

            lock (_mapCursor)
            {
                Commanders.MapCursorCommander.UpdateMapCursor(_mapCursor);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);


            base.OnResize(e);
        }

        static void Main(string[] args)
        {

            using (var p = new Program())
            {
                p.Run();
            }
        }
    }
}
