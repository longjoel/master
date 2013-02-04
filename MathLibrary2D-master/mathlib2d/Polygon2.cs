using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mathlib2d
{
    public class Polygon2
    {
        public List<Vector2> Points { get; set; }
        public List<LineSegment2> Segments
        {
            get
            {
                var ls = new List<LineSegment2>();
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    ls.Add(new LineSegment2() { A = Points[i], B = Points[i + 1] });
                }
                ls.Add(new LineSegment2() { A = Points[Points.Count - 1], B = Points[0] });
                return ls;
            }
        }

        public Tuple<Vector2, Vector2> BoundingBox
        {
            get
            {
                var minX = (from p in Points select p.X).Min();
                var maxX = (from p in Points select p.X).Max();

                var minY = (from p in Points select p.Y).Min();
                var maxY = (from p in Points select p.Y).Max();

                return new Tuple<Vector2, Vector2>(new Vector2() { X = minX, Y = minY }, new Vector2() { X = maxX, Y = maxY });

            }

        }

        public static bool PointInPolygon(Polygon2 poly, Vector2 point)
        {

            var ls = new LineSegment2() { A = point, B = new Vector2() { X = poly.BoundingBox.Item1.X, Y = point.Y } };

            var iCount = (from x in poly.Segments where LineSegment2.Intersection(ls, x) != null select x).Count() % 2;

            return iCount > 0;
        }

        public static bool PolygonInPolygon(Polygon2 A, Polygon2 B)
        {
            foreach (var p in A.Points)
            {
                if (PointInPolygon(B, p))
                    return true;
            }
            return false;
        }
    }
}
