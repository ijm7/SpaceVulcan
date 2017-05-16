using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceVulcan.Model.Projectiles
{
    public class Projectile : ICloneable
    {
        public Projectile(Vector2 position, double damage, int speed, ProjectileType _projectileType, ProjectileDirection _projectileDirection, bool enemy)
        {
            this.position = position;
            this.damage = damage;
            this.speed = speed;
            this._projectileType = _projectileType;
            this._projectileDirection = _projectileDirection;
            this.enemy = enemy;
        }
        public Projectile(double damage, int speed, ProjectileType _projectileType, ProjectileDirection _projectileDirection, bool enemy)
        {
            this.damage = damage;
            this.speed = speed;
            this._projectileType = _projectileType;
            this._projectileDirection = _projectileDirection;
            this.enemy = enemy;
        }
        public Vector2 position { get; set; }
        public double damage { get; set; }
        public int speed { get; set; }
        public ProjectileType _projectileType { get; set; }
        public ProjectileDirection _projectileDirection { get; set; }
        public Texture2D sprite { get; set; }
        public bool enemy;
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (sprite.Width/5)*4, (sprite.Height/5)*4);
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
