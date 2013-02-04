using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Entities
{
    public class SectorBlockBuffer
    {
        public int[] VertexBufferArrays { get; set; }
        public int[] VertexBufferCounts { get; set; }
        public int[] TexBufferArrays { get; set; }
        public int[] TexBufferCounts { get; set; }
        public bool IsDisposed { get; set; }
    }
}
