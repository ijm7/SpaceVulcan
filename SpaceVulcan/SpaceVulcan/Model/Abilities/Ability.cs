using SpaceVulcan.Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceVulcan.Model.Abilities
{
    public class Ability
    {
        double lastUsed { get; set; }
        double coolDown { get; set; }
        int identifier;
        bool isActive { get; set; }
        bool isAvailable { get; set; }
        public Ability(int identifier)
        {
            this.identifier = identifier;
            lastUsed = 0;
            isActive = false;
            isAvailable = true;
            coolDown = 34;

        }
        public int doubleDamage(int damage)
        {
            return damage * 2;
        }

        public int doubleSpeed(int speed)
        {
            return speed * 2;
        }

        public double armourRepair(double armour)
        {
            if (armour < 120)
            {
                return armour + 50;
            }
            else
            {
                return 170;
            }
        } 

        public double increaseShieldRenegerationRate(double regenerationRate)
        {
            return 1.0;
        }

        public double increaseFireRate(double fireRate)
        {
            return fireRate-0.2;
        }
    }
}
