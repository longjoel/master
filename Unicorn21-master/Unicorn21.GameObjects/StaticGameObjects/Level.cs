using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Unicorn21.GameObjects
{
    public class Level// : GameObject
    {
        public double Gravity  {get;set;}

        private List<LevelChunk> _chunks;
        public List<LevelChunk> Chunks { get { return _chunks; } }

        private List<StaticGameObject> _gameObjects;
        public List<StaticGameObject> StaticGameObjects
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }



        [XmlIgnoreAttribute]
        public List<SpawnPoint> SpawnPoints
        {
            get
            {
                return
                    (from x in _gameObjects where x is SpawnPoint select x as SpawnPoint).ToList();
            }
        }



        public double BaseFloorHeight { get { return (from x in _chunks where x is Corridor select ((Corridor)x).FloorHeight).Min(); } }
        public double BaseCeilingHeight { get { return (from x in _chunks where x is Corridor select ((Corridor)x).CeilingHeight).Max(); } }



        internal Level()
            : base()
        {
            _chunks = new List<LevelChunk>();
            _gameObjects = new List<StaticGameObject>();

            Gravity = -9.8;
       
        }

    }
}
