using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;



namespace Unicorn21.GameObjects
{
    public class Platform : LevelChunk
    {

        public LevelTextureAttribute CeilingTexture { get; set; }


        public LevelTextureAttribute FloorTexture { get; set; }


        public LevelTextureAttribute WallTexture { get; set; }


        public double FloorHeight { get; set; }
        public double CeilingHeight { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }


        //internal Platform(string gameObjectId)
        //    : base(gameObjectId)
        //{
        //    CeilingTexture = new LevelTextureAttribute();
        //    FloorTexture = new LevelTextureAttribute();
        //    WallTexture = new LevelTextureAttribute();
        //}

        internal Platform()
            : base()
        {

            CeilingTexture = new LevelTextureAttribute();
            FloorTexture = new LevelTextureAttribute();
            WallTexture = new LevelTextureAttribute();
        }
    }
}
