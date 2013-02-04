using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mathlib2d
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Angle { get { return Math.Atan2(Y, X); } }
        public double Magnitude { get { return Math.Sqrt((X * X) + (Y * Y)); } }

        public Vector2 UnitVector { get { return new Vector2() { X = X / Magnitude, Y = Y / Magnitude }; } }

        public List<double> ToList()
        {
            return new List<double>() { X, Y };
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }
        
        public static double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(){X = a.X+b.X, Y = a.Y+b.Y};
        }

        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2() { X = a.X - b.X, Y = a.Y - b.Y };
        }

        public static Vector2 Scale(Vector2 a, double b)
        {
            return new Vector2() { X = a.X * b, Y = a.Y * b };
        }
       
    }
}
