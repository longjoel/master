using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    class Explosion: BaseGameObject
    {
        private float timeLeft;
        public Explosion(float x, float y)
            : base(GameObjectEnum.EffectExplosion, x, y, 32, 32)
        {
            this.timeLeft = 0.5f;
        }

        public override bool Intersect(BaseGameObject baseObj)
        {
            return base.Intersect(baseObj);
        }

        public override void Process(double dt)
        {
            if (this.timeLeft > 0.0)
            {
                this.timeLeft -= (float)dt;
            }
            else
                this.xPos = 900;    // can't self terminate, so move off the display screen.
                                    // this is pretty hax though. In future implementations, 
                                    // implement mark and sweep or something. Flag for 
                                    // termination.

            base.Process(dt);
        }
    }
}
