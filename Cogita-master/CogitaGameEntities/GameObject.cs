using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaGameEntities
{
    public abstract class GameObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double VX { get; set; }
        public double VY { get; set; }
        public double VZ { get; set; }
       
        public double Radius { get; set; }
        public double Height { get; set; }

        public GameObject()
        {
            X = Y = Z = VX = VY = VZ = 0;
        }

    }
}
