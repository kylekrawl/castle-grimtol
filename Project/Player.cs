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
        public List<Item> Inventory { get; set; }
        public List<Note> Notes { get; set; }

        public Player(Map map)
        {
            Name = "Alchemist";
            Score = 0;
            Inventory = new List<Item>() { 
                //new FlintlockPistol(),
                new DivinePistol(),
                new LethalVenom(),
                new ReactiveSolid()
            };
            Notes = new List<Note>() {
                new Note("Dr. Rithbaun's Letter", $@"
<The handwriting is messy, as if the note was written in haste>

{Name}:

I am in danger. Come to Grimtol.

- R
")
            };
            X = map.PlayerStartX;
            Y = map.PlayerStartY;
            PreviousY = Y;
            PreviousX = X;
            MaxHealth = 100;
            Health = MaxHealth;
            MaxDefenseRating = 15;
            DefenseRating = MaxDefenseRating;
        }
    }
}