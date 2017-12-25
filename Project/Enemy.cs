using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Enemy
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual int Health { get; set; }
        public virtual int MaxHealth { get; set; }
        public virtual int DefenseRating { get; set; }
        public virtual int MaxDefenseRating { get; set; }
        public virtual Dictionary<List<string>, int> Attacks { get; set; }
        public virtual List<Item> DropItems { get; set; }

        public Item DropItem()
        {
            Random r = new Random();
            int randIndex = r.Next(0, DropItems.Count);
            return DropItems[randIndex];
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public Enemy()
        {
            Name = "Enemy";
            Description = "";
            Type = "";
            Health = 0;
            MaxHealth = 0;
            DefenseRating = 0;
            MaxDefenseRating = 0;
            Attacks = new Dictionary<List<string>, int>();
            DropItems = new List<Item>();
        }
    }

    public abstract class TestEnemy : Enemy
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override int Health { get; set; }
        public override int MaxHealth { get; set; }
        public override int DefenseRating { get; set; }
        public override int MaxDefenseRating { get; set; }
        public override Dictionary<List<string>, int> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public TestEnemy()
        {
            Name = "Test Enemy";
            Description = "It is a standard Test Enemy.";
            Type = "purification";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 15;
            DefenseRating = MaxDefenseRating;
            Attacks = new Dictionary<List<string>, int>()
            {
                {new List<string>(){
                    "weak",
                    $"{this.Name} uses a weak attack!"
                }, 5 },
                {new List<string>(){
                    "moderate",
                    $"{this.Name} uses a moderate attack!"
                }, 10 },
                {new List<string>(){
                    "strong",
                    $"{this.Name} uses a strong attack!"
                }, 15 }
            };
            DropItems = new List<Item>(){
                new BoneAsh(), new MetalCore(), new CrimsonOil()
            };
        }
    }
}