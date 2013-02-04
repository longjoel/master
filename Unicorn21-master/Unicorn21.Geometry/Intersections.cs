using System;
using System.Collections.Generic;
using System.Linq;

namespace Unicorn21.Geometry
{
    public static class Intersections
    {
        public static bool CircleContainsVertex(Circle2D a, Vector2D b)
        {
            var l = new Line2D(a.Point, b);
            return l.Length < a.Radius;
        }

        public static bool CircleCircleIntersection(Circle2D a, Circle2D b)
        {
            var l = new Line2D(a.Point, b.Point);
            return l.Length < (a.Radius + b.Radius);
        }

        private static bool Between(double a, double b, double t)
        {
            if (t >= Math.Min(a, b) && t <= Math.Max(a, b))
                return true;
            return false;
        }

        public static bool BoundingBoxIntersection(Vector2D A1, Vector2D A2, Vector2D B1, Vector2D B2)
        {
            double x1min = Math.Min(A1.X, A2.X);
            double x1max = Math.Max(A1.X, A2.X);
            double y1min = Math.Min(A1.Y, A2.Y);
            double y1max = Math.Max(A1.Y, A2.Y);

            double x2min = Math.Min(B1.X, B2.X);
            double x2max = Math.Max(B1.X, B2.X);
            double y2min = Math.Min(B1.Y, B2.Y);
            double y2max = Math.Max(B1.Y, B2.Y);

            bool x1Overlapx2 = (Between(x2min, x2max, x1min) || Between(x2min, x2max, x1max));
            bool y1Overlapy2 = (Between(y2min, y2max, y1min) || Between(y2min, y2max, y1max));
            bool x2Overlapx1 = (Between(x1min, x1max, x2min) || Between(x1min, x1max, x2max));
            bool y2Overlapy1 = (Between(y1min, y1max, y2min) || Between(y1min, y1max, y2max));

            return (x1Overlapx2 || x2Overlapx1) && (y1Overlapy2 || y2Overlapy1);

        }

        public static Vector2D LineLineIntersection(Line2D l1, Line2D l2)
        {
           

           
            if (BoundingBoxIntersection(l1.A,l1.B, l2.A,l2.B))
            {
                // if the slopes are not null
                if (l1.Slope != null && l2.Slope != null)
                {
                    // and the slopes are not equal
                    if (Math.Abs((double)l1.Slope - (double)l2.Slope) < double.Epsilon)
                    {
                        return null;
                    }
                    // if the points form a bounding box

                    // solve for m1x+b1 = m2x+b2
                    // m1x = m2x + b2 - b1
                    // m1x - m2x = b2 - b1
                    // (m1 - m2)x = b2-b1

                    // y = mx+b
                    // m1x+b1 = m2x+b2
                    // m1x = m2x+b2-b1
                    // m1x - m2x = b2-b1
                    // (m1-m2)x = b2-b1
                    // x = (b2-b1)/(m1-m2)
                    // x = (b2-b1)/((dy1/dx1)-(dy2-dx2))

                    double? x = (l2.YIntercept - l1.YIntercept) / ((double)l1.Slope - (double)l2.Slope);

                    double? y = l1.Slope * x + l1.YIntercept;

                    if (x != null) if (y != null) return new Vector2D((double)x, (double)y);
                }

                else if (l1.Slope == null && l2.Slope != null)
                {
                    double? y = l2.Slope * l1.A.X + l2.YIntercept;
                    if (y != null) return new Vector2D(l1.A.X, (double)y);
                }

                else if (l1.Slope != null && l2.Slope == null)
                {
                    double? y = l1.Slope * l2.A.X + l1.YIntercept;
                    if (y != null) return new Vector2D(l2.A.X, (double)y);
                }
            }


            return null;
        }

        public static List<Vector2D> PolygonLineIntersection(Polygon2D polygon, Line2D intersectLine)
        {
            return
                polygon.Lines.Select(line => LineLineIntersection(line, intersectLine)).Where(point => point != null).
                    ToList();
        }

        public static bool PolygonPolygonIntersection(Polygon2D a, Polygon2D b)
        {
            return
                (from line2D in a.Lines
                 from line in b.Lines
                 where LineLineIntersection(line2D, line) != null
                 select line2D).Any();
        }

        // this is sloppy as fuck... but should work... rewrite later
        public static bool IsPointInPolygon(Polygon2D polygon, Vector2D point)
        {
            //double minX = polygon.Points[0].X;
            //double minY = polygon.Points[0].Y;
            //double maxX = polygon.Points[0].X;
            //double maxY = polygon.Points[0].Y;


            //for (int i = 1; i < polygon.Points.Count; i++)
            //{
            //    if (polygon.Points[i].X < minX)
            //        minX = polygon.Points[i].X;
            //    if (polygon.Points[i].Y < minY)
            //        minY = polygon.Points[i].Y;
            //    if (polygon.Points[i].X > maxX)
            //        maxX = polygon.Points[i].X;
            //    if (polygon.Points[i].Y > maxY)
            //        maxY = polygon.Points[i].Y;
            //}

            //var r = new Random();
            //var p = new Vector2D();
            //while (true)
            //{
            //    var rx = r.NextDouble() * double.MaxValue;
            //    var ry = r.NextDouble() * double.MaxValue;

            //    if ((minX <= rx && rx <= maxX) || (minY <= ry && ry <= maxY)) continue;
            //    p.X = rx;
            //    p.Y = ry;
            //    break;
            //}
            //var q = PolygonLineIntersection(polygon, new Line2D(p, point));
            //return q.Count % 2 != 0;

            List<Polygon2D> tris = polygon.Triangulate();

            foreach (Polygon2D polygon2D in tris)
            {
                if (PointInTriangle(point, polygon2D.Points[0], polygon2D.Points[1], polygon2D.Points[2]))
                    return true;
            }
            return false;
        }

