using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace RPG_01
{
    public class mainChar
    {
        static public bool isAlive;
        private int Gold;
        private int Reputation;
        // Description of the properties:
        private int Intelligence;                            // 1 int + 1 % revard from quests. 
        private int Health;                                  // 1 Health = 1 hit points.
        private int Cleverness;                              // Cleverness allow to train intelligence, + 1% more reputation after quest
        private int Strength;                                // 1 Strength = 1 attack.
        private int Attack;                                  // Sum of attack from Strength and eq
        private int HitPoints;                               // sum of eq hp and hp from health.
        public int maxHealth;
        private int BonusReputation;
        private int Cleverness_t;
        private int Intelligence_t;
        private int BonusGold;
        public string[] itemsTab = new string[100];
        public List<Items> listOfItems = new List<Items>();
        public WhiteWeapon wearedWeapon = new WhiteWeapon("small knife",attack:5);
        public Armor wearedArmor = new Armor("black robe",health:10);
        public Pets wearedPet = new Pets(name:"rat");
        public Ring wearedRing = new Ring(Name:"broken ring");
        public mainChar()
        {
            Gold = 100;
            isAlive = true;
            Intelligence = 10;
            Strength = 10;
            attack = 15;
            //calc attack should be usable only in attr.
            Health = 10;
            hitPoint = 20;
            //same here
            Cleverness = 10;
            BonusGold = 10;
            BonusReputation = 10;
            maxHealth = HitPoints;
            Intelligence_t = intelligence;
            Cleverness_t = cleverness;
        }
        public string getSword()
        {
            return wearedWeapon.GetName();
        }
        protected virtual void calculateAttack(ref mainChar p)
        {
           p.Attack = p.wearedRing.weaponAttack+p.wearedWeapon.weaponAttack + p.Strength + p.wearedWeapon.bonusStrength;
        }

        protected virtual void calculateHitpoints(ref mainChar p)
        {
          p.maxHealth = p.wearedArmor.armorHealth+ p.wearedRing.armorHealth + p.Health;
          p.hitPoint = p.maxHealth;
        }

        protected virtual void calculateCleverness(ref mainChar p)
        {
            p.Cleverness_t = p.cleverness + p.wearedArmor.bonusCleverness + p.wearedRing.bonusCleverness + p.wearedWeapon.bonusCleverness;
        }

        protected virtual void calculateInteligence(ref mainChar p)
        {
            p.Intelligence_t = p.intelligence + p.wearedArmor.bonusInt + p.wearedRing.bonusInt + p.wearedWeapon.bonusInt;
        }
        protected virtual void calculateBonusGold(ref mainChar p)
        {
            p.bonusGold = p.Intelligence_t + p.wearedPet.getGoldBonus(); 
        }
        protected virtual void calculateBonusRep(ref mainChar p)
        {
            p.bonusReputation = p.Cleverness_t + p.wearedPet.getReputationBonus();
        }

        //accessing to properies only by in other classes.
        public int intelligence { get { return Intelligence; } set { Intelligence = value; } }
        public int strength { get { return Strength; } set { Strength = value; } }
        public int attack { get { return Attack; } set { Attack = value; } }
        public int hitPoint { get { return HitPoints; } set { HitPoints = value; } }
        public int cleverness { get { return Cleverness; } set { Cleverness = value; } }
        public int health { get { return Health; } set { Health = value; } }
        public int reputation { get { return Reputation; } set { Reputation = value; } }
        public int gold { get { return Gold; } set { Gold = value; } }
        public int bonusGold { get { return BonusGold; } set { BonusGold = value; } }
        public int bonusReputation { get { return BonusReputation; } set { BonusReputation = value; } }

        public int cleverness_t { get { return Cleverness_t; }}
        public int intelligence_t { get { return Intelligence_t; }}

    }
}
