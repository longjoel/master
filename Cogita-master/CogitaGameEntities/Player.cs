using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CogitaGameEntities
{
    public class Player : GameObject
    {
        public double EyeHeight { get; set; }
        public double Pitch { get; set; }
        public double Yaw { get; set; }
        public bool CanJump { get; set; }


        public Player(double x, double y, double z, double pitch, double yaw)
            : base()
        {
            X = x;
            Y = y;
            Z = z;

            EyeHeight = 1.7;
            Height = 1.8;
            Radius = 0.45;

            Pitch = pitch;
            Yaw = yaw;

            CanJump = false;
        }

        public void Move(double yaw, double velocity)
        {
            VX = Math.Cos(Math.PI * yaw / 180.0) * velocity;
            VZ = Math.Sin(Math.PI * yaw / 180.0) * velocity;
        }

        public override string ToString()
        {
            return string.Format("Player: Position({0},{1},{2}) Velocity({3}, {4}, {5}) Pitch({6}) Yaw({7}) ",
                X, Y, Z,
                VX, VY, VZ,
                Pitch, Yaw);
        }

        public FPSCamera Camera
        {
            get
            {
                return new FPSCamera()
                {
                    X = X,
                    Y = Y + EyeHeight,
                    Z = Z,
                    Pitch = Pitch,
                    Yaw = Yaw,

                    FOVY = Math.PI / 180.0 * 90.0,
                    Aspect = 1,

                    Near = 0.25,
                    Far = 100
                };

            }
        }
    }
}
