using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    public class EnemySwooper : BaseGameObject
    {
        public float vx;
        public float vy;

        public EnemySwooper(float x, float y)
            : base(GameObjectEnum.EnemySwooper, x, y, 32, 32)
        {
            vx = (float)(new Random((int)(y + x)^2).Next(4) - 2);
            vy = 1.0f;
        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            return base.Intersect(baseObj);
        }

        public override void Process(double dt)
        {
            this.yPos += vy * GameVars.shipVelocity * dt;

            this.xPos += vx * Math.Sin(dt * 30.0f);
            base.Process(dt);
        } 
    }
}
