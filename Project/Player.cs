using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }
        public List<Item> Inventory { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Move(int x, int y)
        {
            Y += y;
            X += x;
        }

        public Player()
        {
            Score = 0;
            // Will need to pass map to player constructor eventually
            X = 0;
            Y = 0;
        }
    }
}