        public static bool PointInTriangle(Vector2D p, Vector2D a, Vector2D b, Vector2D c)
        {
            return PointsOnSameSide(p, a, b, c) && PointsOnSameSide(p, b, a, c) && PointsOnSameSide(p, c, a, b);
        }

        private static bool PointsOnSameSide(Vector2D p1, Vector2D p2, Vector2D a, Vector2D b)
        {
            double cp1 = CrossProduct(VSub(b, a), VSub(p1, a));
            double cp2 = CrossProduct(VSub(b, a), VSub(p2, a));
            return (cp1 * cp2) >= 0; // they have the same sign if on the same side of the line
        }

        private static Vector2D VSub(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X - b.X, a.Y - b.Y);
        }

        private static Vector2D VAdd(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        private static double CrossProduct(Vector2D p1, Vector2D p2)
        {
            return (p1.X * p2.Y) - (p1.Y * p2.X);
        }

    //    private static int FindLineCircleIntersections(double cx, double cy, double radius,
    //Vector2D point1, Vector2D point2)
    //    {
    //        double dx, dy, A, B, C, det;

    //        dx = point2.X - point1.X;
    //        dy = point2.Y - point1.Y;

    //        A = dx * dx + dy * dy;
    //        B = 2 * (dx * (point1.X - cx) + dy * (point1.Y - cy));
    //        C = (point1.X - cx) * (point1.X - cx) + (point1.Y - cy) * (point1.Y - cy) - radius * radius;

    //        det = B * B - 4 * A * C;
    //        if ((A <= 0.0000001) || (det < 0))
    //        {

    //            return 0;
    //        }
    //        else if (det == 0)
    //        {

    //            return 1;
    //        }
    //        else
    //        {
    //            return 2;
    //        }
    //    }


    //    public static bool CirclePolygonIntersecton(Circle2D c, Polygon2D p)
    //    {
    //        foreach (var l in p.Lines)
    //        {
    //            var count = FindLineCircleIntersections(c.Point.X, c.Point.Y, c.Radius, l.A, l.B);
    //            if (count == 2)
    //                return true;
    //            else if (count == 1 && IsPointInPolygon(p, c.Point))
    //                return true;
    //        }
    //        if (IsPointInPolygon(p, c.Point))
    //            return true;

    //        return false;

    //    }

        public static bool CirclePolygonIntersection(Circle2D c, Polygon2D p)
        {
            if (IsPointInPolygon(p, c.Point))
                return true;

            foreach (var l in p.Lines)
            {
                if (CircleLineIntersection(c, l))
                    return true;
            }

            return false;

        }

        public static bool CircleLineIntersection(Circle2D c, Line2D l)
        {
            var a1 = new Vector2D(c.Point.X - c.Radius, c.Point.Y - c.Radius);
            var a2 = new Vector2D(c.Point.X + c.Radius, c.Point.Y + c.Radius);

            if (BoundingBoxIntersection(a1, a2, l.A, l.B))
            {
                var slope = l.Slope;
                double inverseSlope;

                if (slope == null || (double) slope == 0)
                {
                    // the bounding box already determined that an intersection has been made.
                    return true;
                }

                // slightly more complicated... need to create a tangent line. and find the distance.
                inverseSlope = 1 / (double)slope;

                
                // y= mx+b
                // b = circle.y, x = circle .x
                Line2D l2 = new Line2D();
                l2.A  = new Vector2D(c.Point.X, c.Point.Y);
                l2.B.X = c.Point.X + c.Radius;
                l2.B.Y = c.Point.Y + inverseSlope * (c.Point.X + c.Radius);
                var p = LineLineIntersection(l, l2);

                if (p != null)
                {
                    var d = new Line2D(c.Point, p);
                    if (d.Length < c.Radius)
                        return true;
                    return false;
                }

                Line2D l3 = new Line2D();
                l2.A = new Vector2D(c.Point.X, c.Point.Y);
                l2.B.X = c.Point.X - c.Radius;
                l2.B.Y = c.Point.Y + inverseSlope * (c.Point.X - c.Radius);
                var q = LineLineIntersection(l, l2);

                if (q != null)
                {
                    var d = new Line2D(c.Point, q);
                    if (d.Length < c.Radius)
                        return true;
                    return false;
                }

            }

            return false;
        }
    }
}