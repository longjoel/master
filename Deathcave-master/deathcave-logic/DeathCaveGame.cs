using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic
{

    public partial class DeathCaveGame
    {
        private GameVars gv;
        private List<string> strLevels;
        
        
        public DeathCaveGame()
        {
            this.gv = new GameVars();
            strLevels = new List<string>();
            strLevels.Add("levels/00.lvl");
            strLevels.Add("levels/01.lvl");
            strLevels.Add("levels/02.lvl");
            strLevels.Add("levels/03.lvl");
            strLevels.Add("levels/04.lvl");
            strLevels.Add("levels/05.lvl");
            

            this.StartGame();
            
            this.StartRound();

        }

        public void StartGame()
        {
            this.gv.level = 0;
            this.StartLevel(0);
            this.gv.playerCredits = GameVars.startPlayerCredits;
            this.gv.playerScore = 0;

        }

        public void StartLevel(int levelID)
        {

            levels.Level.BuildLevel(this.strLevels[levelID], ref this.gv);
            this.gv.activeWindow = new System.Drawing.RectangleF(0, this.gv.levelStartPosition-300, 800, 600);
            
        }

        public void StartRound()
        {
            //this.gv.activeWindow.Y += 256;
            //this.gv.ship.SetPosition(this.gv.ship.Position.X, this.gv.activeWindow.Y + 512);
            this.gv.safeTimer = GameVars.maxSafeTimer;

        }

        
        
        public GameVars Process(InputEnum e, float dt)
        {

           

            // step 1, scroll the window and figure out the new position.
            this.gv.activeWindow.Y -= dt * GameVars.scrollVelocity;

            System.Drawing.RectangleF newPosition = this.ProcessInput(e, dt);

            // step 2, do collisions against obstacles, etc.
            bool hit = this.ProcessPlayerCollisions(dt,  newPosition);

            if (!hit)
                this.gv.ship.SetPosition(newPosition.X, newPosition.Y);

            // woah, did we make it past the end of the level? 
            if (this.gv.ship.Position.Y <= 0)
            {
                if (this.gv.level < this.strLevels.Count)
                
                    this.gv.level += 1;
                else
                    this.gv.level = 0;

                this.StartLevel(gv.level);
            }

            if (this.gv.ship.Position.Y > this.gv.activeWindow.Bottom)
            {
                this.gv.playerCredits -= 1;
                this.StartLevel(this.gv.level);
            }

            if (this.gv.safeTimer > 0.0)
                this.gv.safeTimer -= dt;

            // process enemy movement and collision
            this.ProcessEnemies(dt);

            // process projectiles
            this.ProcessProjectiles(dt);
            
            // process powerups
            this.ProcessPowerups(dt);

            // process effects
            this.ProcessEffects(dt);
         
            this.CleanupDeadObjects();

            return this.gv;
        }


    }
}
