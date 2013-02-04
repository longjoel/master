using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    public class PlayerProjectile: BaseGameObject
    {
        public PlayerProjectile(float x, float y)
            : base(GameObjectEnum.PlayerProjectile, x, y, 32, 32)
        {
        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            return base.Intersect(baseObj);
        }

        public override void Process(double dt)
        {
            this.yPos -= dt * GameVars.shipVelocity * 1.5;
            base.Process(dt);
        }
    }
}
