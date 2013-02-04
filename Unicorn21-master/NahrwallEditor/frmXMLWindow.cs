using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Unicorn21;
using Unicorn21.GameObjects;
using Unicorn21.Geometry;


namespace NahrwallEditor
{
    public partial class frmXMLWindow : Form
    {
        public frmXMLWindow()
        {
            InitializeComponent();
        }

        public void UpdateXML()
        {
            this.txtXML.Text = AppGlobals.Instance.EditorGameObjectFactory.SerializeLevel(AppGlobals.Instance.EditorCurrentLevel);

        }

        public void UpdateLevel()
        {
            AppGlobals.Instance.EditorCurrentLevel = AppGlobals.Instance.EditorGameObjectFactory.DeserializeLevel(this.txtXML.Text);
        }

        private void frmXMLWindow_VisibleChanged(object sender, EventArgs e)
        {
            this.txtXML.Text = AppGlobals.Instance.EditorGameObjectFactory.SerializeLevel(AppGlobals.Instance.EditorCurrentLevel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
              
                AppGlobals.Instance.EditorCurrentLevel = AppGlobals.Instance.EditorGameObjectFactory.DeserializeLevel(this.txtXML.Text);
                AppGlobals.Instance.MainWindow.RedrawMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML Entered is invalid./n/n" + ex.Message);
            }
        }
    }
}
