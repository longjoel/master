using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;

using System.Xml;
using System.Xml.Serialization;

namespace Unicorn21.GameObjects
{
    public abstract class GameObject
    {
        [XmlAttribute]
        public string UniqueId { get; set; }

        [XmlAttribute]
        public double X { get; set; }
        
        [XmlAttribute]
        public double Y { get; set; }

        [XmlAttribute]
        public double Z { get; set; }

        [XmlAttribute]
        protected string _textureHandle;
        public string TextureHandle { get { return _textureHandle; } }

        [XmlIgnoreAttribute]
        public Vector2D Position { get { return new Vector2D(X, Y); } set { X = value.X; Y = value.Y; } }

        [XmlAttribute]
        protected bool _isVisible;
        public bool IsVisible { get { return _isVisible; } }

        internal GameObject()
        {
            UniqueId = new System.Guid().ToString();
        }

        internal GameObject(string id)
        {
            UniqueId = id;
        }
        
    }
}
