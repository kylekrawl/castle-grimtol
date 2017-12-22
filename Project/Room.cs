using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    // public class Room : IRoom
    // {
    //     public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //     public string Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //     public List<Item> Items { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //     public void UseItem(Item item)
    //     {
    //         throw new System.NotImplementedException();
    //     }
    // }

    public abstract class Room : IRoom
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public List<Item> Items { get; set; }
        public int Y;
        public int X;
        public bool PassagesBuilt;
        public List<string> Exits = new List<string>();

        public void UseItem(Item item)
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

        public TestRoom(int y, int x) : base(y, x)
        {
            Name = "Test Room";
            Description = $"Test Room at ({x} , {y})";
            Y = y;
            X = x;
            //PassagesBuilt = false;
            //Exits =  new List<string>();
        }

    }

    public class EmptyRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }

        public EmptyRoom(int y, int x) : base(y, x)
        {
            Name = "Empty Cell";
            Description = $"Test Room at ({x} , {y})";
            Y = y;
            X = x;
            //PassagesBuilt = false;
            //Exits =  new List<string>();
        }
    }
}
