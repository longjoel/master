using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace deathcave_logic.levels
{
    /// <summary>
    /// Well, what else?
    /// </summary>
    public class Level
    {
        public static void BuildLevel(string levelPath, ref GameVars gv)
        {
            //List<string> levelData = new List<string>();

            StreamReader sr = new StreamReader(levelPath);
            int row = 0;

            gv.effects = new List<deathcave_logic.gameObjects.BaseGameObject>();
            gv.enemies = new List<deathcave_logic.gameObjects.BaseGameObject>();
            gv.obstacles = new List<deathcave_logic.gameObjects.BaseGameObject>();
            gv.powerups = new List<deathcave_logic.gameObjects.BaseGameObject>();
            
            while (!sr.EndOfStream)
            {
                string lineBuffer = sr.ReadLine().Substring(0, 25);
                int col = 0;
                foreach (char c in lineBuffer)
                {
                    // game obstacles
                    if (c == '#')
                        gv.obstacles.Add(new gameObjects.ObstacleWall(col * 32, row * 32));

                    // game enemies
                    if (c == 'V')
                        gv.enemies.Add(new gameObjects.EnemyDropper(col * 32, row * 32));
                    if (c == '$')
                        gv.enemies.Add(new gameObjects.EnemySwooper(col * 32, row * 32));
                    if (c == 'X')
                        gv.enemies.Add(new gameObjects.EnemyShooter(col * 32, row * 32));

                    // powerups
                    if (c == '"')
                        gv.powerups.Add(new gameObjects.PowerUpShield(col * 32, row * 32));
                    if (c == '%')
                        gv.powerups.Add(new gameObjects.PowerUpExtraLife(col * 32, row * 32));

                    if (c == '1')
                    {
                        gv.ship.SetPosition(col * 32, row * 32);

                        gv.levelStartPosition = row * 32;
                    }

                
                    col = col + 1;
                }

                row = row + 1;
            }

        }
    }
}
