using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DrawObjects();
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            DrawObjects();

        }

        private void DrawObjects()
        {
            textGrid1.Clear();
            for (int i = 1; i < textGrid1.Columns - 1; i++)
            {
                textGrid1.PutChar(i, 0, '#', Color.Red);
                textGrid1.PutChar(i, textGrid1.Rows - 1, '#', Color.Red);
            }

            for (int i = 1; i < textGrid1.Rows - 1; i++)
            {
                textGrid1.PutChar(0, i, '#', Color.Red);
                textGrid1.PutChar(textGrid1.Columns - 1, i, '#', Color.Red);
            }


        }
    }
}
