using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;

namespace Unicorn21.GameObjects
{
    
    public abstract class LevelChunk// : GameObject
    {
        [System.Xml.Serialization.XmlAttribute]
        public double FloorFriction { get; set; }
        
        public Polygon2D Area { get; set; }

        public abstract bool IsValid();
    
        
        //internal LevelChunk(string gameObjectId)
        //    //: base(gameObjectId)
        //{
        //    FloorFriction = 0.6;
        //}

        internal LevelChunk()
            //: base()
        {
            FloorFriction = 0.6;
        }

        

    }
}
