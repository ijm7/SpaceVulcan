using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Players
{
    public class Player
    {
        public Player(int x, int y, int score, int speed, int damage, double fireRate, int armour, int shield, ProjectileType _projectileType)
        {
            this.x = x;
            this.y = y;
            this.score = score;
            this.speed = speed;
            this.damage = damage;
            this.fireRate = fireRate;
            this.armour = armour;
            this.shield = shield;
            this._projectileType = _projectileType;
        }
        int x { get; set; }
        int y { get; set; }
        int score { get; set; }
        int speed { get; set; }
        int damage { get; set; }
        double fireRate { get; set; }
        int armour { get; set; }
        int shield { get; set; }
        ProjectileType _projectileType { get; set; }
    }
}
