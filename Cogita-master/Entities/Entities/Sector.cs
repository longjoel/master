using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaTerrainObjects.Entities
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

        public List<Entities.SectorBlock> GetSectorBlocksFromSector( byte block)
        {
            var s = this;
            var sbl = new List<Entities.SectorBlock>();

            for (int x = 0; x < Entities.Sector.Width; x++)
            {
                for (int y = 0; y < Entities.Sector.Height; y++)
                {
                    for (int z = 0; z < Entities.Sector.Depth; z++)
                    {

                        if (s.GetBlockFromSector( x, y, z) > 0)
                        {
                            var nc = s.GetOpenFacesInSector( x, y, z);
                            if (nc.Count <= 6)
                            {
                                sbl.Add(new Entities.SectorBlock()
                                {
                                    X = x + s.XOffset,
                                    Y = y + s.YOffset,
                                    Z = z + s.ZOffset,
                                    Block = s.GetBlockFromSector( x, y, z),
                                    Faces = nc
                                });
                            }
                        }
                    }
                }
            }

            return sbl;
        }

        private static int GetBlockOffsetFromSector(int x, int y, int z)
        {
            return ((y % Entities.Sector.Height) * Entities.Sector.Width * Entities.Sector.Depth
                + (z % Entities.Sector.Depth) * Entities.Sector.Width
                + (x % Entities.Sector.Width));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="zOffset"></param>
        /// <returns></returns>
        public Sector(long xOffset, long yOffset, long zOffset)
        {
            Entities.Sector s = this;
            s.XOffset = xOffset;
            s.YOffset = yOffset;
            s.ZOffset = zOffset;

            s.IsDirty = false;

            s.Blocks = new byte[Entities.Sector.Width * Entities.Sector.Depth * Entities.Sector.Height];

           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        internal byte GetBlockFromSector( int x, int y, int z)
        {
            return Blocks[GetBlockOffsetFromSector(x, y, z)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="block"></param>
        public  void SetSectorBlock( int x, int y, int z, byte block)
        {
            var s = this;
            lock (s)
            {
                var f = GetBlockOffsetFromSector(x, y, z);
                s.Blocks[f] = block;
                s.IsDirty = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public  List<Entities.SectorBlockFaces> GetOpenFacesInSector( int x, int y, int z)
        {
            var s = this;
            var bf = new List<Entities.SectorBlockFaces>();

            if (x - 1 < 0 || GetBlockFromSector( x - 1, y, z) == 0)
                bf.Add(Entities.SectorBlockFaces.West);

            if (x + 1 >= Entities.Sector.Width || GetBlockFromSector( x + 1, y, z) == 0)
                bf.Add(Entities.SectorBlockFaces.East);

            if (y - 1 < 0 || GetBlockFromSector( x, y - 1, z) == 0)
                bf.Add(Entities.SectorBlockFaces.Bottom);

            if (y + 1 >= Entities.Sector.Height || GetBlockFromSector( x, y + 1, z) == 0)
                bf.Add(Entities.SectorBlockFaces.Top);

            if (z - 1 < 0 || GetBlockFromSector( x, y, z - 1) == 0)
                bf.Add(Entities.SectorBlockFaces.South);

            if (z + 1 >= Entities.Sector.Depth || GetBlockFromSector( x, y, z + 1) == 0)
                bf.Add(Entities.SectorBlockFaces.North);


            return bf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public  Entities.Sector GetHollowSectorFromSector()
        {
            var s = this;

            var ns = new Sector(s.XOffset,s.YOffset, s.ZOffset);
           


            for (int x = 0; x < Entities.Sector.Width; x++)
            {
                for (int y = 0; y < Entities.Sector.Height; y++)
                {
                    for (int z = 0; z < Entities.Sector.Depth; z++)
                    {
                        if (s.GetOpenFacesInSector(x, y, z).Count < 6)
                            ns.SetSectorBlock( x, y, z, s.GetBlockFromSector( x, y, z));
                    }
                }
            }

            return ns;
        }

    }
}
