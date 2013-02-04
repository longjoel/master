using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.GameObjects
{
    public class LevelTextureAttribute
    {
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string TextureTag { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute]
        public double Rotation { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute]
        public double xScale { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute]
        public double yScale { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute]
        public double xOffset { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute]
        public double yOffset { get; set; }

        public LevelTextureAttribute()
        {
            TextureTag = "";
            Rotation = 0;
            xScale = 1;
            yScale = 1;
            xOffset = 0;
            yOffset = 0;

        }

    }
}
