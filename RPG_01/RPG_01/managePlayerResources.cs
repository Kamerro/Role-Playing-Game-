using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_01
{
    class managePlayerResources:mainChar
    {
        //take mainChar as parameter
        mainChar p;
        internal int emblems;

        //Create actual reference to player
        public managePlayerResources(ref mainChar player)
        {
            p = player;
        }

        public void addGold(int value)
        {
            p.gold += value;
        }
        public void addReputation(int value)
        {
            p.reputation += value;
        }
        public int getBonusReputation()
        {
            return p.bonusReputation;
        }

        public int getBonusGold()
        {
            return p.bonusGold;
        }

        public int getGold()
        {
            return p.gold;
        }

        public int getReputation()
        {
            return p.reputation;
        }

    }
}
