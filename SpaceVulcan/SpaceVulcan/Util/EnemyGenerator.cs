using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Model.Enemies;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Util
{
    class EnemyGenerator
    {
        public static Enemy createGrunt(int x, int y, int dx, int dy, int type, double damage)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 5, ProjectileType.Laser, ProjectileDirection.South);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy grunt = new Enemy(sideOrigin, destination, 50, 5, 0.2, 0, 10, projectile, 1, EnemyType.grunt, 5.0);
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt"+type);
            return grunt;
        }
    }
}
