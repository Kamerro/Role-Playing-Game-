using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace RPG_01
{
    class managePlayerEq:mainChar
    {

        mainChar p;
        public managePlayerEq(ref mainChar player)
        {
            p = player;
        }
        public int getCountOfItems()
        {
            WriteLine(p.listOfItems.Count);
            return p.listOfItems.Count;
        }
        public void showAllEq()
        {
            int i = 0;
           foreach(Items item in p.listOfItems)
            {
                WriteLine($"{++i} {item.GetName()} cost:{item.cost}"); 
            }
        }
        public void doThing()
        {
           // makeSound(ref p);
        }
        public int getCostItem(int index)
        {

            return p.listOfItems[index - 1].cost;
        }
        public Items getItem(int index)
        {

            return p.listOfItems[index - 1];
        }
        public void addItem(Items item)
        {
            item.setCost();
            p.listOfItems.Add(item);
        }
        public void wearItem(Items item)
        {

            switch (item)
            {
                case Armor:
                    p.wearedArmor = (Armor)item;
                    calculateHitpoints(ref p);
                    calculateCleverness(ref p);
                    calculateInteligence(ref p);
                    calculateBonusGold(ref p);
                    calculateBonusRep(ref p);
                    break;

                case WhiteWeapon:
                    p.wearedWeapon = (WhiteWeapon)item;
                    calculateAttack(ref p);
                    calculateCleverness(ref p);
                    calculateInteligence(ref p);
                    calculateBonusGold(ref p);
                    calculateBonusRep(ref p);
                    break;

                case Ring:
                    p.wearedRing = (Ring)item;
                    calculateAttack(ref p);
                    calculateCleverness(ref p);
                    calculateInteligence(ref p);
                    calculateHitpoints(ref p);
                    calculateBonusGold(ref p);
                    calculateBonusRep(ref p);
                break;

                case Pets:
                    p.wearedPet = (Pets)item;
                    calculateAttack(ref p);
                    calculateCleverness(ref p);
                    calculateInteligence(ref p);
                    calculateHitpoints(ref p);
                    calculateBonusGold(ref p);
                    calculateBonusRep(ref p);
                 break;

            }


        }

        public void removeItem(int index)
        {
            p.listOfItems.RemoveAt(index - 1);
        }

        internal void manageWearTheChosenEq(int index)
        {
            if (!(index == -1))
            {
                wearItem(p.listOfItems[index]);
            }
            else
                WriteLine("you wrote index out of range.");
        }
        private string getWearedArmorName()
        {
            return p.wearedArmor.GetName();
        }
        private string getWearedWhiteWeaponName()
        {
            return p.wearedWeapon.GetName();
        }
        private string getWearedPetName()
        {
            return p.wearedPet.GetName();
        }
        private string getWearedRingName()
        {
            return p.wearedRing.GetName();
        }
        internal void showWearedEq()
        {
            WriteLine($"Armor:{getWearedArmorName()}");
            WriteLine($"White weapon:{getWearedWhiteWeaponName()}");
            WriteLine($"Pet:{getWearedPetName()}");
            WriteLine($"Ring:{getWearedRingName()}");
        }
    }
}
