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
        private int shield { get; set; }
        private int armour { get; set; }
        private int speed { get; set; }
        private int type { get; set; }
        private int location { get; set; }
        private Projectile weapon { get; set; }
    }
}
