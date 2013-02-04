using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.GameObjects
{
    public class Player : DynamicGameObject
    {
      
        public const double HeadHeight = 1.8;
        public const double WalkingVelocity = 3;
        public const double RunVelocity = 5;
    
        public double LookAngle { get; set; }

        public string PlayerHandle { get; set; }

        internal Player(string gameObjectId)
            : base(gameObjectId)
        {
            Radius = .55;
            Height = 2;
            
        }

        internal Player(Geometry.Vector2D position, double z, double angle, string handle, string gameObjectId)
            : base(gameObjectId)
        {
            Position = position;
            Z = z;
            Angle = angle;
            PlayerHandle = handle;
            Velocity = new Geometry.Vector2D(0, 0);
            Radius = .55;
            Height = 2;
        }

        internal Player()
            : base()
        {
            Radius = .55;
            Height = 2;
        }

        internal Player(Geometry.Vector2D position, double z, double angle, string handle)
            : base()
        {
            Position = position;
            Z = z;
            Angle = angle;
            PlayerHandle = handle;
            Velocity = new Geometry.Vector2D(0, 0);

            Radius = .55;
            Height = 2;
        }


    }
}
