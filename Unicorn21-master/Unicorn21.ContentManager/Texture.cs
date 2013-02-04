using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.ContentManager
{
    public class Texture
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Pixels { get; set; }
        public int BytesPerPixel { get; set; }

    }
}
