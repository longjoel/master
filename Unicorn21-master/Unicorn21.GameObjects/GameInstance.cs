using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.Geometry;

namespace Unicorn21.GameObjects
{
    public abstract class GameInstance
    {
        

        protected List<DynamicGameObject> _livingGameObjects;
        public List<DynamicGameObject> LivingGameObjects { get { return _livingGameObjects; } }


        public List<Player> Players
        {
            get
            {
                return (from x in _livingGameObjects where x is Player select x as Player).ToList();
            }
        }

        protected Queue<GameActions.GameAction> _gameActions;

        public void AddAction(GameActions.GameAction a)
        {
          
                _gameActions.Enqueue(a);
           
        }

        protected Level _level;
        public Level CurrentLevel { get { return _level; } }




        virtual public void DoGame(double dt)
        {
            // step 1, do game actions!
           
            while (_gameActions.Count > 0)
            {
                _gameActions.Dequeue().DoAction(this);

            }

            // step 2, do physics!

            foreach (var g in LivingGameObjects)
                g.DoPhysics(dt, CurrentLevel);

            // step 3, process collisions

            
        }

        public GameInstance(Level l)
        {
            _livingGameObjects = new List<DynamicGameObject>();
            _gameActions = new Queue<GameActions.GameAction>();
            _level = l;
        }

    }
}
