using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.GameObjects
{
    public static class Collisions
    {
        public static bool LevelChunkCollision(Geometry.Circle2D c, double objectZ, double objectHeight, LevelChunk levelChunk)
        {
            if (levelChunk is Wall) return WallCollision(c, objectZ, objectHeight, levelChunk as Wall);
            if (levelChunk is Corridor) return CorridorCollision(c, objectZ, objectHeight, levelChunk as Corridor);
            if (levelChunk is Platform)
            {
                var r = PlatformCollision(c, objectZ, objectHeight, levelChunk as Platform);
               
                return r;
            }
            return false;
        }

        private static bool WallCollision(Geometry.Circle2D c, double objectZ, double objectHeight, Wall wall)
        {
            if (Geometry.Intersections.CirclePolygonIntersection(c,
                   wall.Area))
                return true;
            return false;
        }

        private static bool CorridorCollision(Geometry.Circle2D c, double objectZ, double objectHeight , Corridor corridor)
        {
            if (Geometry.Intersections.CirclePolygonIntersection(c,
                   corridor.Area))
            {
                if (objectZ >= corridor.FloorHeight - (objectHeight / 2)
                    &&
                    objectZ <= corridor.CeilingHeight - (objectHeight))
                    return false;
                return true;
            }
            return false;
        }

        private static bool PlatformCollision(Geometry.Circle2D c, double objectZ, double objectHeight, Platform platform)
        {
            if (!Geometry.Intersections.CirclePolygonIntersection(c,
                   platform.Area))
                return false;
            if (objectZ <= platform.FloorHeight - (objectHeight / 2)
                &&
                objectZ >= platform.FloorHeight - (objectHeight))

            //var playerTalest = (objectZ+objectHeight);
            //var playerShortest = objectZ+objectHeight/4;

            //if (InRange(playerTalest, platform.FloorHeight, platform.CeilingHeight) ||
            //    InRange(playerShortest, platform.FloorHeight, platform.CeilingHeight))
                return true;
            return false;
        }

        private static bool InRange(double test, double high, double low)
        {
            if (test <= high && test >= low)
                return true;
            return false;
        }
    }
}
