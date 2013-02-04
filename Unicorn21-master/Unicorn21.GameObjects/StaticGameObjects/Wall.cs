using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;
using System.Xml;

namespace Unicorn21.GameObjects
{
    [System.Xml.Serialization.XmlInclude(typeof(Wall))]
    public class Wall : LevelChunk
    {

        public LevelTextureAttribute WallTexture { get; set; }
        
        public string WallBottomBorderTexture { get; set; }
        public string WallTopBorderTexture { get; set; }

        //internal Wall(string gameObjectId)
        //    : base(gameObjectId)
        //{
        //    WallTexture = new LevelTextureAttribute();
            

        //}

        internal Wall()
            : base()
        {
            WallTexture = new LevelTextureAttribute();
        }

        // handy self validation method
        public override bool IsValid()
        {
                throw new NotImplementedException();
            
        }


    }
}
