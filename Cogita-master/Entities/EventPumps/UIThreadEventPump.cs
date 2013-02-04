using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using System.Linq;
using System.Text;

namespace CogitaTerrainObjects.EventPumps
{
    public class UIThreadEventPump
    {

        private static readonly Lazy<UIThreadEventPump> _instance = new Lazy<UIThreadEventPump>(new Func<UIThreadEventPump>(() => new UIThreadEventPump()));

        public static UIThreadEventPump Instance { get { return _instance.Value; } }


        private ConcurrentQueue<Action> _actionsQueue;


        private UIThreadEventPump()
        {
            _actionsQueue = new ConcurrentQueue<Action>();
        }



        public void DoWork()
        {
            Action nextAction = null;
            if (_actionsQueue.TryDequeue(out nextAction))
            {
                nextAction();
            }

        }

        public void Add(Action queueAction)
        {
            _actionsQueue.Enqueue(queueAction);
        }
    }
}
