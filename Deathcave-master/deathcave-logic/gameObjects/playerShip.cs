using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    public class playerShip:BaseGameObject
    {
        public playerShip(float x, float y) :
            base(GameObjectEnum.PlayerShip,x,y, 32, 32)
        {

        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            bool isCollission = base.Intersect(baseObj);

            if (isCollission)
            {
                
            }

            return isCollission;
        }

        public override void Process(double dt)
        {

            base.Process(dt);
        }
    }
}
