using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CogitaLoggingEngine;

using CogitaTerrainObjects.Entities;

namespace CogitaGameEntities
{
    public class CogitaGameInstance
    {
        private static readonly Lazy<CogitaGameInstance> _instance = new Lazy<CogitaGameInstance>(new Func<CogitaGameInstance>(() => new CogitaGameInstance()));

        public static CogitaGameInstance Instance { get { return _instance.Value; } }

        public Player Player { get; private set; }
        public Map Map { get; private set; }
        public MapCursor MapCursor { get; private set; }

        private CogitaGameInstance()
        {
            Logger.Data("Game Instance Created");

            Map = new Map();


            for (double x = -50; x <= 50; x++)
            {
                for (double z = -50; z <= 50; z++)
                {
                    var height = ((x * x) + (z * z)) / 256.0;

                    Map.SetBlock((long)x,
                        (long)height,
                        (long)z, 
                        BrickTypeEnum.Stone);
                    Map.SetBlock((long)x,
                       (long)height-1,
                       (long)z,
                       BrickTypeEnum.Stone);

                   Map.SetBlock((long)x, (long)32, (long)z, BrickTypeEnum.Oil);
                }
            }


            MapCursor = new MapCursor(Map, 0, 0, 0);

            Player = new Player(0, 16, 0, 0, 0);
        }

        private static bool GameObjectTouchingFloor(Map m, GameObject g)
        {
            return m.GetBlock((long)g.X, (long)(g.Y), (long)g.Z) > (byte)0 ||
                m.GetBlock((long)(g.X - g.Radius), (long)(g.Y), (long)(g.Z - g.Radius)) > (byte)0 ||
                m.GetBlock((long)(g.X - g.Radius), (long)(g.Y), (long)g.Z) > (byte)0 ||
                m.GetBlock((long)(g.X - g.Radius), (long)(g.Y), (long)(g.Z + g.Radius)) > (byte)0 ||
                m.GetBlock((long)g.X, (long)(g.Y), (long)(g.Z - g.Radius)) > (byte)0 ||
                m.GetBlock((long)g.X, (long)(g.Y), (long)(g.Z + g.Radius)) > (byte)0 ||
                m.GetBlock((long)(g.X + g.Radius), (long)(g.Y), (long)(g.Z - g.Radius)) > (byte)0 ||
                m.GetBlock((long)(g.X + g.Radius), (long)(g.Y), (long)g.Z) > (byte)0 ||
                m.GetBlock((long)(g.X + g.Radius), (long)(g.Y), (long)(g.Z + g.Radius)) > (byte)0;

            //var block =  m.GetBlock((long)g.X, (long)(g.Y), (long)g.Z);
            //return block > 0;

        }
        public void DoPhysics(double t)
        {
            var playerTouchingFloor = GameObjectTouchingFloor(Map, Player);

            if (playerTouchingFloor)
            {
                Player.CanJump = true;

                Player.VY = 0;
                Player.VX = Player.VX * 0.35;
                Player.VZ = Player.VZ * 0.35;
            }
            else
            {
                Player.VY = Player.VY - (9.8) * t;
            }

            var nx = Player.X + (Player.VX * t);
            var ny = Player.Y + (Player.VY * t);
            var nz = Player.Z + (Player.VZ * t);

            Player.X = nx;
            Player.Y = ny;
            Player.Z = nz;

            Logger.Data(Player.ToString());
        }
    }
}
