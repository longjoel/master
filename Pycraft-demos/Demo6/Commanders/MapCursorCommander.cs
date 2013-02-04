using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Commanders
{
    public static class MapCursorCommander
    {
        public static Entities.MapCursor CreateMapCursorForMap(Entities.Map map, long x, long y, long z)
        {
            var mCursor = new Entities.MapCursor();

            mCursor.CursorMap = map;
            mCursor.VisibleSectors = new List<Tuple<Entities.Sector, Entities.SectorBlockBuffer>>();

            mCursor.X = x;
            mCursor.Y = y;
            mCursor.Z = z;

            return mCursor;
        }

        public static void MoveMapCursor(Entities.MapCursor cursor, long x, long y, long z)
        {
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
                            Commanders.SectorBlockBufferCommander.DisposeSectorBlockBuffer(s.Item2);
                            cursor.VisibleSectors.Remove(s);
                        }
                    }
                }));


            foreach (var s in sectorsToAdd)
            {
                EventPumps.UIThreadEventPump.Instance.Add(new Action(() =>
                {
                    cursor.VisibleSectors.Add(new Tuple<Entities.Sector, Entities.SectorBlockBuffer>(s.Item1,
                        Commanders.SectorBlockBufferCommander.CreateBlockBufferFromSectorByBlock(s.Item1, 1)));
                }));
            }


        }

        public static void UpdateMapCursor(Entities.MapCursor cursor)
        {
            var x = cursor.X;
            var y = cursor.Y;
            var z = cursor.Z;


            var minX = x - Entities.MapCursor.CursorRadius * Entities.Sector.Width;
            var maxX = x + Entities.MapCursor.CursorRadius * Entities.Sector.Width;

            var minY = y - Entities.MapCursor.CursorRadius * Entities.Sector.Height;
            var maxY = y + Entities.MapCursor.CursorRadius * Entities.Sector.Height;

            var minZ = z - Entities.MapCursor.CursorRadius * Entities.Sector.Depth;
            var maxZ = z + Entities.MapCursor.CursorRadius * Entities.Sector.Depth;

            List<Entities.Sector> sectors = new List<Entities.Sector>();

            lock (cursor.CursorMap.Sectors)
            {
                sectors.AddRange((from s in cursor.CursorMap.Sectors
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
                var toDelete = (from s in cursor.VisibleSectors where sectors.Contains(s.Item1) select s).ToList();

                lock (cursor.VisibleSectors)
                {
                    for (int i = toDelete.Count() - 1; i >= 0; i--)
                    {
                        var ss = toDelete[i];
                        SectorBlockBufferCommander.DisposeSectorBlockBuffer(ss.Item2);
                        cursor.VisibleSectors.Remove(ss);

                    }

                    for (int i = sectors.Count() - 1; i >= 0; i--)
                    {
                        var s = sectors[i];
                        s.IsDirty = false;

                        var n = new Tuple<Entities.Sector, Entities.SectorBlockBuffer>(s, Commanders.SectorBlockBufferCommander.CreateBlockBufferFromSectorByBlock(s, 1));
                        cursor.VisibleSectors.Add(n);
                    }
                }
            }));

        }

        public static void DrawCursor(Entities.MapCursor cursor)
        {
            for (int i = 0; i < 6; i++)
            {
                lock (cursor.VisibleSectors)
                {
                    foreach (var s in cursor.VisibleSectors)
                    {

                        SectorBlockBufferCommander.DrawSectorBlockBuffer(s.Item2, (Entities.SectorBlockFaces)i);
                    }
                }
            }
        }
    }
}
