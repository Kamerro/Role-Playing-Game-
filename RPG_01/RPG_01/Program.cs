using System;
using System.Collections.Generic;
using static System.Threading.Thread;
using static System.Console;

namespace RPG_01
{
    class Program
    {
        static private bool isFirstAttempt = true;
        public enum EstartPositions
        {
            Buy=1,
            Sell,
            Quest,
            BossFight,
            AmplificationOfStats
        }

        static Shop shop;
        static managePlayerResources managePlayerResources;
        static managePlayerAttr managePlayerAttributes;
        static managePlayerEq managePlayerEquipement;
        static List<Quest> questHolder = new List<Quest>();
        static List<Enemy> enemyHolder = new List<Enemy>();

        static void ini()
        {
            mainChar mainCharacter = new mainChar();
            managePlayerAttributes = new managePlayerAttr(ref mainCharacter);
            managePlayerEquipement = new managePlayerEq(ref mainCharacter);
            managePlayerResources = new managePlayerResources(ref mainCharacter);
            shop = new Shop();
            generateItems();
            generateQuests();
            generateEnemies();
        }
        static void Main(string[] args)
        {
            ini();
            EstartPositions es = new EstartPositions();
            List<string> listOfPosibilitiesInCity = new List<string>();
            listOfPosibilitiesInCity = addPosibilitiesToList(es);
            welcomeWithPlayer(isFirstAttempt);
            for (int round = 0; round < 1000 && mainChar.isAlive;)
            {
                Clear();
                preparationForChallenge();
                WriteLine($"Welcome in the city, round {++round}/1000.");
                WriteLine($"Choose what is your move. Actions from below will take an action.");
                whatPlayerSee(listOfPosibilitiesInCity);
                takeInputAndMakeProperAction(waitForEnterOfThePlayer());
                Clear();
                managePlayerAttributes.showAllAttributes();
            }
            isFirstAttempt = false;
        }

        private static void preparationForChallenge()
        {
            managePlayerAttributes.showAllAttributes();
            WriteLine("Type '2' to go to city, or 1 for wear new eq.");
            if (!(ReadLine() == "2"))
            {
                showEquipementForChange();
            }
            Clear();
            managePlayerAttributes.showAllAttributes();
        }

        private static void showEquipementForChange()
        {
            managePlayerEquipement.showWearedEq();
            WriteLine("Select item you want to wear:");
            managePlayerEquipement.showAllEq();
            string answer = ReadLine();
            int index = int.TryParse(answer, out int result) && result>0 && result<=managePlayerEquipement.getCountOfItems()?result-1:-1;
            managePlayerEquipement.manageWearTheChosenEq(index);
            WriteLine("Type '2' to go to city, or 1 for wear new eq.");
            if (!(ReadLine() == "2"))
            {
                showEquipementForChange();
            }
        }

        private static void takeInputAndMakeProperAction(int chosenIndex)
        {
            int index;
            string answer;
            switch (chosenIndex) {
                case 1:
                    //At time player can make any transactions he wants to.
                   
                        timeAtShopBuy(ref managePlayerEquipement);
                        WriteLine("write '1' if you want to buy something else");
                    break;

                case 2:
                    timeAtShopSell(ref managePlayerEquipement);
                    
                    break;

                case 3:
                    WriteLine("Pick your poison");
                    ShowAllQuests();
                    answer = ReadLine();
                    index = int.TryParse(answer, out int questIndex) && questIndex > 0 && questIndex < questHolder.Count ? questIndex : 1;
                    MakeChosenQuest(index);

                    break;

                case 4:
                    WriteLine("You chose the fight with enemy. What a bravery!");
                    WriteLine("Here is the list of the enemies that are avaiable.");
                    showListOfEnemies();
                    answer = ReadLine();
                    index = int.TryParse(answer, out int enemyIndex) && enemyIndex > 0 && enemyIndex < questHolder.Count ? enemyIndex : 1;
                    Enemy enemy = enemyHolder[index-1];
                    fightWithBoss(ref enemy);
                    enemyHolder.RemoveAt(index - 1);
                    managePlayerResources.emblems++;
                    changeAttributesOfTheEnemies();
                    break;
                case 5:
                    bool isFreeAction = false;
                    //in this case, firstly the algorithm need to check amount of cleverness and add avaiable trainings.
                    WriteLine($"The cost of free training is {serviceOfTraining.freeAttributeGetCostOfReputation()} reputation");
                    if (managePlayerResources.getReputation() >= serviceOfTraining.freeAttributeGetCostOfReputation())
                    {
                        WriteLine("Write 'yes' if you wanna free training.");
                        isFreeAction = Console.ReadLine() == "yes" ? true : false;
                    }
                    serviceOfTraining.makeListOfAvaiableTrainings(managePlayerAttributes);
                    WriteLine("Time for exercising!");
                    showListOfAvaiableTrainings();
                    answer = ReadLine();
                    index = int.TryParse(answer, out int trainingIndex) && trainingIndex > 0 && trainingIndex < questHolder.Count ? trainingIndex : 1;
                    serviceOfTraining.makeProperTraining(ref managePlayerAttributes, index - 1);
                    if (isFreeAction)
                    {
                        serviceOfTraining.changeCostOfFreeTraining();
                        takeInputAndMakeProperAction(5);
                    }
                    break;
            }
        }

