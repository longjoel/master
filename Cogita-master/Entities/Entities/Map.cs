using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaTerrainObjects.Entities
{
    public class Map
    {
       
        public List<Sector> Sectors { get; set; }

        private static Tuple<long, long, long> GetSectorBase(long x, long y, long z)
        {
            var offset = GetSectorOffset(x, y, z);
            return new Tuple<long, long, long>(x - offset.Item1, y - offset.Item2, z - offset.Item3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static Tuple<int, int, int> GetSectorOffset(long x, long y, long z)
        {
            int x1 = (int)(x % 16);
            while (x1 < 0)
                x1 += 16;

            int y1 = (int)(y % 16);
            while (y1 < 0)
                y1 += 16;

            int z1 = (int)(z % 16);
            while (z1 < 0)
                z1 += 16;

            return new Tuple<int, int, int>(x1, y1, z1);
        }

        public Map()
        {
           
            Sectors = new List<Entities.Sector>();
           
        }

        public void SetBlock(long x, long y, long z, BrickTypeEnum bt)
        {
            var sBase = GetSectorBase(x, y, z);

            lock (Sectors)
            {

                var sector = (from sx in Sectors
                              where
                                  sx.XOffset == sBase.Item1
                                  && sx.YOffset == sBase.Item2
                                  && sx.ZOffset == sBase.Item3
                              select sx).FirstOrDefault();

                if (sector == null)
                {
                    sector = new Sector(sBase.Item1,
                        sBase.Item2,
                        sBase.Item3);
                    Sectors.Add(sector);
                }

                var sOffset = GetSectorOffset(x, y, z);

                sector.SetSectorBlock(sOffset.Item1, sOffset.Item2, sOffset.Item3, (byte)bt);
            }
        }


        public static void SetBlocks(Entities.Map m, List<Tuple<long, long, long, byte>> blocks)
        {

            foreach (var b in blocks)
            {
                var x = b.Item1;
                var y = b.Item2;
                var z = b.Item3;
                var block = b.Item4;

                var sBase = GetSectorBase(x, y, z);

                lock (m.Sectors)
                {

                    var sector = (from sx in m.Sectors
                                  where
                                      sx.XOffset == sBase.Item1
                                      && sx.YOffset == sBase.Item2
                                      && sx.ZOffset == sBase.Item3
                                  select sx).FirstOrDefault();

                    if (sector == null)
                    {
                        sector = new Sector(sBase.Item1,
                            sBase.Item2,
                            sBase.Item3);
                        m.Sectors.Add(sector);
                    }

                    var sOffset = GetSectorOffset(x, y, z);

                    sector.SetSectorBlock( sOffset.Item1, sOffset.Item2, sOffset.Item3, block);
                }
            }
        }



        public byte GetBlock( long x, long y, long z)
        {
            var m = this;
            var sBase = GetSectorBase(x, y, z);

            var sector = (from sx in m.Sectors
                          where
                              sx.XOffset == sBase.Item1
                              && sx.YOffset == sBase.Item2
                              && sx.ZOffset == sBase.Item3
                          select sx).FirstOrDefault();

            if (sector == null)
            {
                sector =  new Sector(sBase.Item1,
                    sBase.Item2,
                    sBase.Item3);
                m.Sectors.Add(sector);
            }

            var sOffset = GetSectorOffset(x, y, z);

            return sector.GetBlockFromSector( sOffset.Item1, sOffset.Item2, sOffset.Item3);
        }


    }
}
