using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NahrwallEditor.custControls
{
    public partial class ctrlTextureAttribute : UserControl
    {

        public string Title
        {
            get { return this.grpAttributes.Text; }
            set { this.grpAttributes.Text = value; }
        }

        public double TextureScale
        {
            get
            {
                double o = 0.0;
                var r = double.TryParse(this.txtScale.Text, out o);
                return o;
            }

            set { this.txtScale.Text = value.ToString(); }
        }
        public double TextureRotation
        {
            get
            {
                double o = 0.0;
                var r = double.TryParse(this.txtRotation.Text, out o);
                return o;
            }

            set { this.txtRotation.Text = value.ToString(); }
        }
        public double TextureXOffset
        {
            get
            {
                double o = 0.0;
                var r = double.TryParse(this.txtXOffset.Text, out o);
                return o;
            }

            set { this.txtXOffset.Text = value.ToString(); }
        }
        public double TextureYOffset
        {
            get
            {
                double o = 0.0;
                var r = double.TryParse(this.txtYOffset.Text, out o);
                return o;
            }

            set { this.txtYOffset.Text = value.ToString(); }
        }
        public string TextureName
        {
            get
            {
                return this.cmbTexture.SelectedValue.ToString();
            }

            set { this.cmbTexture.SelectedValue = value.ToString(); }
        }

        public ctrlTextureAttribute()
        {
            TextureScale = 1;
            TextureRotation = 0;
            TextureXOffset = 0;
            TextureYOffset = 0;
            TextureName = "default";
            Title = "default";


            InitializeComponent();
        }

        public ctrlTextureAttribute(double scale, double rotation, double xoffset, double yoffset, string textureName, string title)
        {
            TextureScale = scale;
            TextureRotation = rotation;
            TextureXOffset = xoffset;
            TextureYOffset = yoffset;
            TextureName = textureName;
            Title = title;

            InitializeComponent();
        }
    }
}