        private static void changeAttributesOfTheEnemies()
        {
           foreach(Enemy en in enemyHolder)
            {
                en.changeStats(managePlayerResources.emblems);
            }
        }

        private static void showListOfAvaiableTrainings()
        {
            int i = 0;
           foreach(inprovementOfStat stat in serviceOfTraining.listOfAvaiableTrainings)
            {
                WriteLine($"{++i}: {stat.Name}");
            }
        }

        private static void fightWithBoss(ref Enemy enemy)
        {
            //Serwis w którym dzieją się obliczenia związane z walką.
            serviceOfTheFight.calculateTheResult(ref enemy, ref managePlayerAttributes);
          
        }

        private static void showListOfEnemies()
        {
            int i = 0;
            foreach (Enemy enemy in enemyHolder)
            {
                WriteLine($"{++i}:{enemy.Name} Attack:{enemy.getAttack()} HP:{enemy.getHp()}");
            }            
        }

        private static void MakeChosenQuest(int index)
        {
            managePlayerResources.addReputation(questHolder[index-1]
           .calculateGainedReputation(managePlayerResources.getBonusReputation()));

            managePlayerResources.addGold(questHolder[index-1]
           .calculateGainedGold(managePlayerResources.getBonusGold()));
            WriteLine($"You have chosen {questHolder[index-1].getName()}");
        }

        private static void ShowAllQuests()
        {
            int i = 0;
            foreach(Quest quest in questHolder)
            {
                if(managePlayerResources.emblems >= quest.neededEmblems)
                WriteLine($"{++i} {quest.getName()} gold:{quest.calculateGainedGold(managePlayerAttributes.getBonusGold())} rep:{quest.calculateGainedReputation(managePlayerAttributes.getBonusRep())}");
            }
        }

        private static void timeAtShopSell(ref managePlayerEq mPE)
        {
            while (true)
            {
                Clear();
                WriteLine("Chose from eqipment what you want to sell:");
                mPE.showAllEq();
                WriteLine("Write number by item to sell or 'back' to skip ");
                string answer = ReadLine();
                //if someone write different answer than (1-itemsCount) then break the switch.
                int? i = int.TryParse(answer, out int result) && (result > 0) && (result <= mPE.getCountOfItems()) ? result : null;
                if (!(i is null))
                {
                    //quite simple, doesnt it?
                    int chosenItemIndex = i.Value;
                    managePlayerResources.addGold(mPE.getCostItem(chosenItemIndex));
                    shop.addItem(mPE.getItem(chosenItemIndex));
                    mPE.removeItem(chosenItemIndex);
                    WriteLine(managePlayerResources.getGold());
                }
                WriteLine("Do you want to repeat transaction? write 'back' to exit");
                if (!int.TryParse(ReadLine(), out int repeat))
                    break;


            }
        }

