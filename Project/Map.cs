using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Map
    {
        public string Template { get; set; }
        public string Name { get; set; }
        public int PlayerStartY { get; set; }
        public int PlayerStartX { get; set; }
        public Dictionary<string, Type> RoomDict = new Dictionary<string, Type>()
        {
            {"TR", typeof(TestRoom)},
            {"ER", typeof(EmptyRoom)},
            {"SR", typeof(StartRoom)}
        };

        public List<List<Room>> Grid = new List<List<Room>>();

        public void GenerateGrid()
        {
            var templateRows = Template.Split(".");
            for (int y = 0; y < templateRows.Length - 1; y++)
            {
                string templateRow = templateRows[y].Trim();
                var mapRow = new List<Room>();
                var templateRooms = templateRow.Split(":");
                for (int x = 0; x < templateRooms.Length; x++)
                {
                    var templateRoom = templateRooms[x].Trim();
                    if (templateRoom == "SR") {
                        PlayerStartY = y;
                        PlayerStartX = x;
                    }
                    var roomType = RoomDict[templateRoom];
                    var mapRoom = (Room)Activator.CreateInstance(RoomDict[templateRoom], y, x);
                    mapRow.Add(mapRoom);
                }
                Grid.Add(mapRow);
            }
        }

        public void GeneratePassages()
        {
            int numRows = Grid.Count;
            int numColumns = Grid[0].Count;
            int y = 0;
            int x = 0;
            var buildPath = new List<List<int>>()
            {
                new List<int>(){y, x}
            };
            while (buildPath.Count > 0)
            {
                var currentRoom = Grid[y][x];
                currentRoom.PassagesBuilt = true;
                var validAdjacentRooms = new List<string>();
                if (x > 0 && !Grid[y][x - 1].PassagesBuilt)
                {
                    validAdjacentRooms.Add("w");
                }
                if (y > 0 && !Grid[y - 1][x].PassagesBuilt)
                {
                    validAdjacentRooms.Add("n");
                }
                if (x < numColumns - 1 && !Grid[y][x + 1].PassagesBuilt)
                {
                    validAdjacentRooms.Add("e");
                }
                if (y < numRows - 1 && !Grid[y + 1][x].PassagesBuilt)
                {
                    validAdjacentRooms.Add("s");
                }

                if (validAdjacentRooms.Count > 0)
                {
                    buildPath.Add(new List<int> { y, x });
                    Random r = new Random();
                    int randIndex = r.Next(0, validAdjacentRooms.Count);
                    var moveDirection = validAdjacentRooms[randIndex];
                    if (moveDirection == "w")
                    {
                        currentRoom.Exits.Add("w");
                        x--;
                        Grid[y][x].Exits.Add("e");
                    }
                    if (moveDirection == "n")
                    {
                        currentRoom.Exits.Add("n");
                        y--;
                        Grid[y][x].Exits.Add("s");
                    }
                    if (moveDirection == "e")
                    {
                        currentRoom.Exits.Add("e");
                        x++;
                        Grid[y][x].Exits.Add("w");
                    }
                    if (moveDirection == "s")
                    {
                        currentRoom.Exits.Add("s");
                        y++;
                        Grid[y][x].Exits.Add("n");
                    }
                }
                else
                {
                    y = buildPath[buildPath.Count - 1][0];
                    x = buildPath[buildPath.Count - 1][1];
                    buildPath.Remove(buildPath[buildPath.Count - 1]);
                }
            }
        }

        public void PrintMap(Player player)
        {

            for (var y = 0; y < Grid.Count; y++)
            {
                var topBorder = "";
                var middleSection = "";
                var bottomBorder = "";

                for (var x = 0; x < Grid[y].Count; x++)
                {
                    var room = Grid[y][x];

                    // Build Top Border

                    if (y == 0)
                    {
                        topBorder += ",___,";
                    }
                    else
                    {
                        if (room.Exits.Contains("n"))
                        {
                            topBorder += "|- -|";
                        }
                        else
                        {
                            topBorder += "|---|";
                        }
                    }

                    // Build Middle Border
                    var centerChar = player.Y == y && player.X == x ? "o" : " ";
                    if (room.Exits.Contains("w"))
                    {
                        middleSection += " ";
                    }
                    else
                    {
                        middleSection += "|";
                    }

                    middleSection += $" {centerChar} ";

                    if (room.Exits.Contains("e"))
                    {
                        middleSection += " ";
                    }
                    else
                    {
                        middleSection += "|";
                    }

                    // Build Bottom Border
                    if (y == Grid[y].Count - 1)
                    {
                        bottomBorder += "'---'";
                    }
                }

                //Console.WriteLine($"{topBorder}\n{middleSection}\n{bottomBorder}");
                Console.WriteLine(topBorder);
                Console.WriteLine(middleSection);
                if (!(bottomBorder == ""))
                {
                    Console.WriteLine(bottomBorder);
                }

            }
            /*
            var ct0 = @",___,,___,,___,";
            var ct1 = @"|        ||   |";
            var ct2 = @"|- -||---||- -|";
            var ct3 = @"|        ||   |";
            var ct4 = @"|- -||- -||- -|";
            var ct5 = @"|             |";
            var ct6 = @"|---||---||- -|";
            var ct7 = @"|   ||        |";
            var ct8 = @"'---''---''---'";
            */
        }

        public bool ValidRoom(int y, int x)
        {
            bool valid = true;
            bool inYRange = y >= 0 && y < Grid.Count;
            if (!inYRange)
            {
                valid = false;
            }
            else
            {
                if (!(x >= 0 && x < Grid[y].Count))
                {
                    valid = false;
                }
            }
            return valid;
        }

        public Map(string template, string name)
        {
            Template = template;
            Name = name;
            Grid = new List<List<Room>>();
            GenerateGrid();
            GeneratePassages();
        }
    }
}


