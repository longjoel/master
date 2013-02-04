﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    /// <summary>
    /// 
    /// </summary>
    public  class EnemyProjectile: BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public EnemyProjectile(float x, float y)
            : base(GameObjectEnum.EnemyProjectile, x, y, 32, 32)
        {

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
            this.yPos += dt * GameVars.shipVelocity * 1.5;
            base.Process(dt);
            base.Process(dt);
        }
    }
}