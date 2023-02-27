using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
   public class Items
    {
        protected string Name;
        public int cost;

        public string GetName()
        {
            return Name;
        }
        public int getCost()
        {
            return cost;
        }
        public void setCost()
        {
            cost = cost / 2;
        }
    }
    public class EquipableItems:Items
    {
        public int weaponAttack;
        public int armorHealth;
        public int bonusInt;
        public int bonusCleverness;
        public int bonusStrength;
        public bool destroyable;
    }

    public class WhiteWeapon : EquipableItems
    {
        public WhiteWeapon(string Name,int attack=0,int intelligence=0,int cleverness=0,int strength=0,int cost=0)
        {
            this.Name = Name;
            this.weaponAttack = attack;
            this.bonusInt = intelligence;
            this.bonusCleverness = cleverness;
            this.bonusStrength = strength;
            this.destroyable = Name == "Orkish marashel" ? true : false;
            this.cost = cost;
        }
        public override string ToString() => $"Weapon {Name}" +
            $"{(cost > 0 ? " cost:" : "")}{(cost > 0 ? cost : "")}, attack:{weaponAttack}" +
            $"{(bonusInt > 0 ? " inteligence:" : "")}{(bonusInt > 0 ?bonusInt: "")}" +
            $"{(bonusCleverness > 0 ? " cleverness:" : "")}{(bonusCleverness > 0 ?bonusCleverness : "")}" +
            $"{(bonusStrength > 0 ? " strength:" : "")}{(bonusStrength > 0 ?bonusStrength : "")}.";
    }

    public class Armor : EquipableItems
    {
        public Armor(string Name, int health = 0, int intelligence = 0, int cleverness = 0, int strength = 0, int cost = 0)
        {
            this.Name = Name;
            this.armorHealth = health;
            this.bonusInt = intelligence;
            this.bonusCleverness = cleverness;
            this.bonusStrength = strength;
            this.cost = cost;
        }

        public override string ToString() => $"Armor {Name}" +
            $"{(cost > 0 ? " cost:" : "")}{(cost > 0 ? cost : "")}, health:{armorHealth}" +
            $"{(bonusInt > 0 ? " inteligence:" : "")}{(bonusInt > 0 ? bonusInt : "")}" +
            $"{(bonusCleverness > 0 ? " cleverness:" : "")}{(bonusCleverness > 0 ? bonusCleverness : "")}" +
            $"{(bonusStrength > 0 ? " strength:" : "")}{(bonusStrength > 0 ? bonusStrength : "")}.";
    }

    public class Ring : EquipableItems
    {
        public Ring(string Name, int intelligence = 0, int cleverness = 0, int strength = 0, int cost = 0)
        {
            this.Name = Name;
            this.bonusInt = intelligence;
            this.bonusCleverness = cleverness;
            this.bonusStrength = strength;
            this.cost = cost;
        }

        public override string ToString() => $"Ring {Name}" +
            $"{(cost > 0 ? " cost:" : "")}{(cost > 0 ? cost : "")}"+
            $"{(bonusInt > 0 ? " inteligence:" : "")}{(bonusInt > 0 ? bonusInt : "")}" +
            $"{(bonusCleverness > 0 ? " cleverness:" : "")}{(bonusCleverness > 0 ? bonusCleverness : "")}" +
            $"{(bonusStrength > 0 ? " strength:" : "")}{(bonusStrength > 0 ? bonusStrength : "")}.";
    }

}
