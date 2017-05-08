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
        public static Enemy createGrunt(int x, int y, int dx, int dy)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(5, 5, ProjectileType.Laser, ProjectileDirection.South);
            Enemy grunt = new Enemy(sideOrigin, destination, 50, 5, 2, 0, 10, projectile, 1, EnemyType.grunt);
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt1");
            return grunt;
        }
    }
}
