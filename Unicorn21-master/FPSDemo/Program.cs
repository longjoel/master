using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OpenTK.Graphics;

using Unicorn21.GameObjects;
using Unicorn21.Geometry;

using OpenTK;

//using OpenTK.Graphics;
using OpenTK.Input;

using System.Windows.Forms;

using BeginMode = OpenTK.Graphics.OpenGL.BeginMode;
using ClearBufferMask = OpenTK.Graphics.OpenGL.ClearBufferMask;
using GL = OpenTK.Graphics.OpenGL.GL;
using KeyPressEventArgs = OpenTK.KeyPressEventArgs;
using MatrixMode = OpenTK.Graphics.OpenGL.MatrixMode;

using Unicorn21.OpenTKRenderer;

namespace FPSDemo
{
    class Program : GameWindow
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern void ShowCursor(bool show);

        private static Vector2D[] octagon = new Vector2D[]
                                     {
                                         new Vector2D(25, 20),
                                         new Vector2D(35, 20),
                                         new Vector2D(40, 25), 
                                         new Vector2D(40, 35),
                                         new Vector2D(35,40),
                                          
                                         new Vector2D(25,40),
                                         
                                         new Vector2D(20,35),
                                         new Vector2D(20,25)
                                     };

        private static Vector2D[] octagon2 = new Vector2D[]
                                     {
                                         new Vector2D(25, -20),
                                         new Vector2D(35, -20),
                                         new Vector2D(40, -25), 
                                         new Vector2D(40, -35),
                                         new Vector2D(35,-40),
                                          
                                         new Vector2D(25,-40),
                                         
                                         new Vector2D(20,-35),
                                         new Vector2D(20,-25)
                                     };

        private static Vector2D[] _northWall = {
                                                  new Vector2D(-150,-150),
                                                  new Vector2D(150,-150),
                                                  new Vector2D(150,-120),
                                                  new Vector2D(-150,-120)
                                              };

        private static Vector2D[] _southWall = {
                                                   new Vector2D(-150,120),
                                                   new Vector2D(150,120),
                                                   new Vector2D(150,150),
                                                   new Vector2D(-150,150) 
                                               };

        private static Vector2D[] _westWall = {new Vector2D(-150,-120),
                                            new Vector2D(-120,-120),
                                              new Vector2D(-120,120),
                                              new Vector2D(-150,120) };

        private static Vector2D[] _eastWall = {
                                                  new Vector2D(150,-120),
                                                  new Vector2D(150,120),
                                                  new Vector2D(120,120),
                                                  new Vector2D(120,-120)
    };

        private static Vector2D[] _nwCorner = {
                                                  new Vector2D(-120, -120),
                                                  new Vector2D(0, -120),
                                                  new Vector2D(0, 0),
                                                  new Vector2D(-120, 0)
                                              };

        private static Vector2D[] _neCorner = {
                                                  new Vector2D(0,-120),
                                                  new Vector2D(120,-120),
                                                  new Vector2D(120,0),
                                                  new Vector2D(0,0) 
 
    };

        private static Vector2D[] _seCorner = {
                                                  new Vector2D(120, 120),
                                                  new Vector2D(0, 120),
                                                  new Vector2D(0, 0),
                                                  new Vector2D(120, 0)

                                              };

        private static Vector2D[] _swCorner = {
                                                  new Vector2D(0, 120),
                                                  new Vector2D(-120, 120),
                                                  new Vector2D(-120, 0),
                                                  new Vector2D(0, 0)
                                              };


        //private List<GameObject> _levelPieces;

        private Renderer _renderer;
        private Camera _camera;

        // private Player playerOne;
        //private SpawnPoint spawnPointOne;

        //private double _baseFloor, _baseCeiling;

        private SandboxGameInstance _gi;

        public Program()
            : base(640, 480, GraphicsMode.Default, "FPS Demo", GameWindowFlags.Default)
        {
            

        }

        protected override void OnLoad(EventArgs e)
        {
            var l = BuildLevel();

            var sp = GameObjectFactory.Instance.CreateSpawnPoint(new Vector2D(-50, -50), 6, 75);
            

            l.StaticGameObjects.Add(sp);


            GameObjectFactory.Instance.SaveLevel(l, "l1.level");

            var playerOne = GameObjectFactory.Instance.CreatePlayer("Some Guy", l.SpawnPoints[0]);

            _camera = new FirstPersonCamera(ref playerOne);


            _gi = new SandboxGameInstance(l);
            _gi.LivingGameObjects.Add(playerOne);

            _renderer = new Renderer(((GameInstance)_gi));

            base.OnLoad(e);
        }

