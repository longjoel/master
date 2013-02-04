using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    public class EnemyShooter:BaseGameObject
    {
       
        public EnemyShooter(float x, float y)
            : base(GameObjectEnum.EnemyShooter, x, y, 32, 32)
        {
            
        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            return base.Intersect(baseObj);
        }

        public override void Process(double dt)
        {
           
            base.Process(dt);
        }
    }
}
