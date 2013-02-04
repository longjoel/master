using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextGridControl
{
    public partial class TextGrid : UserControl
    {

        private Dictionary<Tuple<int, int>, Tuple<char, Color>> _chars;

        private int charWidth { get { return (int)Graphics.FromHwnd(this.Handle).MeasureString("a", Font).Width; } }
        private int charHeight { get { return Font.Height; } }

        public int Columns { get { return Width / charWidth; } }
        public int Rows { get { return Height / charHeight; } }

        public TextGrid()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Font = new Font(FontFamily.GenericMonospace, 12);

            _chars = new Dictionary<Tuple<int, int>, Tuple<char,Color>>();
        }

        public void Clear()
        {
            _chars = new Dictionary<Tuple<int, int>, Tuple<char, Color>>();
            Invalidate();
        }


        public void PutChar(int x, int y, char c, Color color)
        {
            _chars[new Tuple<int, int>(x, y)] = new Tuple<char, Color>(c, color);
            Invalidate();
        }

        public void PutStringH(int x, int y, string s, Color color)
        {
            for (int i = 0; i < s.Length; i++)
            {
                PutChar(x + i, y, s[i], color);
            }
        }

        public char GetChar(int x, int y)
        {
            if (_chars.Keys.Contains(new Tuple<int, int>(x, y)))
                return _chars[new Tuple<int, int>(x, y)].Item1;
            return '\0';
        }

        private void TextGrid_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            _chars.ToList().ForEach(c => e.Graphics.DrawString(c.Value.Item1.ToString(), 
                Font, 
                new SolidBrush(c.Value.Item2),
                new PointF(
                    (float)c.Key.Item1 * charWidth, 
                    (float)c.Key.Item2 * charHeight))); 
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }


    }
}
