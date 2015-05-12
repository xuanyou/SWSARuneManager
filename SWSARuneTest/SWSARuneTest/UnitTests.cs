using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWSARuneManager;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SWSARuneTest
{
    [TestClass]
    public class MonsterTest
    {
        private Monster GenerateTestMonster(string name)
        {
            Monster monster = new Monster();

            if (name.Equals("Shannon", StringComparison.CurrentCultureIgnoreCase))
            {
                monster.Name = "Shannon";
                monster.HitPoints = 7245;
                monster.Attack = 602;
                monster.Defense = 560;
                monster.Speed = 111;
            }
            else if (name.Equals("Bernard", StringComparison.CurrentCultureIgnoreCase))
            {
                monster.Name = "Bernard";
                monster.HitPoints = 10377;
                monster.Attack = 418;
                monster.Defense = 703;
                monster.Speed = 111;
            }

            return monster;
        }

        private Boolean MonsterPropertiesEqual(Monster a, Monster b)
        {
            if (ReferenceEquals(a, b)) return true;
            
            Boolean isEqual = true;
            isEqual &= a.Name.Equals(b.Name, StringComparison.CurrentCultureIgnoreCase);
            isEqual &= a.HitPoints.Equals(b.HitPoints);
            isEqual &= a.Attack.Equals(b.Attack);
            isEqual &= a.Defense.Equals(b.Defense);
            isEqual &= a.Speed.Equals(b.Speed);
            isEqual &= a.CriticalRate.Equals(b.CriticalRate);
            isEqual &= a.CriticalDamage.Equals(b.CriticalDamage);
            isEqual &= a.Resistance.Equals(b.Resistance);
            isEqual &= a.Accuracy.Equals(b.Accuracy);

            return isEqual;
        }

        [TestMethod]
        public void TestSerialization()
        {
            Monster monster1 = GenerateTestMonster("Shannon");
            
            IFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, monster1);

            memoryStream.Seek(0, SeekOrigin.Begin);

            Monster monster2 = (Monster) formatter.Deserialize(memoryStream);
            Assert.AreEqual(monster1.Name, monster2.Name);
            Assert.AreEqual(monster1.HitPoints, monster2.HitPoints);
        }

        [TestMethod]
        public void TestListSerialization()
        {
            Monster monster1 = GenerateTestMonster("Shannon");
            Monster monster2 = GenerateTestMonster("Bernard");

            List<Monster> list = new List<Monster>();
            list.Add(monster1);
            list.Add(monster2);

            IFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, list);

            memoryStream.Seek(0, SeekOrigin.Begin);

            List<Monster> readList = (List<Monster>)formatter.Deserialize(memoryStream);

            int i = 0;
            foreach (Monster m in readList)
            {
                Assert.IsTrue(MonsterPropertiesEqual(m, list[i]));
                i++;
            }
        }

        [TestMethod]
        public void TestEquals()
        {
            Monster monster1 = GenerateTestMonster("Shannon");
            Monster monster2 = GenerateTestMonster("Shannon");
            Assert.IsTrue(MonsterPropertiesEqual(monster1,monster2));
        }
    }

    [TestClass]
    public class RuneTest
    {
        [TestMethod]
        public void TestRuneMax()
        {
            Rune rune = new Rune();
            rune.Position = 1;
            rune.Attack = 1;
            rune.CriticalDamage = 5;
            rune.Level = 3;
            Assert.IsTrue(rune.IsMaxed());

            // If the rune has one more stat, then it's not maxed.
            rune.HitPoints = 5;
            Assert.IsFalse(rune.IsMaxed());

            // If that one more stat is fake, then it's maxed.
            rune.HasFakeStat = true;
            Assert.IsTrue(rune.IsMaxed());
            
            // Again, if the rune has one more stat, it's not maxed.
            rune.Speed = 5;
            Assert.IsFalse(rune.IsMaxed());

            // If the rune is now level 6, then it's maxed.
            rune.Level = 6;
            Assert.IsTrue(rune.IsMaxed());
        }
    }

}
