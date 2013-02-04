using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using Unicorn21.GameObjects;
using Unicorn21.Geometry;
using Unicorn21.OpenTKRenderer;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace NahrwallEditor
{
    public class AppGlobals
    {
        private static AppGlobals _instance;

        public static AppGlobals Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppGlobals();
                return _instance;
            }
        }

        public ContentManager EditorContentManager { get; set; }
        public GameObjectFactory EditorGameObjectFactory { get; set; }
        public Renderer EditorRender { get; set; }

        public Level EditorCurrentLevel { get; set; }
        public LevelChunk EditorCurrentChunk { get; set; }

        public StaticGameObject EditorCurrentGameObject { get; set; }

        public double Zoom { get; set; }
        public Vector2D BaseCursorPosition { get; set; }

        public frmXMLWindow XmlWindow { get; set; }
        public FrmMain MainWindow { get; set; }
        public FrmTextureManipulator TextureManipulatorWindow { get; set; }
        public FrmResourceManager ResourceManagerWindow { get; set; }

        public string CurrentPath { get; set; }

        private AppGlobals()
        {

            MainWindow = new FrmMain();
            XmlWindow = new frmXMLWindow();
            TextureManipulatorWindow = new FrmTextureManipulator();
            ResourceManagerWindow = new FrmResourceManager();


            EditorContentManager = ContentManager.Instance;
            EditorGameObjectFactory = GameObjectFactory.Instance;

            EditorCurrentLevel = EditorGameObjectFactory.CreateLevel();

            Zoom = 5.0;
            BaseCursorPosition = Vector2D.Zero;

            EditorCurrentChunk = null;

            CurrentPath = "";
        }

        public void RedrawLevel(bool walls, bool corridors, bool platforms, bool entities)
        {
            if (EditorCurrentLevel != null)
            {
                if (corridors)
                {
                    var q = from x in EditorCurrentLevel.Chunks
                            where x is Corridor
                            orderby (x as Corridor).FloorHeight ascending
                            select x as Corridor;

                    foreach (var r in q)
                    {
                        if (r == EditorCurrentChunk)
                        {
                            var pts = this.EditorCurrentChunk.Area;

                            GL.Color3(System.Drawing.Color.DarkGreen);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in pts.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.Red);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in pts.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                        else
                        {

                            GL.Color3(System.Drawing.Color.Gray);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in r.Area.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.White);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in r.Area.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                    }
                }

                if (platforms)
                {
                    var q = from x in EditorCurrentLevel.Chunks
                            where x is Platform
                            orderby (x as Platform).FloorHeight ascending
                            select x as Platform;

                    foreach (var r in q)
                    {
                        if (r == EditorCurrentChunk)
                        {
                            var pts = this.EditorCurrentChunk.Area;

                            GL.Color3(System.Drawing.Color.DarkGreen);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in pts.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.Red);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in pts.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                        else
                        {
                            GL.Color3(System.Drawing.Color.LightGray);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in r.Area.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.White);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in r.Area.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                    }
                }


                if (walls)
                {
                    var q = from x in EditorCurrentLevel.Chunks
                            where x is Wall
                            //orderby (x as Wall).FloorHeight ascending
                            select x as Wall;

                    foreach (var r in q)
                    {
                        if (r == EditorCurrentChunk)
                        {
                            var pts = this.EditorCurrentChunk.Area;

                            GL.Color3(System.Drawing.Color.DarkGreen);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in pts.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.Red);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in pts.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                        else
                        {
                            GL.Color3(System.Drawing.Color.Black);
                            GL.Begin(BeginMode.Triangles);
                            foreach (var t in r.Area.Triangulate())
                            {
                                GL.Vertex2(t.Points[0].X, t.Points[0].Y);
                                GL.Vertex2(t.Points[1].X, t.Points[1].Y);
                                GL.Vertex2(t.Points[2].X, t.Points[2].Y);
                            }
                            GL.End();

                            GL.Color3(System.Drawing.Color.White);
                            GL.Begin(BeginMode.Lines);
                            foreach (var l in r.Area.Lines)
                            {
                                GL.Vertex2(l.A.X, l.A.Y);
                                GL.Vertex2(l.B.X, l.B.Y);
                            }
                            GL.End();
                        }
                    }
                }


                if (entities)
                {
                    var ents = from e in EditorCurrentLevel.StaticGameObjects
                               orderby e.Z ascending
                               select e;
                    GL.Color3(System.Drawing.Color.Yellow);
                    GL.Begin(BeginMode.Quads);
                    foreach (var e in ents)
                    {
                        var xmin = e.X-.5;
                        var xmax = e.X+.5;
                        var ymin = e.Y-.5;
                        var ymax = e.Y+.5;
                        GL.Vertex2(xmin, ymin);
                        GL.Vertex2(xmax, ymin);
                        GL.Vertex2(xmax, ymax);
                        GL.Vertex2(xmin, ymax);
                    }
                    GL.End();
                }

            }
        }


       
    }
}
