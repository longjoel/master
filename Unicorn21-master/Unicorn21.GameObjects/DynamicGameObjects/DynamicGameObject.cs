using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.Geometry;

namespace Unicorn21.GameObjects
{
    public abstract class DynamicGameObject : GameObject
    {

        public double Height { get; set; }


        public double Radius { get; set; }

        public double Vx { get; set; }
        public double Vy { get; set; }
        public double Vz { get; set; }

        public bool IsAirborne { get; set; }

        //public Geometry.Vector2D Position { get; set; }
        public Geometry.Vector2D Velocity { get { return new Geometry.Vector2D(Vx, Vy); } set { this.Vx = value.X; this.Vy = value.Y; } }
        //public double Z { get; set; }
        //public double vZ { get; set; }

        public double Angle { get; set; }

        internal DynamicGameObject()
            : base()
        {
        }

        internal DynamicGameObject(string gameObjectId)
            : base(gameObjectId)
        {

        }

        public void DoPhysics(double dt, Level l)
        {
            var xTest = this.X + ((this.Vx) * dt);
            var yTest = this.Y + ((this.Vy) * dt);

            bool hitx = false;
            foreach (var levelObject in l.Chunks)
            {
                if (Collisions.LevelChunkCollision(
                    new Circle2D(
                        new Vector2D(xTest, this.Y),
                        this.Radius),
                    this.Z, 
                    this.Height, 
                    levelObject))
                {
                    hitx = true;
                    break;
                }

            }

           
            bool hity = false;
            foreach (var levelObject in l.Chunks)
            {
                if (Collisions.LevelChunkCollision(new Circle2D(new Vector2D(this.X, yTest), this.Radius),
                   this.Z, this.Height, levelObject))
                {
                    hity = true;
                    break;
                }

            }

            if (!hitx) this.X += this.Velocity.X * dt;


            if (!hity) this.Y += this.Velocity.Y * dt;




            var corridors = (from x in l.Chunks where x is Corridor select x as Corridor).ToList();


            var intersectingCorridor = new List<Corridor>();
            foreach (var x in corridors)
            {
                if (Intersections.IsPointInPolygon(x.Area, this.Position))
                {
                    intersectingCorridor.Add(x);
                }
            }



            var ordered = (from x in intersectingCorridor orderby x.FloorHeight descending select x).ToList();

            LevelChunk highest = ordered.FirstOrDefault();


            var platforms = (from x in l.Chunks where x is Platform select x as Platform).ToList();
            var intersectingPlatforms = new List<Platform>();

            foreach (var x in platforms)
            {
                if (Intersections.IsPointInPolygon(x.Area, this.Position)
                    &&
                    (this.Z + this.Height / 2) > (x.FloorHeight)
                    )
                {
                       intersectingPlatforms.Add(x);
                }
            }

            var orderedPlatforms = (from x in intersectingPlatforms orderby x.FloorHeight descending select x).ToList();

            if (orderedPlatforms.Count > 0)
            {
                highest = orderedPlatforms.First();
            }


            double heightestHeight =-1000;

            if (highest is Corridor)
            {

                heightestHeight = (highest as Corridor).FloorHeight;

            }

            if (highest is Platform)
            {

                heightestHeight = (highest as Platform).FloorHeight;

            }

            this.IsAirborne = (this.Z > heightestHeight);

            if (!this.IsAirborne)
            {
                Console.WriteLine("XYZ: {0}, {1}, {2} -- highest: {3}", X, Y, Z, highest.GetType().Name);
                if (highest is Platform)
                {
                    Console.WriteLine("On Platform");
                }

                this.Vz = 0;
                this.Z = heightestHeight;

            }
            else
            {
                // gravity is in play!
                this.Vz += (l.Gravity * dt);
                this.Z += (this.Vz * dt);
            }


            this.Vx *= highest.FloorFriction;
            this.Vy *= highest.FloorFriction;
        }

    }
}
