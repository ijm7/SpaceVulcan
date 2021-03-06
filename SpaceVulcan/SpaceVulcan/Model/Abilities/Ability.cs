﻿namespace SpaceVulcan.Model.Abilities
{
    public class Ability
    {
        public double lastUsed { get; set; }
        public double abilityTime;
        public double coolDown { get; set; }
        public int identifier;
        public bool isActive { get; set; }
        public bool isAvailable { get; set; }

        public Ability(int identifier)
        {
            setup(identifier);
        }
        public void setup(int identifier)
        {
            this.identifier = identifier;
            lastUsed = 0;
            isActive = false;
            isAvailable = true;
            coolDown = 20;
            abilityTime = 20;
        }
        public int doubleDamage(int damage)
        {
            return damage * 3;
        }

        public int doubleSpeed(int speed)
        {
            return speed * 2;
        }

        public double armourRepair(double armour)
        {
            if (armour < 150)
            {
                return armour + 20;
            }
            else
            {
                return 170;
            }
        } 

        public double increaseShieldRegenerationRate(double regenerationRate)
        {
            return 0.05;
        }

        public double increaseFireRate(double fireRate)
        {
            return fireRate-0.2;
        }
    }
}
