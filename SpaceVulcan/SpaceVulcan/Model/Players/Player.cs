﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVulcan.Model.Abilities;
using SpaceVulcan.Model.Projectiles;
using System;
using System.Collections.Generic;

namespace SpaceVulcan.Model.Players
{
    public class Player : ICloneable
    {
        public Player(Vector2 position, int w, int h, int score, int speed, int damage, double fireRate, double armour, double shield, ProjectileType _projectileType, bool firing, float lastShot)
        {
            this.position = position;
            this.w = w;
            this.h = h;
            this.score = score;
            this.speed = speed;
            this.damage = damage;
            this.fireRate = fireRate;
            this.armour = armour;
            this.shield = shield;
            this._projectileType = _projectileType;
            this.firing = firing;
            this.lastShot = lastShot;
            regenerationRate = 0.01;
        }
        public List<Ability> abilityList { get; set; }
        public Texture2D sprite { get; set; }
        public Vector2 position { get; set; }
        public Player playerDefault { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int score { get; set; }
        public int speed { get; set; }
        public int damage { get; set; }
        public double fireRate { get; set; }
        public double armour { get; set; }
        public double shield { get; set; }
        public ProjectileType _projectileType { get; set; }
        public bool firing { get; set; }
        public float lastShot { get; set; }
        public double regenerationRate { get; set; }
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (sprite.Width/5)*2, (sprite.Height/5)*2);
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
