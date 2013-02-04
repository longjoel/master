using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace Pycraft.EventPumps
{
    public class UIThreadEventPump
    {
        SemaphoreSlim _easySem;

        private static readonly Lazy<UIThreadEventPump> _instance = new Lazy<UIThreadEventPump>(new Func<UIThreadEventPump>(() => new UIThreadEventPump()));

        public static UIThreadEventPump Instance { get { return _instance.Value; } }

        private UIThreadEventPump()
        {
            _actionsQueue = new Queue<Action>();
            _easySem = new SemaphoreSlim(2);
        }

        private Queue<Action> _actionsQueue;

        public void DoWork()
        {

              
            lock (_actionsQueue)
            {

                while(_actionsQueue.Any())
                {

                    var nextAction = _actionsQueue.Dequeue();

                    nextAction();
                }

            }
        }

        public void Add(Action queueAction)
        {
            lock (_actionsQueue)
            {
                if (!_actionsQueue.Any())
                    _actionsQueue.Enqueue(queueAction);
                else
                    Thread.Sleep(0);
            }
        }
    }
}
