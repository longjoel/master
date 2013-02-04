using System;
using System.Xml.Serialization;

namespace Unicorn21.Geometry
{
    public class Vector2D
    {
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D()
        {
            X = 0;
            Y = 0;
        }

        [XmlAttribute]
        public double X { get; set; }
        [XmlAttribute]
        public double Y { get; set; }

        public static Vector2D Zero
        {
            get { return new Vector2D(); }
        }

        public double Magnitude
        {
            get { return Math.Sqrt((X) * (X) + (Y) * (Y)); }
        }

        public Vector2D Normal
        {
            get { if(Magnitude == 0.0)  return Zero; 
                return new Vector2D(X/Magnitude, Y/Magnitude); }
        }

        public Vector2D Scale(double d)
        {
            return new Vector2D(this.X * d, this.Y * d);
        }

        public double Distance(Vector2D b)
        {
            Vector2D a = this;
            return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
        }

        public double Cross(Vector2D b)
        {
            var a = this;
            return (a.X * b.Y) - (a.Y * b.X);
        }

        public double Dot(Vector2D b)
        {
            var a = this;
            return (a.X * b.X + a.Y * b.Y);

        }
    }
}