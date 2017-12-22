using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{

    public abstract class Room : IRoom
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public List<Item> Items { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public bool PassagesBuilt { get; set; }
        public virtual bool VisitedByPlayer { get; set; }
        public List<string> Exits { get; set; } = new List<string>();

        public void UseItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Event(Game game, Player player)
        {
            throw new System.NotImplementedException();
        }

        public Room(int y, int x)
        {
            Name = "Room";
            Y = y;
            X = x;
            PassagesBuilt = false;
            Exits = new List<string>();
        }
    }

    public class TestRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("Nothing happens. This is just a test room after all.");
        }
        public TestRoom(int y, int x) : base(y, x)
        {
            Name = "Test Room";
            Description = $"Test Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
        }
    }

    public class EmptyRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("Nothing happens. This is just an empty room after all.");
        }
        public EmptyRoom(int y, int x) : base(y, x)
        {
            Name = "Empty Room";
            Description = $"Empty Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
        }
    }

    public class StartRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }

        public override void Event(Game game, Player player)
        {
            Console.WriteLine("Nothing happens. This is just a start room after all.");
        }
        public StartRoom(int y, int x) : base(y, x)
        {
            Name = "Start Room";
            Description = $"Start Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = true;
        }
    }

    public class DeathRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }

        public override bool VisitedByPlayer { get; set; }

        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou immediately die without explanation.");
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
            game.GameOver();
        }
        public DeathRoom(int y, int x) : base(y, x)
        {
            Name = "Death Room";
            Description = $"Death Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
        }
    }
}
