using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWSARuneManager
{
    [Serializable]
    public class Monster
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int CriticalRate { get; set; }
        public int CriticalDamage { get; set; }
        public int Resistance { get; set; }
        public int Accuracy { get; set; }

        private void InitDefaultValues()
        {
            Name = "";
            HitPoints = 0;
            Attack = 0;
            Defense = 0;
            Speed = 0;
            CriticalRate = 15;
            CriticalDamage = 50;
            Resistance = 15;
            Accuracy = 0;
        }

        public Monster()
        {
            InitDefaultValues();
        }
        
    }

    public class Rune
    {
        public enum RuneType
        {
            Energy,
            Blade,
            Swift,
            Rage,
            Vampire,
            Guard,
            Will,
            Revenge,
            Despair,
            Violent,
            Focus,
            Fatal,
            Endure,
            Nemesis,
            Shield
        }

        private void InitDefaultValues(int position=0, int level=0)
        {

            Position = 1;
            Level = 0;

            HitPointsPercent = 0;
            HitPoints = 0;
            AttackPercent = 0;
            Attack = 0;
            DefensePercent = 0;
            Defense = 0;
            Speed = 0;
            CriticalRate = 0;
            CriticalDamage = 0;
            Resistance = 0;
            Accuracy = 0;

            HasFakeStat = false;
        }

        public RuneType Type { get; set; }
        public int Position { get; set; }
        public int Level { get; set; }
        
        public int HitPointsPercent { get; set; }
        public int HitPoints { get; set; }
        public int AttackPercent { get; set; }
        public int Attack { get; set; }
        public int DefensePercent { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int CriticalRate { get; set; }
        public int CriticalDamage { get; set; }
        public int Resistance { get; set; }
        public int Accuracy { get; set; }

        public Boolean HasFakeStat;

        private int CountSubStats()
        {
            int count = 0;
            if (HitPointsPercent > 0) count++;
            if (HitPoints > 0) count++;
            if (AttackPercent > 0) count++;
            if (Attack > 0) count++;
            if (DefensePercent > 0) count++;
            if (Defense > 0) count++;
            if (Speed > 0) count++;
            if (CriticalRate > 0) count++;
            if (CriticalDamage > 0) count++;
            if (Resistance > 0) count++;
            if (Accuracy > 0) count++;

            // Subtract 1 for the main stat
            count--;

            // Subtract one more if there is a fake stat
            if (HasFakeStat) count--;

            return count;
        }

        public Boolean IsMaxed()
        {
            Boolean returnValue = false;

            if (Position == 2 || Position == 4 || Position == 6)
            {
                // Main Stat Runes (2,4,6)
                if (Level == 15) returnValue = true;
            }
            else
            {
                // Substat Runes (1,3,5)
                int numSubStats = CountSubStats();
                if (Level >= (numSubStats * 3)) returnValue = true;
            }

            return returnValue;
        }
    }
}

