using System;
using static System.Console;
namespace RPG_01
{
    internal class serviceOfTheFight
    {
        internal static void calculateTheResult(ref Enemy enemy, ref managePlayerAttr managePlayerAttributes)
        {
            while (true)
            {
                enemy.hitEnemy(ref managePlayerAttributes);
                WriteLine($"You have {managePlayerAttributes.getHitPoints()} hp");
                if (mainChar.isAlive)
                {
                    managePlayerAttributes.attackEnemy(ref enemy);
                    WriteLine($"Enemy has {enemy.Hp}");
                    if ((enemy.Hp <= 0))
                    {
                        WriteLine("You have won!");
                        break;
                    }
                }
                else
                {
                    WriteLine("Enemy has won!. You died.");
                    break;
                }
            }
            managePlayerAttributes.regenerateHitPoints();
            ReadKey();
        }
    }
}