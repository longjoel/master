using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Entities
{
    public class SectorBlock
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public List<SectorBlockFaces> Faces { get; set; }

        public byte Block { get; set; }

    }
}
