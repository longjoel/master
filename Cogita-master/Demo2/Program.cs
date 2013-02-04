using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using CogitaGameEntities;
using CogitaTerrainObjects;

using CogitaTerrainObjects.Entities;

using System.Diagnostics;

namespace Demo2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            int mouseX = 0;
            int mouseY = 0;

            Dictionary<BrickTypeEnum, int> brickTextures = new Dictionary<BrickTypeEnum,int>();

            using (var w = new GameWindow(640, 480))
            {
                w.CursorVisible = false;

                w.RenderFrame += (o, e) => {
                    GL.ClearColor(Color.Gray);
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    CogitaGameEntities.CogitaGameInstance.Instance.Player.Camera.UseCamera();

                    //GL.ClientActiveTexture(TextureUnit.Texture0);
                    
                    foreach (var bt in Enum.GetValues(typeof(BrickTypeEnum)))
                    {
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, brickTextures[(BrickTypeEnum)bt]);
                        //Debug.Assert(GL.GetError() == ErrorCode.NoError);
                        CogitaGameEntities.CogitaGameInstance.Instance.MapCursor.Draw((BrickTypeEnum)bt);
                        
                        GL.BindTexture(TextureTarget.Texture2D,0);
                        //Debug.Assert(GL.GetError() == ErrorCode.NoError);
                        GL.Disable(EnableCap.Texture2D);
                        
                    }
                   

                    w.SwapBuffers();

                };
                
                w.UpdateFrame += (o, e) => {
                    CogitaGameEntities.CogitaGameInstance.Instance.MapCursor.Update();
                    CogitaTerrainObjects.EventPumps.UIThreadEventPump.Instance.DoWork();
                    CogitaGameEntities.CogitaGameInstance.Instance.DoPhysics(Math.Max(e.Time,0.01));

                    var gi = CogitaGameEntities.CogitaGameInstance.Instance;
                    var player = gi.Player;

                    ProcessKeyPresses(w, player);

                    if (w.Keyboard[OpenTK.Input.Key.Escape])
                        w.Exit();

                    var mState = OpenTK.Input.Mouse.GetState();
                    var mdx = mState.X - mouseX;
                    var mdy = mState.Y - mouseY;

                    mouseX = mState.X;
                    mouseY = mState.Y;

                    player.Pitch += ((double)mdy / 200.0);
                    player.Yaw += ((double)mdx / 200.0);
                };

                w.Load += (o, e) => 
                {
                    foreach (var bt in Enum.GetValues(typeof(BrickTypeEnum)))
                    {
                      brickTextures[(BrickTypeEnum)bt] = Util.GenTexture(string.Format("../res/{0}.png",bt.ToString()));
                    }
                };

                w.Run();
            }
        }


      

        private static void ProcessKeyPresses(GameWindow w, Player player)
        {
            bool isMoving = false;

            var kstate = OpenTK.Input.Keyboard.GetState();
           
            
            if (kstate[OpenTK.Input.Key.Q])
            {
                player.Yaw -= 1.25;
            }

            if (kstate[OpenTK.Input.Key.Space] && Math.Abs(player.VY) < 0.25 && player.CanJump)
            {
                player.VY += 5;
                player.CanJump = false;
            }

            if (kstate[OpenTK.Input.Key.E])
            {
                player.Yaw += 1.25;
            }

            if (kstate[OpenTK.Input.Key.D])
            {
                player.Move(player.Yaw, 9);
                isMoving = true;
            }

            if (kstate[OpenTK.Input.Key.A])
            {
                player.Move(player.Yaw + 180, 4);
                isMoving = true;
            }

            if (kstate[OpenTK.Input.Key.S])
            {
                player.Move(player.Yaw + 90, 4);
                isMoving = true;
            }

            if (kstate[OpenTK.Input.Key.W])
            {
                player.Move(player.Yaw - 90, 9);
                isMoving = true;
            }

            if (isMoving)
                player.Y += 0.025;
        }       
    }
}
