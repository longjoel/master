using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic.gameObjects
{
    /// <summary>
    /// 
    /// </summary>
    public abstract  class BaseGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        protected GameObjectEnum objType;
        
        /// <summary>
        /// 
        /// </summary>
        protected double xPos, yPos, width, height;

        protected bool isAlive;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectType"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BaseGameObject(GameObjectEnum ObjectType,float x, float y, float width, float height)
        {
            this.objType = ObjectType;
            this.xPos = x;
            this.yPos = y;
            this.width = width;
            this.height = height;

            this.isAlive = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.RectangleF Position
        {
            
            get { return new System.Drawing.RectangleF((float)xPos, (float)yPos, (float)width, (float)height); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseObj"></param>
        /// <returns></returns>
        public virtual bool Intersect(BaseGameObject baseObj)
        {
            return this.Position.IntersectsWith(baseObj.Position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public virtual void Process(double dt)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public GameObjectEnum ObjectType
        {
            get { return this.objType; }
        }

        public bool IsAlive
        {
            get { return this.isAlive; }
            set { this.isAlive = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(float x, float y)
        {
            this.xPos = x;
            this.yPos = y;

        }
    }
}
