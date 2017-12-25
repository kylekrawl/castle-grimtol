using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {   
        public string Name {get; set;}
        public int X { get; set; }
        public int Y { get; set; }
        public int PreviousY { get; set; }
        public int PreviousX { get; set; }
        public int Score { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public double DefenseRating { get; set; }
        public double MaxDefenseRating { get; set; }
        public Item Weapon { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(Map map)
        {
            Name = "Alchemist";
            Score = 0;
            Inventory = new List<Item>() { 
                new FlintlockPistol()
            };
            X = map.PlayerStartX;
            Y = map.PlayerStartY;
            PreviousY = Y;
            PreviousX = X;
            MaxHealth = 100;
            Health = MaxHealth;
            MaxDefenseRating = 20;
            DefenseRating = MaxDefenseRating;
        }
    }
}