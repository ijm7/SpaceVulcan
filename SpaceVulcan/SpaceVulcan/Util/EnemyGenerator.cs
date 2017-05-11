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
        public static Enemy createBasicGrunt(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 3, ProjectileType.Missile, ProjectileDirection.South,true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
            Enemy grunt = new Enemy(sideOrigin, destination, 50, 1, damage, 0, 10, projectile, 1, EnemyType.grunt, 5.0);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt"+type);
            return grunt;
        }
    }
}