        private Level BuildLevel()
        {
            var northWall = GameObjectFactory.Instance.CreateWall(new Polygon2D(_northWall.ToList()));
            northWall.WallTexture.TextureTag = "001";
            northWall.WallTexture.xScale = 10;
            northWall.WallTexture.yScale = 2;
            northWall.WallTexture.Rotation = 15;
            northWall.WallTexture.xOffset = 0;
            northWall.WallTexture.yOffset = 0;

            var southWall = GameObjectFactory.Instance.CreateWall(new Polygon2D(_southWall.ToList()));
            southWall.WallTexture.TextureTag = "002";
            
            southWall.WallTexture.xScale = 10;
            southWall.WallTexture.yScale = 2;
            southWall.WallTexture.Rotation = 0;
            southWall.WallTexture.xOffset = 5;
            southWall.WallTexture.yOffset = 10;

            var eastWall = GameObjectFactory.Instance.CreateWall(new Polygon2D(_eastWall.ToList()));
            eastWall.WallTexture.TextureTag = "002";
            
            eastWall.WallTexture.xScale = 10;
            eastWall.WallTexture.yScale = 2;
            eastWall.WallTexture.Rotation = 15;
            eastWall.WallTexture.xOffset = 0;
            eastWall.WallTexture.yOffset = 0;

            var westWall = GameObjectFactory.Instance.CreateWall(new Polygon2D(_westWall.ToList()));
            westWall.WallTexture.TextureTag = "002";

            westWall.WallTexture.xScale = 10;
            westWall.WallTexture.yScale = 2;
            westWall.WallTexture.Rotation = 15;
            westWall.WallTexture.xOffset = 0;
            westWall.WallTexture.yOffset = 0;


            var nwCorner = GameObjectFactory.Instance.CreateCorrdior(new Polygon2D(_nwCorner.ToList()), 5, 100);
            nwCorner.FloorWallTexture.TextureTag = "001";
            nwCorner.CeilingWallTexture.TextureTag = "002";
            
            nwCorner.FloorTexture.TextureTag = "001";
            nwCorner.CeilingTexture.TextureTag = "002";
            

            var swCorner = GameObjectFactory.Instance.CreateCorrdior(new Polygon2D(_swCorner.ToList()), 3, 100);
            swCorner.FloorWallTexture.TextureTag = "001";
            swCorner.CeilingWallTexture.TextureTag = "002";
            swCorner.FloorTexture.TextureTag = "001";
            swCorner.CeilingTexture.TextureTag = "002";
            
            var seCorner = GameObjectFactory.Instance.CreateCorrdior(new Polygon2D(_seCorner.ToList()), 2, 100);
            seCorner.FloorWallTexture.TextureTag = "001";
            seCorner.CeilingWallTexture.TextureTag = "002";
            seCorner.FloorTexture.TextureTag = "002";
            seCorner.CeilingTexture.TextureTag = "001";
            
            var neCorner = GameObjectFactory.Instance.CreateCorrdior(new Polygon2D(_neCorner.ToList()), 1, 100);
            neCorner.FloorWallTexture.TextureTag = "001";
            neCorner.CeilingWallTexture.TextureTag = "002";
            neCorner.FloorTexture.TextureTag = "002";
            neCorner.CeilingTexture.TextureTag = "002";
            

            var oct = GameObjectFactory.Instance.CreateCorrdior(new Polygon2D(octagon.ToList()), 5, 40);
            oct.FloorWallTexture.TextureTag = "001";
            oct.CeilingWallTexture.TextureTag = "002";
            oct.FloorTexture.TextureTag = "002";
            oct.CeilingTexture.TextureTag = "002";
            
            
            var oct2 = GameObjectFactory.Instance.CreatePlatform(new Polygon2D(octagon2.ToList()), 1.5,3);
            oct2.WallTexture.TextureTag = "001";
            oct2.FloorTexture.TextureTag = "001";
            oct2.CeilingTexture.TextureTag = "002";
            


            var l = GameObjectFactory.Instance.CreateLevel();
            l.Chunks.AddRange(
                               new List<LevelChunk>{
                                   westWall,northWall,southWall,eastWall,
                                   nwCorner,neCorner,seCorner,swCorner,
                                   oct, oct2
                               });

            GameObjectFactory.Instance.SaveLevel(l, "level1.level");

            return l;

        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var nmx = Mouse.X;
            var nmy = Mouse.Y;

            var dmx = (double)nmx - 320;
            var dmy = (double)nmy - 240;

            _gi.Players[0].Angle += dmx / 10;
            _gi.Players[0].LookAngle -= dmy / 15;

            var p = PointToScreen(new System.Drawing.Point(320, 240));
            SetCursorPos(p.X, p.Y);

            var velocityVector = new Vector2D();

            //
            bool moving = false;
            if ((GetAsyncKeyState(Keys.W) & 0x8000) != 0)
            {
                moving = true;
                velocityVector.X += Math.Cos(_gi.Players[0].Angle * Math.PI / 180);
                velocityVector.Y += Math.Sin(_gi.Players[0].Angle * Math.PI / 180);

            }


            if ((GetAsyncKeyState(Keys.S) & 0x8000) != 0)
            {
                moving = true;
                velocityVector.X += Math.Cos((_gi.Players[0].Angle + 180) * Math.PI / 180);
                velocityVector.Y += Math.Sin((_gi.Players[0].Angle + 180) * Math.PI / 180);

            }

            if ((GetAsyncKeyState(Keys.A) & 0x8000) != 0)
            {
                moving = true;
                velocityVector.X += Math.Cos((_gi.Players[0].Angle - 90) * Math.PI / 180);
                velocityVector.Y += Math.Sin((_gi.Players[0].Angle - 90) * Math.PI / 180);

            }

            if ((GetAsyncKeyState(Keys.D) & 0x8000) != 0)
            {
                moving = true;
                velocityVector.X += Math.Cos((_gi.Players[0].Angle + 90) * Math.PI / 180);
                velocityVector.Y += Math.Sin((_gi.Players[0].Angle + 90) * Math.PI / 180);

            }

            bool isjumping = false;
            if ((GetAsyncKeyState(Keys.Space) & 0x8000) != 0)
            {
                moving = true;
               isjumping = true;
            }


            if ((GetAsyncKeyState(Keys.Escape) & 0x8000) != 0)
                this.Exit();


            var moveAction = new Unicorn21.GameObjects.GameActions.ActionMovePlayer(_gi.Players[0], velocityVector, isjumping);

            if(moving)
                _gi.AddAction(moveAction);
            _gi.DoGame(e.Time);



            base.OnUpdateFrame(e);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(0, 100, 0, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _renderer.RenderScene(_camera);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        [STAThread]
        static void Main(string[] args)
        {
            ShowCursor(false);
            using (var gameWindow = new Program())
            {
                gameWindow.Run(60, 30);
            }
            ShowCursor(true);
        }
    }
}
