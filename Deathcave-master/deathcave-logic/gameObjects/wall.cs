using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    public class ObstacleWall : BaseGameObject
    {
        public ObstacleWall(float x, float y)
            : base(GameObjectEnum.ObstacleWall,x,y, 32, 32)
        {
           
        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            // it's a wall, do nothing.

            return base.Intersect(baseObj);
        }

        public override void Process(double dt)
        {

            base.Process(dt);
        }
    }
}
