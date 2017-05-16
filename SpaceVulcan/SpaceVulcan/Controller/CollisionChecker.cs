using SpaceVulcan.Model.Projectiles;

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
