using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mathlib2d
{
    public class LineSegment2
    {
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }

        public double DX { get { return B.X - A.X; } }
        public double DY { get { return B.Y - A.Y; } }

        public double Slope { get { return DY / DX; } }

        public double Base { get { return A.Y - Slope * A.X; } }

        public double Angle { get { return Vector2.Subtract(B, A).Angle; } }

        public List<Vector2> ToArray() { return new List<Vector2>() { A, B }; }

        public Tuple<LineSegment2, LineSegment2> Bisect(double x)
        {
            var C = new Vector2(){X = x, Y = Slope*x + Base};
            return new Tuple<LineSegment2, LineSegment2>(new LineSegment2() { A = A, B = C }, new LineSegment2() { A = C, B = B });
        }

        public static Vector2 Intersection(LineSegment2 L1, LineSegment2 L2)
        {
            var a = Math.Min(L1.A.X, L1.B.X);
            var b = Math.Max(L1.A.X, L1.B.X);
            var c = Math.Min(L2.A.X, L2.B.X);
            var d = Math.Max(L2.A.X, L2.B.X);

            if (c - b >= 0)
                return null;

            if ((b - a) == 0 && (d - c) == 0)
                return null;

            
            if ((b - a) == 0)
            {


                var s2 = L2.Slope;
                var b2 = L2.Base;

                double ty = s2 * b + b2;

                var miny = Math.Min(L1.A.Y, L1.B.Y);
                var maxy = Math.Max(L1.A.Y, L1.B.Y);

                if (miny <= ty && ty <= maxy) return new Vector2(){X=b, Y=ty};

                return null;

            }

            if ((d - c) == 0)
            {

                var s1 = L1.Slope;
                var b1 = L1.Base;

                double ty = s1 * d + b1;

                var miny = Math.Min(L2.A.Y, L2.B.Y);
                var maxy = Math.Max(L2.A.Y, L2.B.Y);

                if (miny <= ty && ty <= maxy) return new Vector2() { X = d, Y = ty };



                return null;
            }

            var slope1 = L1.Slope;
            var slope2 = L2.Slope;

            var base1 = L1.Base;
            var base2 = L2.Base;

            if (slope1 == slope2 && base1 != base2)
                return null;

            var xTest = (base2 - base1) / (slope1 - slope2);
            var yTest = slope1 * xTest + base1;

            var xMin = Math.Min(b, c);
            var xMax = Math.Max(b, c);


            if (xMin <= xTest && xTest <= xMax) return new Vector2() { X = xTest, Y = yTest };

            return null;
        }

        public static double AcuteIntersectionAngle(LineSegment2 L1, LineSegment2 L2)
        {
            return Math.Atan((L2.Slope - L1.Slope) / (1 + (L2.Slope * L1.Slope)));
        }

        public static double ObtuseIntersectionAngle(LineSegment2 L1, LineSegment2 L2)
        {
            return Math.PI - AcuteIntersectionAngle(L1, L2);
        }

        public static double ReflectionAngle(LineSegment2 L1, LineSegment2 L2)
        {
            return L2.Angle + AcuteIntersectionAngle(L1, L2);
        }

        public static Vector2 ReflectionUnitVector(LineSegment2 L1, LineSegment2 L2)
        {
            var angle = ReflectionAngle(L1, L2);
            return new Vector2() { X = Math.Sin(angle), Y = Math.Cos(angle) };
        }
    }
}
