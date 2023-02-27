using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
    public class Enemy
    {
        public int Hp;
        public int Attack;
        public string Name;
        private double Scale;
        bool isSpecialItemNeeded_silverDust;
        private mainChar p;
        public Enemy(string name,int hp, int attack,double scale)
        {
            Hp = hp;
            Attack = attack;
            Name = name;
            //This expensive item is needed for get track of enemy.
          //isSpecialItemNeeded_silverDust = name == "Unseen Enemy" ? true : false;
            this.Scale = scale;
        }

        public void hitEnemy(ref managePlayerAttr main)
        {
            main.getHitByEnemy(Attack);
        }

        public int getHp()
        {
            return Hp;
        }

        public int getAttack()
        {
            return Attack;
        }

        internal void getHitByHero(int attack)
        {
            Hp -= attack;
        }

        internal void changeStats(int emblems)
        {
            this.Attack += (int)(this.Attack * this.Scale);
            this.Hp += (int)(this.Hp * this.Scale);
        }
    }
}
