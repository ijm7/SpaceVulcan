using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Projectiles
{
    public class Projectile
    {
        public Projectile(int w, int h, int x, int y, int damage, ProjectileType projectileType)
        {
            this.w = w;
            this.h = h;
            this.x = x;
            this.y = y;
            this.damage = damage;
            this.projectileType = projectileType;
        }   
        int w { get; set; }
        int h { get; set; }
        int x { get; set; }
        int y { get; set; }
        int damage { get; set; }
        ProjectileType projectileType { get; set; }
    }
}
