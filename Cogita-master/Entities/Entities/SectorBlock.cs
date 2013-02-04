using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaTerrainObjects.Entities
{
    public class SectorBlock
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public List<SectorBlockFaces> Faces { get; set; }

        public byte Block { get; set; }

        

        public  double[] GetVerticiesByFace(Entities.SectorBlockFaces face)
        {
            var sb = this;
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
