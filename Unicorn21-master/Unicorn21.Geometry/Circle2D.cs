using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.Geometry
{
    public class Circle2D
    {
        public Vector2D Point { get; set; }
        public double Radius { get; set; }

        public Circle2D()
        {
            Point = new Vector2D();
            Radius = 0;
        }

        public Circle2D(Vector2D point, double radius)
        {
            Point = point;
            Radius = radius;
        }
    }
}
