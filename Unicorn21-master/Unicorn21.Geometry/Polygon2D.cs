using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml;
using System.Xml.Serialization;

namespace Unicorn21.Geometry
{
    public class Polygon2D
    {
        #region Polygon2DType enum

        public enum Polygon2DType
        {
            Convex,
            Concave
        }

        #endregion

        public Polygon2D(List<Vector2D> points)
        {
            Points = points;
        }

        public Polygon2D()
        {
            Points = new List<Vector2D>();
        }

        public List<Vector2D> Points { get; set; }

        [XmlIgnoreAttribute]
        public List<Vector2D> ClosedPoints
        {
            get
            {
                var p = new List<Vector2D>(Points);
                p.Add(Points[0]);
                return p;
            }
        }

        [XmlIgnoreAttribute]
        public Vector2D Center
        {
            get
            {
                var xs = (from x in Points select x.X);
                var xCenter = (xs.Min() + xs.Max()) / 2;
                var ys = (from y in Points select y.Y);
                var yCenter = (ys.Min() + ys.Max()) / 2;

                return new Vector2D(xCenter, yCenter);


            }
        }

        [XmlIgnoreAttribute]
        public List<Line2D> Lines
        {
            get
            {
                var lines = new List<Line2D>();
                if (Points == null)
                    return null;

                if (Points.Count < 3)
                    return null;


                for (int i = 0; i < Points.Count - 1; i++)
                {
                    lines.Add(new Line2D(Points[i], Points[i + 1]));
                }
                lines.Add(new Line2D(Points[Points.Count - 1], Points[0]));

                return lines;
            }
        }

        [XmlIgnoreAttribute]
        private bool IsSimple
        {
            get { throw new NotImplementedException(); }
        }

        [XmlIgnoreAttribute]
        private bool IsComplex
        {
            get { return !IsSimple; }
        }

        [XmlIgnoreAttribute]
        private bool IsConvex
        {
            get
            {
                if (SumOfAngles == null)
                    return false;

                return Math.Abs((double) SumOfAngles - 180) <= Double.Epsilon;
            }
        }

        [XmlIgnoreAttribute]
        private bool IsConcave
        {
            get
            {
                if (SumOfAngles == null)
                    return false;

                return Math.Abs((double) SumOfAngles - 180) >= Double.Epsilon;
            }
        }

        [XmlIgnoreAttribute]
        public double? SumOfAngles
        {
            get
            {
                if (Lines == null)
                    return null;

                return Lines.Sum(line => line.AngleInDegrees);
            }
        }

        [XmlIgnoreAttribute]
        public double Area
        {
            get
            {
                double area = 0;
                for (int i = 0; i < ClosedPoints.Count() - 1; i++)
                    area += ClosedPoints[i].X*ClosedPoints[i + 1].Y - ClosedPoints[i + 1].X*ClosedPoints[i].Y;
                return area/2;
            }
        }


        //
        private static double PolyArea(List<Vector2D> ptlist)
        {
            double area = 0;
            for (int i = 0; i < ptlist.Count() - 1; i++)
                area += ptlist[i].X*ptlist[i + 1].Y - ptlist[i + 1].X*ptlist[i].Y;
            return area/2;
        }

        // find the type, Concave or Convex, of a Simple polygon
        private static Polygon2DType PolyType(List<Vector2D> ptlist, float area)
        {
            int polysign = Math.Sign(area);

            for (int i = 0; i < ptlist.Count() - 2; i++)
            {
                if (Math.Sign(PolyArea(new List<Vector2D> {ptlist[i], ptlist[i + 1], ptlist[i + 2]})) != polysign)
                    return Polygon2DType.Concave;
            }
            return Polygon2DType.Convex;
        }

        // find the type of a specific vertex in a polygon, either Concave or Convex.
        public Polygon2DType VertexType(int vertexNo)
        {
            Polygon2D triangle;
            if (vertexNo == 0)
            {
                triangle =
                    new Polygon2D(new List<Vector2D>
                                      {ClosedPoints[ClosedPoints.Count - 2], ClosedPoints[0], ClosedPoints[1]});
                    // the polygon is always closed so the last point is the same as the first
            }
            else
            {
                triangle =
                    new Polygon2D(new List<Vector2D>
                                      {ClosedPoints[vertexNo - 1], ClosedPoints[vertexNo], ClosedPoints[vertexNo + 1]});
            }

            if (Math.Sign(triangle.Area) == Math.Sign(Area))
                return Polygon2DType.Convex;
            else
                return Polygon2DType.Concave;
        }

        public List<Polygon2D> Triangulate()
        {
            Polygon2D poly = new Polygon2D(this.Points);
            //poly.Points.Reverse();
            var triangles = new List<Polygon2D>(); // accumulate the triangles here
            // keep clipping ears off of poly until only one triangle remains
            while (poly.Points.Count > 3) // if only 3 points are left, we have the final triangle
            {
                int midvertex = FindEar(poly); // find the middle vertex of the next "ear"
                triangles.Add(new Polygon2D(new List<Vector2D>
                                  {
                                      poly.ClosedPoints[midvertex - 1], poly.ClosedPoints[midvertex],
                                      poly.ClosedPoints[midvertex + 1]
                                  }));
                // create a new polygon that clips off the ear; i.e., all vertices but midvertex
                var newPts = new List<Vector2D>(poly.Points);
                newPts.RemoveAt(midvertex); // clip off the ear
                poly = new Polygon2D(newPts); // poly now has one less point
            }
            // only a single triangle remains, so add it to the triangle list
            triangles.Add(poly);
            return triangles;
        }

        private static int FindEar(Polygon2D poly)
        {
            for (int i = 0; i < poly.ClosedPoints.Count - 2; i++)
            {
                if (poly.VertexType(i + 1) == Polygon2DType.Convex)
                {
                    // get the three points of the triangle we are about to test
                    Vector2D a = poly.ClosedPoints[i];
                    Vector2D b = poly.ClosedPoints[i + 1];
                    Vector2D c = poly.ClosedPoints[i + 2];
                    bool foundAPointInTheTriangle = false;
                        // see if any of the other points in the polygon are in this triangle
                    for (int j = 0; j < poly.Points.Count; j++)
                        // don't check the last point, which is a duplicate of the first
                    {
                        if (j != i && j != i + 1 && j != i + 2 && Intersections.PointInTriangle(poly.ClosedPoints[j], a, b, c))
                            foundAPointInTheTriangle = true;
                    }
                    if (!foundAPointInTheTriangle)
                        // the middle point of this triangle is convex and none of the other points in the polygon are in this triangle, so it is an ear
                        return i + 1; // EXITING HERE!
                }
            }
            throw new ApplicationException("Improperly formed polygon");
        }

       
    }
}