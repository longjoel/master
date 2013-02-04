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
    public partial class FrmMain : Form
    {

        public void RedrawMainWindow()
        {
            glViewMain.MakeCurrent();
            var xBase = AppGlobals.Instance.BaseCursorPosition.X;
            var yBase = AppGlobals.Instance.BaseCursorPosition.Y;
            var scale = AppGlobals.Instance.Zoom;

            var halfWindowWidth = glViewMain.Size.Width / 2;
            var halfWindowHeight = glViewMain.Size.Height / 2;

            GL.Viewport(new Rectangle(0, 0, this.glViewMain.Size.Width, this.glViewMain.Size.Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(xBase - (halfWindowWidth / scale), xBase + (halfWindowWidth / scale),
               yBase + (halfWindowHeight / scale), yBase - (halfWindowHeight / scale), -1, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.ClearColor(Color.DarkBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            //AppGlobals.Instance.RedrawLevel(true, true, true, false);
            AppGlobals.Instance.RedrawLevel(cbxWalls.Checked, cbxCorridors.Checked, cbxPlatforms.Checked, cbxEntities.Checked);


            GL.Color3(Color.White);

            GL.Begin(BeginMode.Points);
            for (double i = AppGlobals.Instance.BaseCursorPosition.Y - halfWindowHeight; i < AppGlobals.Instance.BaseCursorPosition.Y + halfWindowHeight; i += 1)
            {
                for (double j = AppGlobals.Instance.BaseCursorPosition.X - halfWindowWidth; j < AppGlobals.Instance.BaseCursorPosition.X + halfWindowWidth; j += 1)
                {
                    GL.Vertex2((j - AppGlobals.Instance.BaseCursorPosition.X),
                       (i - AppGlobals.Instance.BaseCursorPosition.Y));
                }
            }
            GL.End();

            glViewMain.SwapBuffers();
        }

        public FrmMain()
        {
            InitializeComponent();

        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            glViewMain.Size = this.Size;
            glViewMain.Location = new Point(0, 0);

            Invalidate();

            AppGlobals.Instance.XmlWindow.Show(this);
            AppGlobals.Instance.XmlWindow.Visible = false;

            AppGlobals.Instance.TextureManipulatorWindow.Show(this);
            AppGlobals.Instance.TextureManipulatorWindow.Visible = false;

            AppGlobals.Instance.ResourceManagerWindow.Show(this);
            AppGlobals.Instance.ResourceManagerWindow.Visible = false;


            //var types = from q in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.GameObject)) select q;
            var types = from q in System.Reflection.Assembly.GetAssembly(typeof(GameInstance)).GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) && !q.IsAbstract select q;
            foreach (var t in types)
            {
                this.cmbAddEntity.Items.Add(t.Name);
            }

            if (types.Count() > 0)
            {
                this.cmbAddEntity.SelectedIndex = 0;
                SetupAddEntity();
            }



        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            glViewMain.Size = this.Size;
            glViewMain.Location = new Point(0, 0);

            Invalidate();


        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            RedrawMainWindow();
        }

        private void glViewMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' && AppGlobals.Instance.Zoom > 5)
                AppGlobals.Instance.Zoom -= 1;

            if (e.KeyChar == '=' && AppGlobals.Instance.Zoom < 25)
                AppGlobals.Instance.Zoom += 1;

            RedrawMainWindow();

            txtZoom.Text = AppGlobals.Instance.Zoom.ToString();
        }

        bool isScrolling = false;
        double lastMouseX = 0;
        double lastMouseY = 0;
        private void glViewMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var p =
                isScrolling = true;
                lastMouseX = e.Location.X;
                lastMouseY = e.Location.Y;
            }


        }

        private void glViewMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { isScrolling = false; }
        }

        private static Vector2D UnProject(Vector2D point, Vector2D windowSize, Vector2D camOrigin, Vector2D camSize)
        {


            var xScale = point.X / windowSize.X;
            var yScale = point.Y / windowSize.Y;

            return new Vector2D(
                (xScale * camSize.X) + camOrigin.X,
                (yScale * camSize.Y) + camOrigin.Y);


        }

        private void glViewMain_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseNewX = e.Location.X;
            var mouseNewY = e.Location.Y;

            var ww = this.glViewMain.Width;
            var wh = this.glViewMain.Height;

            var halfWindowWidth = glViewMain.Size.Width / 2;
            var halfWindowHeight = glViewMain.Size.Height / 2;

            var xBase = AppGlobals.Instance.BaseCursorPosition.X;
            var yBase = AppGlobals.Instance.BaseCursorPosition.Y;
            var scale = AppGlobals.Instance.Zoom;

            var cw = (ww / scale);
            var ch = (wh / scale);

            var lastMousePos = UnProject(new Vector2D(lastMouseX, lastMouseY),
                new Vector2D(ww, wh),
                new Vector2D(xBase - (halfWindowWidth / scale),
                    yBase - (halfWindowHeight / scale)),
                new Vector2D(cw, ch));

            var newMousePos = UnProject(new Vector2D(mouseNewX, mouseNewY),
                new Vector2D(ww, wh),
                new Vector2D(xBase - (halfWindowWidth / scale),
                    yBase - (halfWindowHeight / scale)),
                new Vector2D(cw, ch));

            this.lblxMousePos.Text = newMousePos.X.ToString();
            this.lblyMousePos.Text = newMousePos.Y.ToString();

            lastMouseX = mouseNewX;
            lastMouseY = mouseNewY;

            if (isScrolling)
            {



                AppGlobals.Instance.BaseCursorPosition.X -= (newMousePos.X - lastMousePos.X);
                AppGlobals.Instance.BaseCursorPosition.Y -= (newMousePos.Y - lastMousePos.Y);



                Invalidate();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AppGlobals.Instance.TextureManipulatorWindow.Visible = !AppGlobals.Instance.TextureManipulatorWindow.Visible;

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AppGlobals.Instance.XmlWindow.Visible = !AppGlobals.Instance.XmlWindow.Visible;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            AppGlobals.Instance.ResourceManagerWindow.Visible = !AppGlobals.Instance.ResourceManagerWindow.Visible;
        }

        void glViewMain_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var newPos = AppGlobals.Instance.Zoom + (double)(e.Delta / 120);

            if (newPos > 5 && newPos < 25)
                AppGlobals.Instance.Zoom = newPos;

            this.txtZoom.Text = newPos.ToString();

            Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q = new OpenFileDialog();
            q.AddExtension = true;
            q.CheckFileExists = true;
            q.CheckPathExists = true;
            q.DefaultExt = ".level";
            q.Filter = "Level Files (*.level)|*.level";
            q.Multiselect = false;


            if (q.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                Level x = null;
                try
                {
                    x = GameObjectFactory.Instance.LoadLevel(q.FileName);

                    AppGlobals.Instance.CurrentPath = q.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load file.\n Please make sure the file you are trying to load was generated with this tool.\n If it was, please file a bug report.");
                    MessageBox.Show(ex.Message);
                    throw ex;
                }

                AppGlobals.Instance.EditorCurrentLevel = x;

                Invalidate();

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppGlobals.Instance.CurrentPath == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                GameObjectFactory.Instance.SaveLevel(AppGlobals.Instance.EditorCurrentLevel, AppGlobals.Instance.CurrentPath);

            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q = new SaveFileDialog();
            q.CheckFileExists = false;
            q.AddExtension = true;

            q.DefaultExt = ".level";
            q.Filter = "Level Files (*.level)|*.level";
            //q.Multiselect = false;


            if (q.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {

                try
                {
                    GameObjectFactory.Instance.SaveLevel(AppGlobals.Instance.EditorCurrentLevel, q.FileName);
                    //x = GameObjectFactory.Instance.LoadLevel(q.FileName);

                    AppGlobals.Instance.CurrentPath = q.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to save file.");
                    MessageBox.Show(ex.Message);
                    throw ex;
                }

            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppGlobals.Instance.CurrentPath = "";
            AppGlobals.Instance.EditorCurrentLevel = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbAddEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupAddEntity();
        }

        private void SetupAddEntity()
        {
            var types = (from q in System.Reflection.Assembly.GetAssembly(typeof(GameInstance)).GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) && !q.IsAbstract select q).ToList();

            var t = (from tx in types where tx.Name == cmbAddEntity.SelectedItem.ToString() select tx).First();


            var attribs = from at in t.GetProperties()
                          where at.PropertyType == typeof(double)
                              || at.PropertyType == typeof(string)
                          select at;

            this.flwAddEntity.Controls.Clear();



            int r = 0;
            foreach (var ta in attribs)
            {
                //this.tblAttributes.RowStyles[r].SizeType = SizeType.Absolute;
                //this.tblAttributes.RowStyles[r].Height = 30;

                var lbl = new Label();
                lbl.Text = ta.Name;
                var txt = new TextBox();
                txt.Name = "txt" + ta.Name;

                this.flwAddEntity.Controls.Add(lbl);
                this.flwAddEntity.Controls.Add(txt);
                r += 1;
            }
        }

        private void btnAddEntity_Click(object sender, EventArgs e)
        {

            var types = (from q in System.Reflection.Assembly.GetAssembly(typeof(GameInstance)).GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) && !q.IsAbstract select q).ToList();

            var t = (from tx in types where tx.Name == cmbAddEntity.SelectedItem.ToString() select tx).First();

            var i = Activator.CreateInstance(t);

            var attribs = (from at in t.GetProperties()
                           where at.PropertyType == typeof(double)
                               || at.PropertyType == typeof(string)
                           select at).ToList();

            try
            {

                foreach (var a in attribs)
                {
                    var x = (from tx in this.flwAddEntity.Controls.Cast<Control>()
                             where tx is TextBox && (tx as TextBox).Name.Contains(a.Name)
                             select tx as TextBox).ToList();

                    if (a.PropertyType == typeof(double))
                        a.SetValue(i, double.Parse(x.First().Text), null);

                    if (a.PropertyType == typeof(string))
                        a.SetValue(i, x.First().Text, null);
                }

                AppGlobals.Instance.EditorCurrentLevel.StaticGameObjects.Add(i as StaticGameObject);

                foreach (var a in attribs)
                {
                    var x = (from tx in this.flwAddEntity.Controls.Cast<Control>()
                             where tx is TextBox && (tx as TextBox).Name.Contains(a.Name)
                             select tx as TextBox).ToList();

                    foreach (var y in x)
                    {
                        y.Text = "";
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to create Entity, please make sure that the fields make sense.\n" + ex.Message);
            }

        }

        private void glViewMain_MouseClick(object sender, MouseEventArgs e)
        {
            // pick 
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {


                var ww = this.glViewMain.Width;
                var wh = this.glViewMain.Height;

                var halfWindowWidth = glViewMain.Size.Width / 2;
                var halfWindowHeight = glViewMain.Size.Height / 2;

                var xBase = AppGlobals.Instance.BaseCursorPosition.X;
                var yBase = AppGlobals.Instance.BaseCursorPosition.Y;
                var scale = AppGlobals.Instance.Zoom;

                var cw = (ww / scale);
                var ch = (wh / scale);

                // new Vector2D(xBase - (halfWindowWidth / scale),
                    //yBase - (halfWindowHeight / scale)),

                /* var lastMousePos = UnProject(new Vector2D(lastMouseX, lastMouseY),
                new Vector2D(ww, wh),
                new Vector2D(xBase - (halfWindowWidth / scale),
                    yBase - (halfWindowHeight / scale)),
                new Vector2D(cw, ch));*/

                var levelChunks = from x in AppGlobals.Instance.EditorCurrentLevel.Chunks
                                  where Unicorn21.Geometry.Intersections.IsPointInPolygon(
                                  x.Area, UnProject(
                                    new Vector2D(e.X, e.Y),
                                    new Vector2D(glViewMain.Width, glViewMain.Height),
                                     new Vector2D(xBase - (halfWindowWidth / scale),
                    yBase - (halfWindowHeight / scale)),
                                    new Vector2D(cw, ch)))
                                  select x;

                LevelChunk lcHighest = null;
                double dblHighest = 0;

                if (this.cbxCorridors.Checked)
                {
                    var chunks = from lc in levelChunks
                                 where lc is Corridor
                                 orderby (lc as Corridor).FloorHeight ascending
                                 select lc as Corridor;

                    if (chunks.Count() > 0)
                    {
                        lcHighest = chunks.Last();
                        dblHighest = (lcHighest as Corridor).FloorHeight;

                    }
                }

                if (this.cbxPlatforms.Checked)
                {
                    var chunks = from lc in levelChunks
                                 where lc is Platform
                                 && (lc as Platform).FloorHeight >= dblHighest
                                 orderby (lc as Platform).FloorHeight ascending
                                 select lc as Platform;

                    if (chunks.Count() > 0)
                    {
                        if ((chunks.Last() as Platform).FloorHeight>= dblHighest)
                        {
                            lcHighest = chunks.Last();
                            dblHighest = (lcHighest as Platform).FloorHeight;
                        }

                    }
                }

                if (this.cbxWalls.Checked)
                {
                    var chunks = from lc in levelChunks
                                 where lc is Wall
                                 //orderby (lc as Wall).FloorHeight ascending
                                 select lc as Wall;

                    if (chunks.Count() > 0)
                    {
                        lcHighest = chunks.Last();
                        dblHighest = 0;
                    }
                }

                AppGlobals.Instance.EditorCurrentChunk = lcHighest;
                AppGlobals.Instance.TextureManipulatorWindow.SetChunk();
            }
            RedrawMainWindow();
        }

        private void cbxWalls_CheckedChanged(object sender, EventArgs e)
        {
            RedrawMainWindow();
        }

        private void cbxCorridors_CheckedChanged(object sender, EventArgs e)
        {
            RedrawMainWindow();
        }

        private void cbxPlatforms_CheckedChanged(object sender, EventArgs e)
        {
            RedrawMainWindow();
        }

        private void cbxEntities_CheckedChanged(object sender, EventArgs e)
        {
            RedrawMainWindow();
        }

       

    }
}
