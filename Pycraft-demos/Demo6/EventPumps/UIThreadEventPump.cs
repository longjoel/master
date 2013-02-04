using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pycraft.EventPumps
{
    public class UIThreadEventPump
    {
        
        private static readonly Lazy<UIThreadEventPump> _instance = new Lazy<UIThreadEventPump>(new Func<UIThreadEventPump>(() => new UIThreadEventPump()));

        public static UIThreadEventPump Instance { get { return _instance.Value; } }

        private UIThreadEventPump()
        {
            _actionsQueue = new Queue<Action>();
        }

        private Queue<Action> _actionsQueue;

        public void DoWork()
        {
            lock (_actionsQueue)
            {
                if (_actionsQueue.Count <= 0)
                    return;

                var nextAction = _actionsQueue.Dequeue();

                nextAction();
            }
        }

        public void Add(Action queueAction)
        {
            lock (_actionsQueue)
            {
                _actionsQueue.Enqueue(queueAction);
            }
        }
    }
}
