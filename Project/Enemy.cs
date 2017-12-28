using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Enemy
    {
        public virtual string Name { get; set; }
        public virtual string ApproachDescription { get; set; }
        public virtual string CombatDescription { get; set; }
        public virtual string DefeatedDescription { get; set; }
        public virtual string VictoriousDescription { get; set; }
        public virtual string Type { get; set; }
        public virtual double Health { get; set; }
        public virtual double MaxHealth { get; set; }
        public virtual double DefenseRating { get; set; }
        public virtual double MaxDefenseRating { get; set; }
        public virtual List<Attack> Attacks { get; set; }
        public virtual List<Item> DropItems { get; set; }

        public Item DropItem()
        {
            Random r = new Random();
            int randIndex = r.Next(0, DropItems.Count);
            return DropItems[randIndex];
        }

        public Enemy()
        {
            Name = "Enemy";
            ApproachDescription = "";
            CombatDescription = "";
            DefeatedDescription = "";
            VictoriousDescription = "";
            Type = "";
            Health = 0;
            MaxHealth = 0;
            DefenseRating = 0;
            MaxDefenseRating = 0;
            Attacks = new List<Attack>();
            DropItems = new List<Item>();
        }
    }

    public class TestEnemy : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public TestEnemy()
        {
            Name = "Test Enemy";
            ApproachDescription = "A terrifying creature lumbers toward you.";
            CombatDescription = "The creature flails around menacingly.";
            DefeatedDescription = "The creature lets out a gurgling cry as it fall to the ground.";
            VictoriousDescription = "The creature lands a killing blow.";
            Type = "purification";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Weak Attack", $"{this.Name} uses a weak attack.", 15, 50),
               new Attack("Strong Attack", $"{this.Name} uses a strong attack.", 50, 100)
            };
            DropItems = new List<Item>(){
                new BoneAsh(), new MetalCore(), new CrimsonOil()
            };
        }
    }
}