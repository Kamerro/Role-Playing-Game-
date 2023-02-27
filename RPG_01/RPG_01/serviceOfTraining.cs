using System;
using System.Collections.Generic;

namespace RPG_01
{
    internal class serviceOfTraining
    {
        static int costOfFreeTraining=20;
        internal static void makeProperTraining(ref managePlayerAttr managePlayerAttributes, int index)
        {
            try
            {
                managePlayerAttributes.changeCvs(listOfAvaiableTrainings[index].cleverness);
                managePlayerAttributes.changeStr(listOfAvaiableTrainings[index].strength);
                managePlayerAttributes.changeInt(listOfAvaiableTrainings[index].inteligence);
                managePlayerAttributes.changeHlth(listOfAvaiableTrainings[index].health);
            }
            catch
            {
                index = 1;
                
            }

        }
        static public List<inprovementOfStat> listOfAvaiableTrainings = new List<inprovementOfStat>();
        internal static void makeListOfAvaiableTrainings(managePlayerAttr managePlayerAttribute)
        {
            listOfAvaiableTrainings.Clear();
            int total_cleverness = managePlayerAttribute.getCleverness();
            CalculateAdditionalValues(total_cleverness,out int additional_int, out int additional_health, out int additional_cleverness, out int additional_strength);
            if (total_cleverness>100)
            {
                listOfAvaiableTrainings.Add(new inprovementOfStat(inteligence:additional_int,Name:"Improvement int"));
            }
            if(total_cleverness > 60)
            {
                listOfAvaiableTrainings.Add(new inprovementOfStat(health: additional_health, Name: "Improvement health"));
            }
            if (total_cleverness > 60)
            {
                listOfAvaiableTrainings.Add(new inprovementOfStat(strength: additional_strength, Name: "Improvement str"));
            }
            listOfAvaiableTrainings.Add(new inprovementOfStat(cleverness: additional_cleverness, Name: "Improvement clv"));
        }

        private static void CalculateAdditionalValues(int total_cleverness, out int additional_int, out int additional_health, out int additional_cleverness, out int additional_strength)
        {
            additional_int = (total_cleverness * 10) / 200;
            additional_cleverness = 10;
            additional_strength = (total_cleverness * 10) / 200 + (additional_int * 10) / 200;
            additional_health = (total_cleverness * 10) / 300 + additional_strength / 5;
        }

        internal static int freeAttributeGetCostOfReputation()
        {
            return costOfFreeTraining;
        }
        internal static void changeCostOfFreeTraining()
        {
            costOfFreeTraining += costOfFreeTraining / 2;
        }
    }
}