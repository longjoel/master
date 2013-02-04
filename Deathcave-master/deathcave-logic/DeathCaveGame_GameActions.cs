using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deathcave_logic
{
    /// <summary>
    /// This partial class hides a bunch of the ugly implementation details.
    /// </summary>
    public partial class DeathCaveGame
    {
        private System.Drawing.RectangleF ProcessInput(InputEnum e, float dt)
        {
            System.Drawing.RectangleF newPosition = gv.ship.Position;

            // step 2, process player input.

            if ((e & InputEnum.Player1Up) == InputEnum.Player1Up)
                newPosition.Y -= dt * GameVars.shipVelocity;

            if ((e & InputEnum.Player1Down) == InputEnum.Player1Down)
                newPosition.Y += dt * GameVars.shipVelocity;

            if ((e & InputEnum.Player1Left) == InputEnum.Player1Left)
                newPosition.X -= dt * GameVars.shipVelocity;

            if ((e & InputEnum.Player1Right) == InputEnum.Player1Right)
                newPosition.X += dt * GameVars.shipVelocity;

            // spawn any new player shots.
            if ((e & InputEnum.Player1Fire) == InputEnum.Player1Fire)
            {
                this.gv.projectiles.Add(new gameObjects.PlayerProjectile(this.gv.ship.Position.X, this.gv.ship.Position.Y - 35));
            }

            return newPosition;

        }

        private bool ProcessPlayerCollisions(float dt, System.Drawing.RectangleF newPosition)
        {
            bool hit = false;
            bool enemyHit = false;

            // check to see if the player has colided with any of the walls, or other objects
            foreach (gameObjects.BaseGameObject b in this.gv.obstacles)
            {
                
                if (newPosition.IntersectsWith(b.Position) )
                {

                    // temporary until more logic is added.
                    hit = true;
                    
                }
            }

            // check to see if the player has collided with any of the enemies
            foreach (gameObjects.BaseGameObject b in this.gv.enemies)
            {
                if (gv.ship.Intersect(b))
                {
                    b.IsAlive = false;
                    gv.effects.Add(new gameObjects.Explosion(b.Position.X, b.Position.Y));

                    if (this.gv.safeTimer < 0.0f)
                    {
                        gv.effects.Add(new gameObjects.Explosion(gv.ship.Position.X, gv.ship.Position.Y));
                        enemyHit = true;

                    }
                   
                    break;
                }
            }

            //could also do a check to see if the player has collided with any explosions...


            // check to see if the player has run into any projectiles
            foreach (gameObjects.BaseGameObject b in this.gv.projectiles)
            {
                if (this.gv.ship.Intersect(b))
                {
                    if (this.gv.safeTimer < 0.0f)
                    {
                        gv.effects.Add(new gameObjects.Explosion(gv.ship.Position.X, gv.ship.Position.Y));
                        enemyHit = true;

                    }
                    break;
                }
            }


            // if the player has been hit at all, by something other than a wall, take off a life
            // and subtract a credit.
            if (enemyHit)
            {
                this.gv.playerCredits -= 1;
                this.StartRound();
            }
            return hit;
        }


        private void ProcessEnemies(float dt)
        {
            // step 4, run process on all the active game objects.
            foreach (gameObjects.BaseGameObject b in this.gv.enemies)
            {
                if (b.Position.IntersectsWith(gv.activeWindow))
                {

                    
                    bool hitWall = false;
                    gameObjects.EnemyDropper ed = null;
                    gameObjects.EnemyShooter es = null;
                    gameObjects.EnemySwooper ew = null;

                    b.Process(dt);
                    if (b.ObjectType == GameObjectEnum.EnemyDropper)
                    {
                        ed = (gameObjects.EnemyDropper)b;
                        
                    }
                    if (b.ObjectType == GameObjectEnum.EnemySwooper)
                    {
                        ew = (gameObjects.EnemySwooper)b;
                    }

                    foreach (gameObjects.BaseGameObject ob in this.gv.obstacles)
                    //if (b.Intersect(ob))
                    //{
                    //    gv.effects.Add(new gameObjects.Explosion(b.Position.X, b.Position.Y));
                    //    b.IsAlive = false;
                    //}
                    {
                       
                        if (!hitWall)
                        {
                            if (b.Intersect(ob))
                            {
                                hitWall = true;

                                if (b.ObjectType == GameObjectEnum.EnemyDropper)
                                {
                                    ed.vx = -ed.vx;
                                    ed.vy = -ed.vy;
                                }

                                if (b.ObjectType == GameObjectEnum.EnemySwooper)
                                {
                                    ew.vx = -ew.vx;
                                    ew.vy = -ew.vy;
                                }
                            }

                        }
                        
                    }

                    if (b.ObjectType == GameObjectEnum.EnemyShooter || b.ObjectType==GameObjectEnum.EnemySwooper)
                    {
                        if (this.gv.refireTime < 0)
                        {
                            this.gv.projectiles.Add(new gameObjects.EnemyProjectile(b.Position.X, b.Position.Y+35));
                        }
                    }

                    foreach (gameObjects.BaseGameObject ob in this.gv.projectiles)
                    {
                        if (b.Intersect(ob))
                        {
                            this.gv.effects.Add(new gameObjects.Explosion(b.Position.X, b.Position.Y));

                            ob.IsAlive = false;
                            b.IsAlive = false;
                            gv.playerScore += 100;
                        }
                    }
                }

               
                
            }

            if (gv.refireTime < 0)
                gv.refireTime += GameVars.refireRate;
            else
                gv.refireTime -= dt;
     
        }

        private void ProcessEffects(double dt)
        {

            foreach (gameObjects.BaseGameObject b in this.gv.effects)
                b.Process(dt);

        }

        private void ProcessPowerups(float dt)
        {
            foreach (gameObjects.BaseGameObject b in this.gv.powerups)
                b.Process(dt);
        }
     

        private void ProcessProjectiles(float dt)
        {
            
            for(int i = 0; i < this.gv.projectiles.Count; i += 1)
            {
                gameObjects.BaseGameObject b = this.gv.projectiles[i];
                if (b.Position.IntersectsWith(gv.activeWindow))
                {
                    b.Process(dt);
                    foreach (gameObjects.BaseGameObject ob in this.gv.obstacles)
                        if (b.Intersect(ob))
                        {
                            gv.effects.Add(new gameObjects.Explosion(b.Position.X, b.Position.Y));
                            b.IsAlive = false;
                        }

                }
                
                if (b.Position.Y < gv.activeWindow.Y || b.Position.Y > gv.activeWindow.Bottom)
                {
                    b.IsAlive = false;
                }
            }
        }

        // destroy unused objects
 
        private void CleanupDeadObjects()
        {
            // cleanup effects.
            for (int i = 0; i < gv.effects.Count; i += 1)
            {
                if (!gv.effects[i].IsAlive)
                {
                    gv.effects.Remove(gv.effects[i]);
                    i += 1;
                }
            }

            // clean up enemies
            for (int i = 0; i < gv.enemies.Count; i += 1)
            {
                if (!gv.enemies[i].IsAlive)
                {
                    gv.enemies.Remove(gv.enemies[i]);
                    i += 1;
                }
            }

            // cleanup obstacles (right now, objects don't die...)
            for (int i = 0; i < gv.obstacles.Count; i += 1)
            {
                if (!gv.obstacles[i].IsAlive)
                {
                    gv.obstacles.Remove(gv.obstacles[i]);
                    i += 1;
                }
            }

            // cleanup dead powerups
            for (int i = 0; i < gv.powerups.Count; i += 1)
            {
                if (!gv.powerups[i].IsAlive)
                {
                    gv.powerups.Remove(gv.powerups[i]);
                    i += 1;
                }
            }

            // cleanup projectile sprites
            for (int i = 0; i < gv.projectiles.Count; i += 1)
            {
                if (!gv.projectiles[i].IsAlive)
                {
                    gv.projectiles.Remove(gv.projectiles[i]);
                    i += 1;
                }
            }
    
        }
    }
}
