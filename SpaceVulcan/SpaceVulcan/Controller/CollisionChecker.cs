using Microsoft.Xna.Framework;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Controller
{
    public static class CollisionChecker
    {
        public static bool checkProjectileBounds(Projectile currentProjectile)
        {
            if (currentProjectile.boundingBox.Top > GameArea.BOTTOM)
            {
                return false;
            }
            else if (currentProjectile.boundingBox.Bottom < GameArea.TOP)
            {
                return false;
            }
            else if (currentProjectile.boundingBox.Right < GameArea.LEFT)
            {
                return false;
            }
            else if (currentProjectile.boundingBox.Left > GameArea.RIGHT)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
