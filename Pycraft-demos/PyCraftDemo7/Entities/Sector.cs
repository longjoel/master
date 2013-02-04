using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Entities
{
    public class Sector
    {
        public const int Width = 16;
        public const int Height = 16;
        public const int Depth = 16;

        public byte[] Blocks { get; set; }

        public long XOffset { get; set; }
        public long YOffset { get; set; }
        public long ZOffset { get; set; }

        public bool IsDirty { get; set; }
        

    }
}
