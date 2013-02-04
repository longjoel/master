using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unicorn21.Geometry;
using Unicorn21.GameObjects;

using System.Xml;
using System.Xml.Serialization;

namespace Unicorn21.GameObjects
{
    
    public class GameObjectFactory
    {
        //private Dictionary<string, GameObject> _objects;
        
        internal GameObjectFactory()
        {
           // _objects = new Dictionary<string, GameObject>();
        }

        private static GameObjectFactory _instance;

        public static GameObjectFactory Instance
        {
            get { return _instance ?? (_instance = new GameObjectFactory()); }
        }

        public SpawnPoint CreateSpawnPoint(Vector2D position, double height, double initialAngle)
        {
            return new SpawnPoint(position,height, initialAngle);
        }

        public Player CreatePlayer(string playerHandle, SpawnPoint spawnPoint)
        {
            return new Player(spawnPoint.Position, spawnPoint.Z, spawnPoint.InitialAngle, playerHandle);
        }

        public Level CreateLevel()
        {
            return new Level();
        }

      
        public void SaveLevel(Level l, string path)
        {
            var types = from q in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) || q.IsSubclassOf( typeof(Unicorn21.GameObjects.LevelChunk)) select q;
            
            //var exTypes = from r in System.Reflection.Assembly.LoadFile(l.GameObjectsPath).GetTypes() where r.IsSubclassOf(typeof(Unicorn21.GameObjects.GameObject)) select r;

            //types = types.Union(exTypes);
            
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(l.GetType(), types.ToArray());
            x.Serialize(new System.IO.FileStream(path,System.IO.FileMode.Create), l);
        }

        public string SerializeLevel(Level l)
        {
            var types = 
                from q 
                in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() 
                where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) || q.IsSubclassOf(typeof(LevelChunk)) 
                select q;

            //try
            //{
            //    var exTypes =
            //        from r
            //            in System.Reflection.Assembly.LoadFile(l.GameObjectsPath).GetTypes()
            //        where r.IsSubclassOf(typeof(Unicorn21.GameObjects.GameObject))
            //        select r;

            //    types = types.Union(exTypes);
            //}
            //catch
            //{
            //}
            var sw = new System.IO.StringWriter();
            System.Xml.Serialization.XmlSerializer x = new 
                System.Xml.Serialization.XmlSerializer(l.GetType(), types.ToArray());
            
            x.Serialize(sw, l);
            return sw.ToString();
        }

        public Level DeserializeLevel(string s)
        {
            var types = from q in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) || q.IsSubclassOf(typeof(LevelChunk)) select q;
            //var exTypes = from r in System.Reflection.Assembly.LoadFile(l.GameObjectsPath).GetTypes() where r.IsSubclassOf(typeof(Unicorn21.GameObjects.GameObject)) select r;

            //var allTypes = types.Union(exTypes);

            var des = new System.Xml.Serialization.XmlSerializer(typeof(Level), types.ToArray());
            return des.Deserialize(new System.IO.StringReader(s)) as Level;

        }

        public Level LoadLevel(string path)
        {
            var types = from q in System.Reflection.Assembly.GetExecutingAssembly().GetTypes() where q.IsSubclassOf(typeof(Unicorn21.GameObjects.StaticGameObject)) || q.IsSubclassOf(typeof(LevelChunk)) select q;
            //var exTypes = from r in System.Reflection.Assembly.LoadFile(l.GameObjectsPath).GetTypes() where r.IsSubclassOf(typeof(Unicorn21.GameObjects.GameObject)) select r;

            //var allTypes = types.Union(exTypes);

            var des = new System.Xml.Serialization.XmlSerializer(typeof(Level), types.ToArray());
            return des.Deserialize(new System.IO.StreamReader(path)) as Level;
        }

        public Wall CreateWall(Polygon2D area)
        {
            var w = new Wall();

            w.Area = area;

            return w;

        }

        public Corridor CreateCorrdior(Polygon2D area, double floorHeight, double ceilingHeight)
        {
            var c = new Corridor();

            c.Area = area;
            c.FloorHeight = floorHeight;
            c.CeilingHeight = ceilingHeight;

            return c;
        }

        public Platform CreatePlatform(Polygon2D area, double ceilingHeight, double floorHeight)
        {
            var c = new Platform();

            c.Area = area;
            c.FloorHeight = floorHeight;
            c.CeilingHeight = ceilingHeight;

            return c;
        }
    }
}