        private static void timeAtShopBuy(ref managePlayerEq mPE)
        {
            while (true)
            {
                //Showing offer can be made in different method.
                WriteLine("Write max amount of gold that you want to spend.");
                string maxAmountOfMoney = ReadLine();
                //func that will check what would be really showed,
                //amount will be max if the input will be out of range or trash
                int amount = int.TryParse(maxAmountOfMoney, out int realCash) && realCash > 0? realCash : managePlayerResources.getGold();
                WriteLine("There is an offer.");
                shop.showItems(amount);
                WriteLine("Write number by item to buy or 'back' to skip ");
                string answer = ReadLine();
                //if someone write different answer than (1-itemsCount) then break the switch.
                int? i = int.TryParse(answer, out int result) && (result > 0) && (result <= shop.itemsCount()) ? result : null;
                if (i is null) break;
                //quite simple, doesnt it?
                int chosenItemIndex = i.Value;
                if (managePlayerResources.getGold() >= shop.getCostOfItemByIndex(chosenItemIndex))
                {
                    managePlayerResources.addGold(-shop.getCostOfItemByIndex(chosenItemIndex));
                    mPE.addItem(shop.getItemByIndex(chosenItemIndex));
                    shop.removeItemByIndex(chosenItemIndex);
                    WriteLine("Do you want to repeat transaction? write 'back' to exit");
                    if (!int.TryParse(ReadLine(), out int repeat))
                        break;

                }
                WriteLine("Not enough gold, try buy something else or go away!");

            }
        }

        private static int waitForEnterOfThePlayer()
        {
            //setting 3 as default value for response
            return (int.TryParse(Console.ReadLine(), out int response) && response<6 && response > 0) ? response : 3;
        }
        //Functions 

        private static List<string> addPosibilitiesToList<T>(T e)
        {
            //Function that adds posibilities of actions, that may be done by player.
            List<string> listOfPosibilitiesInCity = new List<string>();
            Array val = Enum.GetValues(typeof(T));
            
            foreach (var obj in val)
            {
                listOfPosibilitiesInCity.Add(obj.ToString());
            }
            return listOfPosibilitiesInCity;
        }

        private static void whatPlayerSee<T>(List<T> listOfPossibilitiesInCity)
        {
            int i = 0;
            foreach(var option in listOfPossibilitiesInCity)
            {
                Console.WriteLine($"{++i}:{option}");
            }
        }

        private static void welcomeWithPlayer(bool isFirstAttempt)
        {
            if (isFirstAttempt)
            {

                firstWelcome();
            }
            else
            {
                welcomeAgain();
            }
        }


        //Strings notthing interesting.


        private static void welcomeAgain()
        {
            Console.WriteLine("Hello there again, you know the rules.");
        }

        private static void firstWelcome()
        {
            /*
            Console.WriteLine("Hey, in this world YOU need to defeat 10 enemies in terms to win." +
                "Some of them are sneaky, so better be care.");
            Sleep(2000);
            Console.WriteLine("You have nothig with yourself. Your body is weak and tired.");
            Sleep(3000);
            Console.WriteLine("Only thing You know for now is that, You have came out from sever.");
            Sleep(3000);
            Console.WriteLine("After some time you're up. The city around is something beutifull.");
            Sleep(3000);
            Console.WriteLine("In your pocket is 1 copper. Damn you...");
            Console.WriteLine("Write anything to start");
            Console.ReadLine();
            Console.Clear();
            */
        }


