using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaTerrainObjects.Entities
{
    public class MapCursor
    {
        public const int CursorRadius = 4;

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        private Map CursorMap { get; set; }

        public List<Tuple<Sector,SectorBlockBuffer>> VisibleSectors { get; set; }

        public MapCursor(Entities.Map map, long x, long y, long z)
        {
            var mCursor = this;

            mCursor.CursorMap = map;
            mCursor.VisibleSectors = new List<Tuple<Entities.Sector, Entities.SectorBlockBuffer>>();

            mCursor.X = x;
            mCursor.Y = y;
            mCursor.Z = z;

           
        }

        public void MoveMapCursor( long x, long y, long z)
        {
            var cursor = this;
            var map = cursor.CursorMap;

            // We only want the old clean sectors. The dirty ones should get caught up in the new pass.
            var oldSectors = (from s in cursor.VisibleSectors where s.Item1.IsDirty == false select s);

            var minX = x - Entities.MapCursor.CursorRadius * Entities.Sector.Width;
            var maxX = x + Entities.MapCursor.CursorRadius * Entities.Sector.Width;

            var minY = y - Entities.MapCursor.CursorRadius * Entities.Sector.Height;
            var maxY = y + Entities.MapCursor.CursorRadius * Entities.Sector.Height;

            var minZ = z - Entities.MapCursor.CursorRadius * Entities.Sector.Depth;
            var maxZ = z + Entities.MapCursor.CursorRadius * Entities.Sector.Depth;

            var newSectors = (from s in map.Sectors
                              where
                                (s.XOffset >= minX && s.XOffset < maxX) &&
                                (s.YOffset >= minY && s.YOffset < maxY) &&
                                (s.ZOffset >= minZ && s.ZOffset < maxZ)
                              select new Tuple<Entities.Sector,
                                  Entities.SectorBlockBuffer>(s, null));

            var intersection = newSectors.Intersect(oldSectors);

            var sectorsToAdd = (from s in newSectors where !intersection.Contains(s) select s);

            var sectorsToDelete = (from s in oldSectors where !intersection.Contains(s) select s);


            EventPumps.UIThreadEventPump.Instance.Add(new Action(() =>
            {
                lock (sectorsToDelete)
                {
                    foreach (var s in sectorsToDelete)
                    {
                        s.Item2.Dispose();
                        cursor.VisibleSectors.Remove(s);
                    }
                }
            }));


            foreach (var s in sectorsToAdd)
            {
                EventPumps.UIThreadEventPump.Instance.Add(new Action(() =>
                {
                    foreach(var bt in Enum.GetValues(typeof(BrickTypeEnum)))
                    {
                    cursor.VisibleSectors.Add(new Tuple<Entities.Sector, Entities.SectorBlockBuffer>(s.Item1,
                        new SectorBlockBuffer(s.Item1, (BrickTypeEnum)bt)));
                    }
                }));
            }


        }

        public  void Update()
        {
            var x = X;
            var y = Y;
            var z = Z;


            var minX = x - Entities.MapCursor.CursorRadius * Entities.Sector.Width;
            var maxX = x + Entities.MapCursor.CursorRadius * Entities.Sector.Width;

            var minY = y - Entities.MapCursor.CursorRadius * Entities.Sector.Height;
            var maxY = y + Entities.MapCursor.CursorRadius * Entities.Sector.Height;

            var minZ = z - Entities.MapCursor.CursorRadius * Entities.Sector.Depth;
            var maxZ = z + Entities.MapCursor.CursorRadius * Entities.Sector.Depth;

            List<Entities.Sector> sectors = new List<Entities.Sector>();

            lock (CursorMap.Sectors)
            {
                sectors.AddRange((from s in CursorMap.Sectors
                                  where
                                    (s.XOffset >= minX && s.XOffset < maxX) &&
                                    (s.YOffset >= minY && s.YOffset < maxY) &&
                                    (s.ZOffset >= minZ && s.ZOffset < maxZ) &&

                                       s.IsDirty
                                  select s).ToList());
            }
            if (sectors == null || sectors.Count() == 0)
                return;



            EventPumps.UIThreadEventPump.Instance.Add(new Action(() =>
            {

                // dispose of all the elements that are going to be redrawn
                var toDelete = (from s in VisibleSectors where sectors.Contains(s.Item1) select s).ToList();

                lock (VisibleSectors)
                {
                    for (int i = toDelete.Count() - 1; i >= 0; i--)
                    {
                        var ss = toDelete[i];
                        ss.Item2.Dispose();
                        VisibleSectors.Remove(ss);

                    }

                    for (int i = sectors.Count() - 1; i >= 0; i--)
                    {
                        var s = sectors[i];
                        s.IsDirty = false;

                        foreach (var bt in Enum.GetValues(typeof(BrickTypeEnum)))
                        {

                            var n = new Tuple<Entities.Sector, Entities.SectorBlockBuffer>(s, new SectorBlockBuffer(s, (BrickTypeEnum)bt));
                            VisibleSectors.Add(n);
                        }
                    }
                }
            }));

        }

        public  void Draw(BrickTypeEnum brickType)
        {
            var cursor = this;

            for (int i = 0; i < 6; i++)
            {
                lock (cursor.VisibleSectors)
                {
                    foreach (var s in cursor.VisibleSectors)
                    {
                        if(s.Item2.BrickType == brickType)
                            s.Item2.DrawSectorBlockBuffer((Entities.SectorBlockFaces)i);
                    }
                }
            }
        }
       

    }
}
