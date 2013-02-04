using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.GameObjects;

using Unicorn21.Geometry;

using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;

namespace Unicorn21.OpenTKRenderer
{
    public abstract class Camera
    {

        protected Player _player;
        

        public Camera(ref Player p)
        {
            _player = p;
        }

        public Vector2D Position { get { return _player.Position; } }
        public double Angle { get { return _player.Angle; } }
        public abstract void SetupCamera();
        public abstract void UseCamera();

    }
}
