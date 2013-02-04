using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Audio;
using OpenTK.Math;
using OpenTK.Input;
using OpenTK.Platform;

using deathcave_logic;

namespace deathcave_OpenTK
{
    class Game : GameWindow
    {
        
        
        private DeathCaveGame dcg;
        private GameVars gv;

        bool spaceDown = false;

        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public Game()
            //: base(800, 600, GraphicsMode.Default,"", GameWindowFlags.Fullscreen)
            : base(800, 600, GraphicsMode.Default, "", GameWindowFlags.Fullscreen)
        {

            dcg = new DeathCaveGame();
        }

       
        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        public override void OnLoad(EventArgs e)
        {
            GL.ClearColor(System.Drawing.Color.SteelBlue);
            //GL.Enable(EnableCap.DepthTest);

        }

        
        /// <summary>
        /// Called when it is time to setup the next frame. Add you game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
        public override void OnUpdateFrame(UpdateFrameEventArgs e)
        {
           
            InputEnum IE = new InputEnum();

            if (Joysticks.Count > 0)
            {
                if (Joysticks[0].Axis[1] > 0.2f)
                    IE |= InputEnum.Player1Up;

                if (Joysticks[0].Axis[1] < -0.2f)
                    IE |= InputEnum.Player1Down;

                if (Joysticks[0].Axis[0] < -0.2f)
                    IE |= InputEnum.Player1Left;

                if (Joysticks[0].Axis[0] > 0.2f)
                    IE |= InputEnum.Player1Right;

                if (Joysticks[0].Button[0])
                {
                    if (!this.spaceDown)
                    {
                        this.spaceDown = true;
                        IE |= InputEnum.Player1Fire;
                    }
                }
                else
                    this.spaceDown = false;
            }
            else
            {

                // Process held down keyboard inputs 
                if (Keyboard[Key.Up])
                    IE |= InputEnum.Player1Up;

                if (Keyboard[Key.Down])
                    IE |= InputEnum.Player1Down;

                if (Keyboard[Key.Left] )
                    IE |= InputEnum.Player1Left;

                if (Keyboard[Key.Right])
                    IE |= InputEnum.Player1Right;

                if (Keyboard[Key.Space])
                {
                    if (!this.spaceDown)
                    { 
                        this.spaceDown = true;
                        IE |= InputEnum.Player1Fire;
                    }
                }
                else
                    this.spaceDown = false;
            }


            
            gv = dcg.Process(IE, (float)e.Time);
            
            if (Keyboard[Key.Escape])
                Exit();
        }

        public void DrawBGO(deathcave_logic.gameObjects.BaseGameObject bgo)
        {
            if (bgo.Position.IntersectsWith(gv.activeWindow))
            {
                GL.Vertex2(bgo.Position.X, bgo.Position.Y);
                GL.Vertex2(bgo.Position.X + bgo.Position.Width, bgo.Position.Y);
                GL.Vertex2(bgo.Position.X + bgo.Position.Width, bgo.Position.Y + bgo.Position.Height);
                GL.Vertex2(bgo.Position.X, bgo.Position.Y + bgo.Position.Height);
                
            }
            
        }

        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        public override void OnRenderFrame(RenderFrameEventArgs e)
        {
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (gv != null)
            {
                //GL.Viewport((int)gv.activeWindow.X, (int)gv.activeWindow.Y, (int)gv.activeWindow.Width, (int)gv.activeWindow.Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                Glu.Ortho2D(0, 800, gv.activeWindow.Y + 600, gv.activeWindow.Y);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();


                // draw all the obstacles

                GL.Color3(Color.Black);
                GL.Begin(BeginMode.Quads);
                
                foreach (deathcave_logic.gameObjects.BaseGameObject bgo in gv.obstacles)
                {
                    DrawBGO(bgo);
                   
                }

                GL.End();

                // draw the shots
                GL.Color3(Color.YellowGreen);
                GL.Begin(BeginMode.Quads);
                foreach (deathcave_logic.gameObjects.BaseGameObject bgo in gv.projectiles)
                {
                    DrawBGO(bgo);
                }
                GL.End();

                // draw explosions
                GL.Color3(Color.Red);
                GL.Begin(BeginMode.Quads);
                foreach (deathcave_logic.gameObjects.BaseGameObject bgo in gv.effects)
                {
                    DrawBGO(bgo);
                }
                GL.End();

                //draw the enemies
                GL.Begin(BeginMode.Quads);
                foreach (deathcave_logic.gameObjects.BaseGameObject bgo in gv.enemies)
                {
                    
                    switch (bgo.ObjectType)
                    {
                        case GameObjectEnum.EnemyDropper:
                            GL.Color3(Color.Plum);
                            break;
                        case GameObjectEnum.EnemyShooter:
                            GL.Color3(Color.SeaShell);
                            break;
                        case GameObjectEnum.EnemySwooper:
                            GL.Color3(Color.BlanchedAlmond);
                            break;
                    }

                    DrawBGO(bgo);
                }
                GL.End();

                // draw the ship

                if (this.gv.safeTimer < 0.0f)
                    GL.Color3(Color.Blue);
                else
                    GL.Color3(Color.Yellow);

                GL.Begin(BeginMode.Quads);
                
                DrawBGO(gv.ship);
                
                GL.End();
                
            }

            SwapBuffers();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (Game game = new Game())
            {
                game.Run(60.0, 0.0);
            }
        }
    }
}