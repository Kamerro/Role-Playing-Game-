using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
   public class Quest
    {
        private int reputation;
        private int gold;
        private string name;
        public int neededEmblems;

        public Quest(string name, int rep, int gold,int emblems)
        {
            reputation = rep;
            this.gold = gold;
            this.name = name;
            this.neededEmblems = emblems;
        }
        public int getGold()
        {
            return gold;
        }
        public int getReputation()
        {
            return reputation;
        }
        public int calculateGainedReputation(int bonusPercentage)
        {
            return (int)((bonusPercentage * this.reputation) / 100) + this.reputation;
        }
        public int calculateGainedGold(int bonusPercentage)
        {
            return (int)((bonusPercentage * this.gold) / 100) + this.gold;
        }
        public string getName()
        {
            return name;
        }
    }

}
