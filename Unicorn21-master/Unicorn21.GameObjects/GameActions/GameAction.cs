using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unicorn21.GameObjects.GameActions
{
    public abstract class GameAction
    {
        internal abstract void DoAction(GameInstance gameInstance);
    }
}