          //                                           //                               // Generate items, quests enemies.
        private static void generateQuests()
        {
            Quest Quest_01 = new Quest(name: "Quest_01", rep: 10, gold: 2,emblems:0);
            Quest Quest_02 = new Quest(name: "Quest_02", rep: 5, gold: 5, emblems: 0);
            Quest Quest_03 = new Quest(name: "Quest_03", rep: 2, gold: 10, emblems: 0);
           //---------------------Down quest after 1 boss -> max sum of rep + gold = 13
            Quest Quest_04 = new Quest(name: "Quest_04", rep: 11, gold: 2, emblems: 1);
            Quest Quest_05 = new Quest(name: "Quest_05", rep: 6, gold: 7, emblems: 1);
            Quest Quest_06 = new Quest(name: "Quest_06", rep: 2, gold: 11, emblems: 1);
            //---------------------Down quest after 2 boss -> max sum is 20
            Quest Quest_07 = new Quest(name: "Quest_07", rep: 15, gold: 5, emblems: 2);
            Quest Quest_08 = new Quest(name: "Quest_08", rep: 10, gold: 10, emblems: 2);
            Quest Quest_09 = new Quest(name: "Quest_09", rep: 5, gold: 15, emblems: 2);
            //---------------------Down quest after 3 boss -> max sum is 25
            Quest Quest_10 = new Quest(name: "Quest_10", rep: 20, gold: 5, emblems: 3);
            Quest Quest_11 = new Quest(name: "Quest_11", rep: 10, gold: 15, emblems: 3);
            Quest Quest_12 = new Quest(name: "Quest_12", rep: 5, gold: 20, emblems: 3);
            //---------------------Down quest after 5 boss -> max sum is 27
            Quest Quest_13 = new Quest(name: "Quest_13", rep: 25, gold: 2, emblems: 5);
            Quest Quest_14 = new Quest(name: "Quest_14", rep: 17, gold: 10, emblems: 5);
            Quest Quest_15 = new Quest(name: "Quest_15", rep: 2, gold: 25, emblems: 5);
            //---------------------Down quest after 7 boss -> max sum is 33
            Quest Quest_16 = new Quest(name: "Quest_16", rep: 30, gold: 3, emblems: 7);
            Quest Quest_17 = new Quest(name: "Quest_17", rep: 15, gold: 15, emblems: 7);
            Quest Quest_18 = new Quest(name: "Quest_18", rep: 3, gold: 30, emblems: 7);
            //---------------------Down quest after 9 boss -> max sum is 40
            Quest Quest_19 = new Quest(name: "Quest_19", rep: 30, gold: 10, emblems: 9);
            Quest Quest_20 = new Quest(name: "Quest_20", rep: 2, gold:20, emblems: 9);
            Quest Quest_21 = new Quest(name: "Quest_21", rep: 10, gold: 30, emblems: 9);
          
            questHolder.Add(Quest_01);
            questHolder.Add(Quest_02);
            questHolder.Add(Quest_03);
            questHolder.Add(Quest_04);
            questHolder.Add(Quest_05);
            questHolder.Add(Quest_06);
            questHolder.Add(Quest_07);
            questHolder.Add(Quest_08);
            questHolder.Add(Quest_09);
            questHolder.Add(Quest_10);
            questHolder.Add(Quest_11);
            questHolder.Add(Quest_12);
            questHolder.Add(Quest_13);
            questHolder.Add(Quest_14);
            questHolder.Add(Quest_15);
            questHolder.Add(Quest_16);
            questHolder.Add(Quest_17);
            questHolder.Add(Quest_18);
            questHolder.Add(Quest_19);
            questHolder.Add(Quest_20);
            questHolder.Add(Quest_21);
        }

