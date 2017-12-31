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
        public Dictionary<string, Type> Rooms = new Dictionary<string, Type>()
        {
            {"MF", typeof(MainFoyer)},
            {"ER", typeof(EnemyRoom)},
            {"TR", typeof(TrapRoom)},
            {"AL", typeof(AssemblyLab)},
            {"SA", typeof(StorageArea)},
            {"OL", typeof(OvergrownLab)},
            {"GV", typeof(GerminationVats)},
            {"ML", typeof(MakeshiftLab)},
            {"HL", typeof(HaphazardLibrary)},
            {"RL", typeof(RefiningLab)},
            {"FR", typeof(FurnaceRoom)},
            {"AP", typeof(AbandonedParlor)},
            {"CS", typeof(ConvertedSunroom)},
            {"IG", typeof(ImmaculateGallery)},
            {"AS", typeof(AldricsStudy)},
            {"FC", typeof(FetidCourtyard)},
            {"MW", typeof(MacabreWorkroom)},
            {"CG", typeof(ClockworkGarden)},
            {"RW", typeof(RansackedWorkroom)},
            {"VR", typeof(ViviansRoom)},
            {"MS", typeof(MirandasStudio)},
            {"CL", typeof(CrystallographyLab)},
            {"OB", typeof(OpticsLab)},
            {"LL", typeof(LiquefactionLab)},
            {"VL", typeof(VaporizationLab)},
            {"GL", typeof(GrandLibrary)},
            {"TS", typeof(TheodoresStudy)},
            {"CC", typeof(CentralChamber)},
            {"FD", typeof(ForgottenDungeon)},
            {"DA", typeof(DustyArmory)},
            {"TM", typeof(TrophyRoom)},
            {"OR", typeof(OperatingRoom)}

        };
        public List<Trap> Traps = new List<Trap>()
        {
            new FlameTrap(), new FlameTrap(),
            new AcidTrap(), new AcidTrap(),
            new RiftTrap(), new RiftTrap()
        };

        public List<Enemy> Enemies = new List<Enemy>()
        {
            new KilnbornSentinel(), new CinderGolem(),
            new RavenousHusk(), new WeepingHorror(),
            new ShiftingSlime(), new FlickeringTerror()
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
                    if (templateRoom == "MF")
                    {
                        PlayerStartY = y;
                        PlayerStartX = x;
                    }
                    var roomType = Rooms[templateRoom];
                    var mapRoom = (Room)Activator.CreateInstance(Rooms[templateRoom], y, x);
                    if (templateRoom == "TR")
                    {
                        Random r = new Random();
                        int randIndex = r.Next(0, Traps.Count);
                        Trap trap = Traps[randIndex];
                        mapRoom.Trap = trap;
                        Traps.Remove(trap);
                    }
                    if (templateRoom == "ER")
                    {
                        Random r = new Random();
                        int randIndex = r.Next(0, Enemies.Count);
                        Enemy enemy = Enemies[randIndex];
                        mapRoom.Enemy = enemy;
                        Enemies.Remove(enemy);
                    }
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
                        topBorder += room.VisitedByPlayer ? ",___," : "?????";
                    }
                    else
                    {
                        if (room.Exits.Contains("n"))
                        {
                            topBorder += (room.VisitedByPlayer || Grid[y - 1][x].VisitedByPlayer) ? "|- -|" : "?????";
                        }
                        else
                        {
                            topBorder += (room.VisitedByPlayer || Grid[y - 1][x].VisitedByPlayer) ? "|---|" : "?????";
                        }
                    }

                    // Build Middle Border
                    string centerChar;

                    if (room.VisitedByPlayer)
                    {
                        if (player.Y == y && player.X == x)
                        {
                            centerChar = "o";
                        }
                        else
                        {
                            centerChar = " ";
                        }

                    }
                    else
                    {
                        centerChar = "?";
                    }

                    if (room.Exits.Contains("w"))
                    {
                        middleSection += room.VisitedByPlayer ? " " : "?";
                    }
                    else
                    {
                        middleSection += room.VisitedByPlayer ? "|" : "?";
                    }

                    middleSection += room.VisitedByPlayer ? $" {centerChar} " : $"?{centerChar}?";

                    if (room.Exits.Contains("e"))
                    {
                        middleSection += room.VisitedByPlayer ? " " : "?";
                    }
                    else
                    {
                        middleSection += room.VisitedByPlayer ? "|" : "?";
                    }

                    // Build Bottom Border
                    if (y == Grid.Count - 1)
                    {
                        bottomBorder += room.VisitedByPlayer ? "'---'" : "?????";
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
            @",___,,___,,___,";
            @"|        ||   |";
            @"|- -||---||- -|";
            @"|        ||   |";
            @"|- -||- -||- -|";
            @"|             |";
            @"|---||---||- -|";
            @"|   ||        |";
            @"'---''---''---'";
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


