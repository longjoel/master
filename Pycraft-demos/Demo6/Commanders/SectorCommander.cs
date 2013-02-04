using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Commanders
{
    /// <summary>
    /// 
    /// </summary>
    public static class SectorCommander
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int GetBlockOffsetFromSector(int x, int y, int z)
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
        public static Entities.Sector CreateSector(long xOffset, long yOffset, long zOffset)
        {
            Entities.Sector s = new Entities.Sector();
            s.XOffset = xOffset;
            s.YOffset = yOffset;
            s.ZOffset = zOffset;

            s.IsDirty = false;

            s.Blocks = new byte[Entities.Sector.Width * Entities.Sector.Depth * Entities.Sector.Height];

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static byte GetBlockFromSector(Entities.Sector s, int x, int y, int z)
        {
            return s.Blocks[GetBlockOffsetFromSector(x, y, z)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="block"></param>
        public static void SetSectorBlock(Entities.Sector s, int x, int y, int z, byte block)
        {
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
        public static List<Entities.SectorBlockFaces> GetOpenFacesInSector(Entities.Sector s, int x, int y, int z)
        {
            var bf = new List<Entities.SectorBlockFaces>();

            if (x - 1 < 0 || GetBlockFromSector(s, x - 1, y, z) == 0)
                bf.Add(Entities.SectorBlockFaces.West);

            if (x + 1 >= Entities.Sector.Width || GetBlockFromSector(s, x + 1, y, z) == 0)
                bf.Add(Entities.SectorBlockFaces.East);

            if (y - 1 < 0 || GetBlockFromSector(s, x, y - 1, z) == 0)
                bf.Add(Entities.SectorBlockFaces.Bottom);

            if (y + 1 >= Entities.Sector.Height || GetBlockFromSector(s, x, y + 1, z) == 0)
                bf.Add(Entities.SectorBlockFaces.Top);

            if (z - 1 < 0 || GetBlockFromSector(s, x, y, z - 1) == 0)
                bf.Add(Entities.SectorBlockFaces.South);

            if (z + 1 >= Entities.Sector.Depth || GetBlockFromSector(s, x, y, z + 1) == 0)
                bf.Add(Entities.SectorBlockFaces.North);


            return bf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Entities.Sector GetHollowSectorFromSector(Entities.Sector s)
        {

            var ns = new Entities.Sector();
            ns.XOffset = s.XOffset;
            ns.YOffset = s.YOffset;
            ns.ZOffset = s.ZOffset;


            for (int x = 0; x < Entities.Sector.Width; x++)
            {
                for (int y = 0; y < Entities.Sector.Height; y++)
                {
                    for (int z = 0; z < Entities.Sector.Depth; z++)
                    {
                        if (GetOpenFacesInSector(s, x, y, z).Count < 6)
                            SetSectorBlock(ns, x, y, z, GetBlockFromSector(s, x, y, z));
                    }
                }
            }

            return ns;
        }


        

    }
}
