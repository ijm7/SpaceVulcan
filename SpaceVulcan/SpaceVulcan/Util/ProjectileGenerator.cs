using Microsoft.Xna.Framework;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Util
{
    public static class ProjectileGenerator
    {
        public static Projectile getPlayerProjectile(Vector2 position, int damage, ProjectileType _projectileType)
        {
            Projectile projectile = new Projectile(position, damage, 5, _projectileType, ProjectileDirection.North);
            return projectile;
        }
    }
}
