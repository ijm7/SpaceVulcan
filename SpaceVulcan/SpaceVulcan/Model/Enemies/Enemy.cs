using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Enemies
{
    class Enemy
    {
        public Enemy(int shield,int armour,int speed, int type)
        {
            this.shield = shield;
            this.armour = armour;
            this.speed = speed;
            this.type = type;
        }
        int shield { get; set; }
        int armour { get; set; }
        int speed { get; set; }
        int type { get; set; }
        int location { get; set; }
        Projectile weapon { get; set; }
    }
}
