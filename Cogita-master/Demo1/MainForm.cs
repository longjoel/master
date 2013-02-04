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

using CogitaLoggingEngine;

namespace Demo1
{
    public partial class MainForm : Form
    {

        private bool _isInitialized = false;
        
        private const bool RENDER_DEBUG = false;

        Logger _cogitaLogging = null;

        public MainForm()
        {
            InitializeComponent();

            _cogitaLogging = new Logger();
            _cogitaLogging.Show();
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_isInitialized)
            {
                GL.ClearColor(Color.Gray);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                CogitaGameEntities.CogitaGameInstance.Instance.Player.Camera.UseCamera();

                //if (RENDER_DEBUG)
                //{
                //    GL.Begin(BeginMode.Lines);

                //    for (int i = -10; i <= 10; i++)
                //    {
                //        GL.Vertex3(-10, -10, i); GL.Vertex3(10, -10, i);
                //        GL.Vertex3(-10,  i,-10); GL.Vertex3(10, -10, i);
                //    }

                //    GL.End();
                //}


                

                CogitaGameEntities.CogitaGameInstance.Instance.MapCursor.Update();
                CogitaGameEntities.CogitaGameInstance.Instance.MapCursor.Draw(1);

                this.PrimaryViewGLControl.SwapBuffers();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrimaryViewGLControl.MakeCurrent();
            _isInitialized = true;

            GameInstanceTimer.Start();

            System.Threading.Tasks.Task.Factory.StartNew(() => {
                while (true)
                {
                    CogitaGameEntities.CogitaGameInstance.Instance.DoPhysics((100.0) / 1000.0);
                    System.Threading.Thread.Sleep(100);
                }
            });

            KeyPreview = true;
        }

        private void GameInstanceTimer_Tick(object sender, EventArgs e)
        {
            // Update the terrain info
            CogitaTerrainObjects.EventPumps.UIThreadEventPump.Instance.DoWork();
            Invalidate();
          

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var gi = CogitaGameEntities.CogitaGameInstance.Instance;
            var player = gi.Player;

            if (e.KeyCode == Keys.Q)
            {
                player.Yaw -= 1.25;

            }

            if (e.KeyCode == Keys.E)
            {
                player.Yaw += 1.25;
            }

            if (e.KeyCode == Keys.W)
            {
                player.Move(player.Yaw, 9);
                
            }

            if (e.KeyCode == Keys.S)
            {
                player.Move(player.Yaw+180, 9);
            }

            if (e.KeyCode == Keys.A)
            {
                player.Move(player.Yaw-90, 5);

            }

            if (e.KeyCode == Keys.D)
            {
                player.Move(player.Yaw +90, 5);
            }

            e.Handled = true;
            base.OnKeyDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            
            base.OnMouseMove(e);
        }
       
    }
}
