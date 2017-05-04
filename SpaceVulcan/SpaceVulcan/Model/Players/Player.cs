using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Players
{
    class Player
    {
        public Player(int score, Projectile weapon)
        {
            this.score = score;
            this.weapon = weapon;
            
        }
        int score { get; set; }
        Projectile weapon { get; set; }
    }
}
