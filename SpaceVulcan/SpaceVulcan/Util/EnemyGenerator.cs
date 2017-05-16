using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Controller;
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
        //LEVEL1
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

        public static Enemy createEasyMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 3, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy medium = new Enemy(sideOrigin, destination, 400, 3, damage, 0, 100, projectile, type, EnemyType.medium, 4);
            medium.shots = shots;
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            return medium;
        }
        public static Enemy createEasyLoopingMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 origin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Vector2 secondaryDestination = new Vector2(0);
            Projectile projectile = new Projectile(damage, 3, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy medium = new Enemy(origin, destination, secondaryDestination, true, 750, 3, damage, type, 200, projectile, 1, EnemyType.medium, 4);
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            medium.secondaryDestination = new Vector2(GameArea.RIGHT - medium.boundingBox.Width - 1, dy);
            medium.shots = shots;
            return medium;
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

       
        //LEVEL2
        public static Enemy createMediumGrunt(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 5, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
            Enemy grunt = new Enemy(sideOrigin, destination, 55, 2, damage, 0, 30, projectile, type, EnemyType.grunt, 4.0);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt" + type);
            return grunt;
        }
        public static Enemy createMediumMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 6, ProjectileType.Missile, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
            Enemy medium = new Enemy(sideOrigin, destination, 900, 3, damage, 0, 500, projectile, type, EnemyType.medium, 3);
            medium.shots = shots;
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            return medium;
        }
        public static Enemy createMediumLoopingMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 origin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Vector2 secondaryDestination = new Vector2(0);
            Projectile projectile = new Projectile(damage, 6, ProjectileType.Missile, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
            Enemy medium = new Enemy(origin, destination, secondaryDestination, true, 1000, 3, damage, type, 600, projectile, 1, EnemyType.medium, 3);
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            medium.secondaryDestination = new Vector2(GameArea.RIGHT - medium.boundingBox.Width, dy);
            medium.shots = shots;
            return medium;
        }
        public static Enemy createMediumBoss()
        {
            Vector2 origin = new Vector2(-200, -200);
            Vector2 destination = new Vector2(501, 20);
            Vector2 secondaryDestination = new Vector2(1189, 20);
            Projectile projectile = new Projectile(40, 5, ProjectileType.Laser, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy mediumBoss = new Enemy(origin, destination, secondaryDestination, true, 5000, 3, 40, 1, 3000, projectile, 1, EnemyType.boss, 1);
            mediumBoss.shots = 3;
            mediumBoss.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/boss/level2boss");
            return mediumBoss;
        }
        //LEVEL 3
        public static Enemy createHardGrunt(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 7, ProjectileType.Laser, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Laser");
            Enemy grunt = new Enemy(sideOrigin, destination, 85, 3, damage, 0, 50, projectile, type, EnemyType.grunt, 3);
            grunt.shots = shots;
            grunt.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/grunt/grunt" + type);
            return grunt;
        }
        
        public static Enemy createHardMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 sideOrigin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Projectile projectile = new Projectile(damage, 10, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
            Enemy medium = new Enemy(sideOrigin, destination, 1200, 3, damage, 0, 700, projectile, type, EnemyType.medium, 3);
            medium.shots = shots;
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            return medium;
        }
        public static Enemy createHardLoopingMedium(int x, int y, int dx, int dy, int type, double damage, int shots)
        {
            Vector2 origin = new Vector2(x, y);
            Vector2 destination = new Vector2(dx, dy);
            Vector2 secondaryDestination = new Vector2(0);
            Projectile projectile = new Projectile(damage, 10, ProjectileType.MassDriver, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/MassDriver");
            Enemy medium = new Enemy(origin, destination, secondaryDestination, true, 1200, 3, damage, 0, 800, projectile, 1, EnemyType.medium, 3);
            medium.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/medium/medium" + type);
            medium.secondaryDestination = new Vector2(GameArea.RIGHT - medium.boundingBox.Width, dy);
            medium.shots = shots;
            return medium;
        }
        public static Enemy createHardBoss()
        {
            Vector2 origin = new Vector2(-200, -200);
            Vector2 destination = new Vector2(501, 20);
            Vector2 secondaryDestination = new Vector2(1058, 20);
            Projectile projectile = new Projectile(80, 8, ProjectileType.Missile, ProjectileDirection.South, true);
            projectile.sprite = Program.game.Content.Load<Texture2D>("Projectiles/Missile");
            Enemy hardBoss = new Enemy(origin, destination, secondaryDestination, true, 8000, 5, 40, 1, 2000, projectile, 1, EnemyType.boss, 0.5);
            hardBoss.shots = 3;
            hardBoss.sprite = Program.game.Content.Load<Texture2D>("EnemySprites/boss/level3boss");
            return hardBoss;
        }
    }
}
