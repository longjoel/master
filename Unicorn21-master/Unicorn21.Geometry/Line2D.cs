using System;

namespace Unicorn21.Geometry
{
    public class Line2D
    {
        public Line2D(Vector2D a, Vector2D b)
        {
            A = a;
            B = b;
        }

        public Line2D()
        {
            A = Vector2D.Zero;
            B = Vector2D.Zero;
        }

        public Vector2D A { get; set; }
        public Vector2D B { get; set; }

        public double Dx
        {
            get { return B.X - A.X; }
        }

        public double Dy
        {
            get { return B.Y - A.Y; }
        }

        public double? Slope
        {
            get { return Math.Abs(Dx) >= double.Epsilon ? (double?) (Dy/Dx) : null; }
        }

        public double Length
        {
            get { return A.Distance(B); }
        }


        public Vector2D Normal
        {
            get { return new Vector2D(Dx/Length, Dy/Length); }
        }


        // y = mx+b
        // x = (y-b)/m
        public double? XIntercept
        {
            // 0  = mx+b -> x = -b/m
            get
            {
                if (YIntercept != null)
                    return -YIntercept/Slope;
                return A.X;
            }
        }

        // y = mx+b
        // a.y = m*a.x + b
        // a.y - m*a.x = b
        // a.y - (dy/dx)*ax = b
        public double? YIntercept
        {
            get
            {
                if (Slope != null)
                    return A.Y - (Slope*A.X);
                return null;
            }
        }

        // cos (theta) = x / magnitude
        public double AngleInRadians
        {
            get { return Math.Acos(Dx/Length); }
        }

        public double AngleInDegrees
        {
            get { return AngleInRadians*180/Math.PI; }
        }

      


    }
}