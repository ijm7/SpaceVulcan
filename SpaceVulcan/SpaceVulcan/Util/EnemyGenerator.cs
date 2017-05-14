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
            Enemy grunt = new Enemy(sideOrigin, destination, 50, 1, damage, 0, 10, projectile, type, EnemyType.grunt, 5.0);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt"+type);
            return grunt;
        }
        public static Enemy createBasicMassGrunt(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 5, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
            Enemy grunt = new Enemy(sideOrigin, destination, 75, 1, damage, 0, 25, projectile, type, EnemyType.grunt, 5.0);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt" + type);
            return grunt;
        }

        public static Enemy createFastGrunt(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 5, ProjectileType.Laser, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy grunt = new Enemy(sideOrigin, destination, 60, 10, damage, 0, 15, projectile,type, EnemyType.grunt, 5.0);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt"+type);
            return grunt;
        }

        public static Enemy createEasyBoss()
        {
            Vector2 origin = new Vector2(-200, -200);
            Vector2 destination = new Vector2(501, 20);
            Vector2 secondaryDestination = new Vector2(1021,20);
            Projectile projectile = new Projectile(40, 5, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
            Enemy easyBoss = new Enemy(origin, destination, secondaryDestination, true, 2000, 3, 40, 1, 500, projectile, 1, EnemyType.boss, 1);
            easyBoss.shots = 2;
            easyBoss.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/boss/level1boss");
            return easyBoss;
        }

        public static Enemy createEasyMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 6, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy medium = new Enemy(sideOrigin, destination, 400, 3, damage, 0, 100, projectile, type, EnemyType.medium, 3);
            medium.shots = shots;
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            return medium;
        }
    }
}
