using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{

    public abstract class Room : IRoom
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<Item> RespawnItems { get; set; }
        public virtual List<string> Attributes { get; set; }
        public virtual Enemy Enemy { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public bool PassagesBuilt { get; set; }
        public virtual bool CraftingArea { get; set; }
        public virtual bool VisitedByPlayer { get; set; }
        public List<string> Exits { get; set; } = new List<string>();
        public virtual string EventStage { get; set; }
        public virtual Trap Trap { get; set; }
        public virtual void UseItem(Item item)
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
            Attributes = new List<string>() { "basic" };
            PassagesBuilt = false;
            Exits = new List<string>();
            Enemy = null;
            Trap = null;
            EventStage = null;
        }
    }

    public class TestRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nNothing happens. This is just a test room after all.");
        }
        public TestRoom(int y, int x) : base(y, x)
        {
            Name = "Test Room";
            Description = $"Test Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
            Items = new List<Item>(){
                new LuminousDust(), new ReactiveSolid(), new CrimsonOil(), new YellowIchor(), new ReactiveSolid(), new BoneAsh(), new AcridPowder(), new PulseEmitter()
            };
        }
    }

    public class EmptyRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nNothing happens. This is just an empty room after all.");
        }
        public EmptyRoom(int y, int x) : base(y, x)
        {
            Name = "Empty Room";
            Description = $"Empty Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
            Items = new List<Item>();
        }
    }

    public class MainFoyer : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override List<Item> Items { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nNothing happens. This is just a start room after all.");
        }
        public MainFoyer(int y, int x) : base(y, x)
        {
            Name = "Main Foyer";
            Description = $@"
A large room crafted from dark, polished stone. A collection of tapestries and paintings adorn the walls, 
and a large, ornately carved table sits in the room's center. A makeshift alchemical workstation takes 
up most of the table's space...probably Dr. Rithbaun's handiwork.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            Items = new List<Item>();
        }
    }

    public class DeathRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override List<Item> Items { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
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
            Items = new List<Item>();
        }
    }

    public class EnemyRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override List<Item> Items { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (Enemy.Health > 0)
            {
                game.Combat(Enemy);
            }
        }
        public EnemyRoom(int y, int x) : base(y, x)
        {
            Name = "Enemy Room";
            Description = $"Enemy Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
            Items = new List<Item>();
            Enemy = null;
        }
    }

    public class TrapRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override List<Item> Items { get; set; }
        public override Trap Trap { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine("Nothing happens.");
        }
        public override void Event(Game game, Player player)
        {
            game.TrapEvent();
        }
        public TrapRoom(int y, int x) : base(y, x)
        {
            Name = "Trap Room";
            Description = $"Trap Room at (x:{x} , y:{y})";
            Y = y;
            X = x;
            VisitedByPlayer = false;
            Items = new List<Item>();
            Trap = null;
        }
    }

    public class AssemblyLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public AssemblyLab(int y, int x) : base(y, x)
        {
            Name = "Assembly Lab";
            Description = $@"
You're in a room lit by a dim orange glow. Dark metal plates are affixed to the walls, and the floorspace is almost entirely taken up by metal operating
tables aligned in neat, even rows. Several of them are occupied by what look to be human skeletons with strange bit of machinery attached to them. 
A tidy alchemical workstation sits in one coerner of the room.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new BoneAsh(), new CrimsonOil() };
            Items = new List<Item>() { new BoneAsh(), new CrimsonOil() };
        }
    }

    public class StorageArea : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public StorageArea(int y, int x) : base(y, x)
        {
            Name = "Storage Area";
            Description = $@"
The room is lit only by a faintly glowing lamp on a counter at its center. The walls are completely covered with square metal lockers,
the majority of which are filled with various mechanical components. A few of them contain bones, neatly sorted by type.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            RespawnItems = new List<Item>() { new BoneAsh(), new MetalCore() };
            Items = new List<Item>() { new BoneAsh(), new MetalCore() };
        }
    }

    public class OvergrownLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public OvergrownLab(int y, int x) : base(y, x)
        {
            Name = "Overgrown Lab";
            Description = $@"
You're in a room that looks like it may have once been a lab, but is now almost entirely covered by a strange black fungus or moss. Thorned vines
jut from cracks in the dark stone walls at odd intervals, and nearly all of the tables in the room have been overtaken by the growth. Only a small
alchemical workstation in one corner of the room appears to be undisturbed.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new AcridPowder(), new PutridNodule() };
            Items = new List<Item>() { new AcridPowder(), new PutridNodule() };
        }
    }

    public class GerminationVats : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public GerminationVats(int y, int x) : base(y, x)
        {
            Name = "Germination Vats";
            Description = $@"
Aside from a metal walkway around its edges, the floor of this room appears to have been rather haphazardly dug out. The gaping pit at its center is
filled with rows of massive glass containers filled with a bright green, cloudy liquid. A few of them seem to have large, dark shapes suspended at their center.
They look vaguely organic, but beyond that you can't quite identify them.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            RespawnItems = new List<Item>() { new AcridPowder(), new YellowIchor() };
            Items = new List<Item>() { new AcridPowder(), new YellowIchor() };
        }
    }

    public class MakeshiftLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public MakeshiftLab(int y, int x) : base(y, x)
        {
            Name = "Makeshift Lab";
            Description = $@"
The room appears to be some kind of kitchen and dining area that has been somewhat hapazardly repurposed as a lab. Pages and pages of notes cover almost every
available surface, and alchemical tools of all sorts seem to be scattered seemingly at random around the room.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new LuminousDust(), new QuiveringOoze() };
            Items = new List<Item>() { new LuminousDust(), new QuiveringOoze() };
        }
    }

    public class MakeshiftLibrary : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public MakeshiftLibrary(int y, int x) : base(y, x)
        {
            Name = "Makeshift Library";
            Description = $@"
The room looks to have been once used for food storage. The wooden shelves that line it's walls, however, are now filled entirely with books. They all look to be
fairly advanced alchemical texts, along with a few tomes on cross-planar travel and even more esoteric topics.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            RespawnItems = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
            Items = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
        }
    }
}
