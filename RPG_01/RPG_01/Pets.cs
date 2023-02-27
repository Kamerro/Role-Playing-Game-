using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
    public class Pets:Items
    {
        // in percentage
        private int bonusGold;
        private int bonusReputation;
        public Pets(string name, int goldBonus=0, int reputationBonus=0, int cost = 0)
        {
            this.Name = name;
            this.bonusGold = goldBonus;
            this.bonusReputation = reputationBonus;
            this.cost = cost;
        }

        public int getGoldBonus()
        {
            return this.bonusGold;
        }
        public int getReputationBonus()
        {
            return this.bonusReputation;
        }

        public override string ToString() => $"Pet {Name}" +
            $"{(cost > 0 ? " cost:" : "")}{(cost > 0 ? cost : "")}" +
            $"{(bonusReputation > 0 ? " Reputation bonus:" : "")}{(bonusReputation > 0 ? bonusReputation+"%" : "")}" +
            $"{(bonusGold > 0 ? " Gold bonus:" : "")}{(bonusGold > 0 ? bonusGold+"%" : "")}";
    }
}
