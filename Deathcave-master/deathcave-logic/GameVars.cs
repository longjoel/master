using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic
{
    public  class GameVars
    {
        public const float scrollVelocity = 25.0f;
        public const float shipVelocity = 175.0f;

        public const float maxSafeTimer = 3.0f;
        public float safeTimer;

        public System.Drawing.RectangleF activeWindow;
       
        public int playerScore;

        public const int startPlayerCredits = 5;
        public int playerCredits;
        public int level;                   
        public int levelStartPosition;

        public gameObjects.playerShip ship;

        public List<gameObjects.BaseGameObject> obstacles;
        public List<gameObjects.BaseGameObject> enemies;
        public List<gameObjects.BaseGameObject> powerups;

        public List<gameObjects.BaseGameObject> effects;

        public List<gameObjects.BaseGameObject> projectiles;


        // this is for enemies that shoot projectiles
        public const float refireRate = 2.0f;        
        public float refireTime;


            
        public GameVars()
        {
            ship = new deathcave_logic.gameObjects.playerShip(0, 0);
            obstacles = new List<deathcave_logic.gameObjects.BaseGameObject>();
            enemies = new List<deathcave_logic.gameObjects.BaseGameObject>();
            powerups = new List<deathcave_logic.gameObjects.BaseGameObject>();
            effects = new List<deathcave_logic.gameObjects.BaseGameObject>();
            projectiles = new List<deathcave_logic.gameObjects.BaseGameObject>();
            safeTimer = 0.0f;

            this.refireTime = refireRate;

        }
    }
}
