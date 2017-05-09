using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Enemies
{
    public class Enemy
    {
        public Enemy(Vector2 position, Vector2 destination, int hp, int speed, double damage, int type, int score, Projectile projectile, int id, EnemyType _enemyType, double fireRate)
        {
            this.position = position;
            this.destination = destination;
            this.hp = hp;
            this.speed = speed;
            this.type = type;
            this.score = score;
            this.projectile = projectile;
            this.id = id;
            this._enemyType = _enemyType;
            this.fireRate = fireRate;
            firstDestination = false;
            secondaryDestination = new Vector2 (0);
            this.lastSpawn = 0;
            this.nextSpawn = 0;
            destroyed = false;
        }
        public Enemy(Vector2 position, Vector2 destination, Vector2 secondaryDestination, bool looping, int hp, int speed, double damage, bool firstDestination, int score, Projectile projectile, int id, EnemyType _enemyType, double fireRate)
        {
            this.position = position;
            this.destination = destination;
            this.secondaryDestination = secondaryDestination;
            this.looping = looping;
            this.hp = hp;
            this.speed = speed;
            this.type = type;
            this.score = score;
            this.firstDestination = firstDestination;
            this.projectile = projectile;
            this.id = id;
            this._enemyType = _enemyType;
            firstDestination = false;
            this.fireRate = fireRate;
            this.lastSpawn = 0;
            this.nextSpawn = 0;
            destroyed = false;
        }

        public Vector2 position { get; set; }
        public Vector2 destination { get; set; }
        public Vector2 secondaryDestination { get; set; }
        bool looping { get; set; }
        public int hp { get; set; }
        public int speed { get; set; }
        public int type { get; set; }
        public int score { get; set; }
        public bool firstDestination { get; set; }
        public Projectile projectile { get; set; }
        public int id { get; set; }
        public EnemyType _enemyType { get; set; }
        public Texture2D sprite { get; set; }
        public double lastSpawn { get; set; }
        public double nextSpawn { get; set; }
        public bool destroyed { get; set; }
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
        }
        public double fireRate { get; set; }
        public string dialog { get; set; }
    }
}
