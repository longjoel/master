using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.Commanders
{
    public static class SectorBlockCommander
    {

        public static List<Entities.SectorBlock> GetSectorBlocksFromSector(Entities.Sector s, byte block)
        {

            var sbl = new List<Entities.SectorBlock>();

            for (int x = 0; x < Entities.Sector.Width; x++)
            {
                for (int y = 0; y < Entities.Sector.Height; y++)
                {
                    for (int z = 0; z < Entities.Sector.Depth; z++)
                    {

                        if (SectorCommander.GetBlockFromSector(s, x, y, z) > 0)
                        {
                            var nc = SectorCommander.GetOpenFacesInSector(s, x, y, z);
                            if (nc.Count <= 6)
                            {
                                sbl.Add(new Entities.SectorBlock()
                                {
                                    X = x + s.XOffset,
                                    Y = y + s.YOffset,
                                    Z = z + s.ZOffset,
                                    Block = SectorCommander.GetBlockFromSector(s, x, y, z),
                                    Faces = nc
                                });
                            }
                        }
                    }
                }
            }

            return sbl;
        }

        public static double[] GetVerticiesByFace(Entities.SectorBlock sb, Entities.SectorBlockFaces face)
        {
            var X = sb.X;
            var Y = sb.Y;
            var Z = sb.Z;

            var vertexList = new List<double>();

            switch (face)
            {
                case Entities.SectorBlockFaces.West:
                    vertexList.AddRange(new List<double> 
                    { 0+X, 0+Y, 1+Z, 
                      0+X, 1+Y, 1+Z, 
                      0+X, 1+Y, 0+Z, 
                      0+X, 0+Y, 0+Z  });

                    break;

                case Entities.SectorBlockFaces.South:
                    vertexList.AddRange(new List<double>
                     { 1+X, 1+Y, 0+Z, 
                       1+X, 0+Y, 0+Z, 
                       0+X, 0+Y, 0+Z,
                       0+X, 1+Y, 0+Z });
                    break;

                case Entities.SectorBlockFaces.East:
                    vertexList.AddRange(new List<double> 
               
                      { 1+X, 0+Y, 0+Z,
                        1+X, 1+Y, 0+Z,
                        1+X, 1+Y, 1+Z, 
                        1+X, 0+Y, 1+Z });
                    break;

                case Entities.SectorBlockFaces.North:
                    vertexList.AddRange(new List<double> 
                    {  0+X, 1+Y, 1+Z, 
                       0+X, 0+Y, 1+Z, 
                       1+X, 0+Y, 1+Z, 
                       1+X, 1+Y, 1+Z });
                    break;

                case Entities.SectorBlockFaces.Bottom:
                    vertexList.AddRange(new List<double> 
                    {  1+X, 0+Y, 0+Z,
                       1+X, 0+Y, 1+Z,
                       0+X, 0+Y, 1+Z, 
                       0+X, 0+Y, 0+Z
                    });
                    break;

                case Entities.SectorBlockFaces.Top:
                    vertexList.AddRange(new List<double> 
                    {   0+X, 1+Y, 0+Z, 
                        0+X, 1+Y, 1+Z,
                        1+X, 1+Y, 1+Z, 
                        1+X, 1+Y, 0+Z });
                    break;
            }

            return vertexList.ToArray();
        }

        public static double[] GetTexCoordsByFace(Entities.SectorBlockFaces face)
        {
            var coords = new List<double>();
            double[] i = {
                             0.0 / 6.0, 
                             1.0 / 6.0, 
                             2.0 / 6.0, 
                             3.0 / 6.0, 
                             4.0 / 6.0, 
                             5.0 / 6.0, 
                             6.0 / 6.0 };

            switch (face)
            {
                case Entities.SectorBlockFaces.East:
                    coords.AddRange(new List<double> { 
                        i[1], 1, 
                        i[1], 0, 
                        i[0], 0, 
                        i[0], 1 });
                    break;

                case Entities.SectorBlockFaces.North:
                    coords.AddRange(new List<double> { 
                        i[1], 0, 
                        i[1], 1, 
                        i[2], 1, 
                        i[2], 0 });
                    break;

                case Entities.SectorBlockFaces.West:
                    coords.AddRange(new List<double> { 
                        i[3], 1, 
                        i[3], 0, 
                        i[2], 0, 
                        i[2], 1 });
                    break;

                case Entities.SectorBlockFaces.South:
                    coords.AddRange(new List<double> { 
                        i[3], 0, 
                        i[3], 1, 
                        i[4], 1, 
                        i[4], 0 });
                    break;

                case Entities.SectorBlockFaces.Top:
                    coords.AddRange(new List<double> { 
                        i[4], 1, 
                        i[4], 0, 
                        i[5], 0, 
                        i[5], 1 });
                    break;

                case Entities.SectorBlockFaces.Bottom:
                    coords.AddRange(new List<double> { 
                        i[5], 1, 
                        i[5], 0, 
                        i[6], 0, 
                        i[6], 1 });
                    break;

            }
            return coords.ToArray();
        }

    }
}
