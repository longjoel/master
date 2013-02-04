using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    /// <summary>
    /// 
    /// </summary>
     public class EnemyDropper :BaseGameObject
    {
         public float vx;
         public float vy;
         /// <summary>
         /// 
         /// </summary>
         /// <param name="x"></param>
         /// <param name="y"></param>
        public EnemyDropper(float x, float y)
            : base(GameObjectEnum.EnemyDropper, x, y, 32, 32)
        {
            vx = (float)(new Random((int)(y+x)).Next(4) - 2);
            vy = 1.0f;
        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="baseObj"></param>
         /// <returns></returns>
        public override bool Intersect(BaseGameObject baseObj)
        {
          
            return base.Intersect(baseObj);
        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="dt"></param>
        public override void Process(double dt)
        {
            this.yPos += vy * GameVars.shipVelocity * dt ;

            this.xPos += vx * Math.Sin(dt*30.0f);

            base.Process(dt);
        }
    }
}
