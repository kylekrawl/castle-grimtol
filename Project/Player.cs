using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(Map map)
        {
            Score = 0;
            Inventory = new List<Item>(){};
            X = map.PlayerStartX;
            Y = map.PlayerStartY;
        }
    }
}