using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;

using System.Xml.Serialization;

namespace Unicorn21.GameObjects
{

    // A simple spawn point class for placing players

    public class SpawnPoint : StaticGameObject
    {
        
        [XmlAttribute]
        public double InitialAngle { get; set; }

        public SpawnPoint(Vector2D position, double height , double initialAngle, string gameObjectId)
            : base(gameObjectId)
        {
            Position = position;
            Z = height;
            InitialAngle = initialAngle;

            _isVisible = true;

            _textureHandle = "SpawnPoint";
        }

        public SpawnPoint(Vector2D position, double height, double initialAngle)
            : base()
        {
            Position = position;
            Z = height;
            InitialAngle = initialAngle;

            _isVisible = true;

            _textureHandle = "SpawnPoint";
        }

        public SpawnPoint()
            : base()
        {
            Position = Vector2D.Zero;
            Z = 0;
            InitialAngle = 0;

            _isVisible = true;

            _textureHandle = "SpawnPoint";
        }
    }

}
