using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Projectiles
{
    public class Projectile
    {
        public Projectile(Vector2 position, int damage, int speed, ProjectileType _projectileType, ProjectileDirection _projectileDirection)
        {
            this.position = position;
            this.damage = damage;
            this.speed = speed;
            this._projectileType = _projectileType;
            this._projectileDirection = _projectileDirection;
        }
        public Projectile(int damage, int speed, ProjectileType _projectileType, ProjectileDirection _projectileDirection)
        {
            this.damage = damage;
            this.speed = speed;
            this._projectileType = _projectileType;
            this._projectileDirection = _projectileDirection;
        }
        public Vector2 position { get; set; }
        public int damage { get; set; }
        public int speed { get; set; }
        public ProjectileType _projectileType { get; set; }
        public ProjectileDirection _projectileDirection { get; set; }
        public Texture2D sprite { get; set; }
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
        }
    }
}
