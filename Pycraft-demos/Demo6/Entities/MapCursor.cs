using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Entities
{
    public class MapCursor
    {
        public const int CursorRadius = 12;

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public Map CursorMap { get; set; }

        public List<Tuple<Sector,SectorBlockBuffer>> VisibleSectors { get; set; }
       

    }
}