        private static void generateItems()
        {
            //Swords, halabards and third ones.
            shop.addItem(new WhiteWeapon(Name: "dagger", attack: 10, cleverness: 20, intelligence: 20, cost: 2));
            shop.addItem(new WhiteWeapon(Name: "rusted sword", attack: 20, cleverness: 30, intelligence: 30, cost: 10));
            shop.addItem(new WhiteWeapon(Name: "medium of strength", attack: 100, cleverness: 90, cost: 100));
            shop.addItem(new WhiteWeapon(Name: "Stricky sword", attack: 200, intelligence: 300, cleverness: 300, cost: 400));
            shop.addItem(new WhiteWeapon(Name: "Legendary sword", attack: 400, intelligence: 1000, cleverness: 1000, cost: 3000));
            //axes with no bonuses but huge strike.
            shop.addItem(new WhiteWeapon(Name: "small axe", attack: 50, cost: 10));
            shop.addItem(new WhiteWeapon(Name: "Big axe", attack: 100, cost: 30));
            shop.addItem(new WhiteWeapon(Name: "Destroyer", attack: 300, cost: 600));
            shop.addItem(new WhiteWeapon(Name: "Magnificant axe", attack: 900, cost: 4000));
            shop.addItem(new WhiteWeapon(Name: "head-off fker", attack: 2000, cost: 9000));
            //third ones. even more damage, but huge price
            shop.addItem(new WhiteWeapon(Name: "diggy-piggy", attack: 120, cost: 40));
            shop.addItem(new WhiteWeapon(Name: "spike", attack: 500, cost: 2000));
            shop.addItem(new WhiteWeapon(Name: "geralt-killer", attack: 2500, cost: 10000));
            shop.addItem(new WhiteWeapon(Name: "splinter", attack: 9000, cost: 30000));
            shop.addItem(new WhiteWeapon(Name: "fk-offer", attack: 120000, cost: 1200000));

            //armor with bonuses:
            shop.addItem(new Armor(Name: "coat", health: 20, cost: 8));
            shop.addItem(new Armor(Name: "medium armor", health: 60, cleverness: 60, cost: 80));
            shop.addItem(new Armor(Name: "advanced armor", health: 200, intelligence: 50, cleverness: 100, cost: 200));
            shop.addItem(new Armor(Name: "Legendary armor", health: 500, intelligence: 200, cleverness: 100, strength: 200, cost: 500));
            shop.addItem(new Armor(Name: "Heroic", health: 2000, intelligence: 1000, cost: 2000));

            //Armor with no bonuses.
            shop.addItem(new Armor(Name: "light armor", health: 50, cost: 50));
            shop.addItem(new Armor(Name: "well worn armor", health: 300, cost: 200));
            shop.addItem(new Armor(Name: "heavy armor", health: 1200, cost: 1000));
            shop.addItem(new Armor(Name: "Good ol'dick twist", health: 6000, cost: 6000));
            shop.addItem(new Armor(Name: "manga-lover-tshirt.", health: 50000, cost: 45000));


            shop.addItem(new Ring(Name: "copper ring", cleverness: 10, intelligence: 10, cost: 1));
            shop.addItem(new Ring(Name: "silver ring", cleverness: 50, intelligence: 20, cost: 10));
            shop.addItem(new Ring(Name: "gold ring", cleverness: 100, intelligence: 70, cost: 100));
            shop.addItem(new Ring(Name: "platinum ring", cleverness: 300, intelligence: 100, cost: 500));
            shop.addItem(new Ring(Name: "composit ring", cleverness: 700, intelligence: 120, cost: 2500));
            shop.addItem(new Ring(Name: "light ring", cleverness: 20, intelligence: 30, cost: 10));
            shop.addItem(new Ring(Name: "small ring", cleverness: 30, intelligence: 100, cost: 100));
            shop.addItem(new Ring(Name: "demonic ring", cleverness: 2000, intelligence: 1000, cost: 9000));

            shop.addItem(new Pets(name: "german shipherd", reputationBonus: 20, goldBonus: 10, cost: 30));
            shop.addItem(new Pets(name: "small troll", reputationBonus: 100, goldBonus: 300, cost: 150));
            shop.addItem(new Pets(name: "small mule", reputationBonus: 200, goldBonus: 200, cost: 300));
            shop.addItem(new Pets(name: "wolf", reputationBonus: 800, goldBonus: 600, cost: 1000));
            shop.addItem(new Pets(name: "bear", reputationBonus: 1000, goldBonus: 2000, cost: 2000));
            shop.addItem(new Pets(name: "small dragon", reputationBonus: 2000, goldBonus: 2000, cost: 5000));
            shop.addItem(new Pets(name: "huge dragon", reputationBonus: 10000, goldBonus: 10000, cost: 6000));
            shop.addItem(new Pets(name: "weather controller", reputationBonus: 20000, goldBonus: 20000, cost: 12000));
        }
        private static void generateEnemies()
        {
            enemyHolder.Add(new Enemy(name: "Time Shifter", hp: 100, attack: 50, scale: 2.1));
            enemyHolder.Add(new Enemy(name: "Illusionist", hp: 110, attack: 45, scale: 3));
            enemyHolder.Add(new Enemy(name: "Mind Tricker", hp: 120, attack: 40, scale: 2.2));
            enemyHolder.Add(new Enemy(name: "Lost Hearth", hp: 10, attack: 10, scale: 1));
            enemyHolder.Add(new Enemy(name: "Unseen Enemy", hp: 10, attack: 100, scale: 1));
            enemyHolder.Add(new Enemy(name: "Hunter", hp: 120, attack: 50, scale: 2));
            enemyHolder.Add(new Enemy(name: "Nightmare Of The Nations", hp: 100, attack: 100, scale: 1.7));
            enemyHolder.Add(new Enemy(name: "Specialist", hp: 100, attack: 40, scale: 1));
            enemyHolder.Add(new Enemy(name: "Extra Killer", hp: 100, attack: 40, scale: 1));
            enemyHolder.Add(new Enemy(name: "hk", hp: 100, attack: 40, scale: 4));
        }



    }
}
