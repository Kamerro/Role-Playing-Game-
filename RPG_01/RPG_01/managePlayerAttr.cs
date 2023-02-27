using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace RPG_01
{
    public class managePlayerAttr:mainChar
    {
        mainChar p;
        public managePlayerAttr(ref mainChar player)
        {
            p = player;
        }
        public void changeInt(int value)
        {
            p.intelligence += value;
            calculateInteligence(ref p);
            calculateBonusGold(ref p);
        }
        public void changeStr(int value)
        {
            p.strength += value;
            calculateAttack(ref p);
        }
        public void changeHlth(int value)
        {
            p.health += value;
            calculateHitpoints(ref p);
        }
        public void changeCvs(int value)
        {
            p.cleverness += value;
            calculateCleverness(ref p);
            calculateBonusRep(ref p);
        }
        public int getBonusRep()
        {
            return p.bonusReputation;
        }
        public int getBonusGold()
        {
            return p.bonusGold;
        }
        public int getInt()
        {
            return p.intelligence;
        }
        public int getStr()
        {
            return p.strength;
        }
        public int getHitPoints()
        {
            return p.hitPoint;
        }
        public int getCvs()
        {
            return p.cleverness;
        }
        public int getCleverness()
        {
            return p.cleverness_t;
        }
        public int getIntelligence()
        {
            return p.intelligence_t;
        }
        public int getAttack()
        {
            return p.attack;
        }
        public int getHp()
        {
            return p.hitPoint;
        }
        public void attackEnemy(ref Enemy enemy)
        {
            enemy.getHitByHero(getAttack());
        }
        public void regenerateHitPoints()
        {
            p.hitPoint = p.maxHealth;
        }


        public void getHitByEnemy(int damage)
        {
            p.hitPoint -= damage;
            mainChar.isAlive = p.hitPoint > 0 ? true : false;
        }

        internal void showAllAttributes()
        {
            Clear();
            WriteLine("---------------------------");
            WriteLine("Player raw attributes:");
            WriteLine("---------------------------");
            WriteLine($"Player strength:{p.strength}");
            WriteLine($"Player health:{p.health}");
            WriteLine($"Player intelligence:{getIntelligence()}");
            WriteLine($"Player cleverness:{getCleverness()}");
            WriteLine($"Player reputation:{p.reputation}");
            WriteLine("---------------------------");
            WriteLine("Player total attributes:");
            WriteLine("---------------------------");
            WriteLine($"Player gold:{p.gold}");
            WriteLine($"Player attack:{getAttack()}");
            WriteLine($"Player hitpoints:{getHp()}");
            WriteLine($"Player gold bonus:{p.bonusGold}");
            WriteLine($"Player reputation bonus:{p.bonusReputation}");
        }
    }
}
