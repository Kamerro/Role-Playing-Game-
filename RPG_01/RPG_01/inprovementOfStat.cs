namespace RPG_01
{
    internal class inprovementOfStat
    {
        public int inteligence;
        public int strength;
        public int health;
        public int cleverness;
        public string Name;
        public inprovementOfStat(string Name,int inteligence = 0, int cleverness = 0, int strength = 0, int health = 0)
        {
            this.inteligence = inteligence;
            this.strength = strength;
            this.health = health;
            this.cleverness = cleverness;
            this.Name = Name;
        }
    }
}