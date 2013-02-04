using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unicorn21.Geometry;

namespace Unicorn21.GameObjects.GameActions
{
    public class ActionMovePlayer : GameAction
    {
        public string PlayerID { get; set; }
        public Vector2D Velocity { get; set; }
        public bool IsJumping { get; set; }
       
        public ActionMovePlayer(Player p, Vector2D velocity, bool isJumping)
        {
            PlayerID = p.UniqueId;
            Velocity = velocity;
            IsJumping = isJumping;
        }

        // executed on the game instance
        internal override void DoAction( GameInstance gameInstance)
        {
            var plr =  (from p in gameInstance.LivingGameObjects  where p is Player && (p as Player).UniqueId == PlayerID select p as Player).First();

            plr.Velocity = new Vector2D(plr.Velocity.X + Velocity.Normal.Scale(Player.RunVelocity).X,
               plr.Velocity.Y + Velocity.Normal.Scale(Player.RunVelocity).Y);

            if (IsJumping)
            {
                if (!plr.IsAirborne)
                {
                    plr.Vz = 7;
                    plr.Z += .25;
                    plr.IsAirborne = true;
                }
            }



        }


    }
}
