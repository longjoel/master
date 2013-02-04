using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Pycraft
{
    public partial class Form1 : Form
    {
        Timer _uiThreadPumpTimer;
        bool _isLoaded;

        Entities.Map _map;
        Entities.MapCursor _cursor;

        int _texture;

        public Form1()
        {
            InitializeComponent();
            _isLoaded = false;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (_isLoaded)
            {
                GL.Viewport(0, 0, Width, Height);
                Invalidate();
            }

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (_isLoaded)
            {
                Commanders.MapCursorCommander.UpdateMapCursor(_cursor);

                GL.ClearColor(Color.Gray);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                var mv = Matrix4.LookAt(
                    60, 32, 0,
                    0, 0, 0,
                    0, 1, 0);
                GL.LoadMatrix(ref mv);

                GL.Rotate(45, 0, 1, 0);

                Commanders.MapCursorCommander.DrawCursor(_cursor);


                glControl1.SwapBuffers();

                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _uiThreadPumpTimer = new Timer();
            _uiThreadPumpTimer.Interval = 1;
            
            _uiThreadPumpTimer.Tick +=new EventHandler((a,b)=>{
                EventPumps.UIThreadEventPump.Instance.DoWork();
                glControl1.Invalidate();
                
            });

            _uiThreadPumpTimer.Start();

            _map = Commanders.MapCommander.CreateMap();
            _cursor = Commanders.MapCursorCommander.CreateMapCursorForMap(_map, 0, 0, 0);

            EventPumps.IronPythonScriptQueue.Instance.Start();
            EventPumps.IronPythonScriptQueue.Instance.SetMap(_map);


           

            glControl1.MakeCurrent();

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            var vm = Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI / 4.0), 1, 1, 200);
            GL.LoadMatrix(ref vm);



            GL.MatrixMode(MatrixMode.Modelview);

            _isLoaded = true;
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            EventPumps.IronPythonScriptQueue.Instance.EnqueueScript(txtScript.Text);
            
        }
    }
}
