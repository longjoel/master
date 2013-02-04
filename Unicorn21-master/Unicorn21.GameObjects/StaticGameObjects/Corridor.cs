using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;

namespace Unicorn21.GameObjects
{
    public class Corridor : LevelChunk
    {
        /// <summary>
        /// Ceiling Texture
        /// </summary>
        public LevelTextureAttribute CeilingTexture { get; set; }

        /// <summary>
        /// The Wall from the top down to the ceiling
        /// </summary>
        public LevelTextureAttribute CeilingWallTexture { get; set; }

        /// <summary>
        /// The Floor Texture
        /// </summary>
        public LevelTextureAttribute FloorTexture { get; set; }

        /// <summary>
        /// Wall from the floor to the bottom
        /// </summary>
        public LevelTextureAttribute FloorWallTexture { get; set; }


        // textures for the repeating floor and ceiling borders
        public string FloorBottomBorder { get; set; }
        public string CeilingBorder { get; set; }

        // the height of the floor and the ceiling
        public double FloorHeight { get; set; }
        public double CeilingHeight { get; set; }

        // handy self validation method
        public override bool IsValid()
        {
                throw new NotImplementedException();
        }


        //internal Corridor(string gameObjectId)
        //    : base(gameObjectId)
        //{
        //    CeilingTexture = new LevelTextureAttribute();
        //    CeilingWallTexture = new LevelTextureAttribute();
        //    FloorTexture = new LevelTextureAttribute();
        //    FloorWallTexture = new LevelTextureAttribute();
        //}

        internal Corridor()
            : base()
        {
            CeilingTexture = new LevelTextureAttribute();
            CeilingWallTexture = new LevelTextureAttribute();
            FloorTexture = new LevelTextureAttribute();
            FloorWallTexture = new LevelTextureAttribute();

        }

        

    }
}
