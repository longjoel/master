using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.GameObjects
{
    public abstract class StaticGameObject:GameObject
    {
         internal StaticGameObject()
            : base()
        {
            _isVisible = false;
        }

        internal StaticGameObject(string gameObjectId)
            : base(gameObjectId)
        {
            _isVisible = false;
        }

      
    }
}
