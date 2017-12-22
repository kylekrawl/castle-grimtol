using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public string MapTemplate { get; set; }
        public Map CurrentMap { get; set; }
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        //public bool SavedProgress { get; set; }
        public bool Playing { get; set; }
        ConsoleKeyInfo keyInfo;

        public void StartScreen()
        {
            SetConsoleColors();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"____ ____ ____ ___ _    ____    ____ ____ _ _  _ ___ ____ _    
|    |__| [__   |  |    |___    | __ |__/ | |\/|  |  |  | |    
|___ |  | ___]  |  |___ |___    |__] |  \ | |  |  |  |__| |___ 
                                                               ");
                Console.WriteLine("\n| N | New Game");
                Console.WriteLine("| Q | Exit");
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    return;
                }
                if (keyInfo.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    Setup();
                }
                // Print title
                // Print options:
                // if no game started:
                //  - New Game
                // Run Setup
                // if game started:
                //  - Restart Game
                // Run Reset
                //  - Continue (if game begun)
                // Run MainLoop without resetting
                //  - Exit
            }

        }

        public void GameOverScreen()
        {
            Console.WriteLine(@"____ ____ _  _ ____    ____ _  _ ____ ____ 
| __ |__| |\/| |___    |  | |  | |___ |__/ 
|__] |  | |  | |___    |__|  \/  |___ |  \ 
                                           ");
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public void SetConsoleColors(string mode = "default")
        {
            if (mode == "default")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (mode == "alternate")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (mode == "gameOver")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Reset()
        {
            Setup(); // What different logic is needed here?
        }

        public void Setup()
        {
            // Set Playing to true
            // Generate Player
            // Generate Map
            // Set Player Location to Start Location On Map
            // Play Intro
            // Run MainLoop
            Playing = true;
            CurrentMap = new Map(MapTemplate, "Test");
            CurrentPlayer = new Player(CurrentMap);
            CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
            Intro();
            MainLoop();
            //Console.Clear();
        }

        public void Intro()
        {
            // Print intro text, keypress to advance
            // Choose player name?
            Console.WriteLine("TODO: Add actual game intro here.");
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public void HelpScreen()
        {
            // Will need a way to prevent this from wasting player's turn if in combat.
            // Might be able to bundle player action prompt in method and call again in
            // a way that avoids triggering enemy action round
            Console.WriteLine("TODO: Add actual 'Help' info here.");
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public void MainLoop()
        {
            while (Playing)
            {
                while (true)
                {
                    var validActionKeys = new List<string>() { };
                    Console.Clear();
                    CurrentMap.PrintMap(CurrentPlayer);
                    Console.WriteLine($"{CurrentRoom.Name}");
                    Console.WriteLine("\nPress key to choose an action:");
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y, CurrentPlayer.X - 1) && CurrentRoom.Exits.Contains("w"))
                    {
                        validActionKeys.Add("a");
                        Console.WriteLine("| A | Go West");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y - 1, CurrentPlayer.X) && CurrentRoom.Exits.Contains("n"))
                    {
                        validActionKeys.Add("w");
                        Console.WriteLine("| W | Go North");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y, CurrentPlayer.X + 1) && CurrentRoom.Exits.Contains("e"))
                    {
                        validActionKeys.Add("d");
                        Console.WriteLine("| D | Go East");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y + 1, CurrentPlayer.X) && CurrentRoom.Exits.Contains("s"))
                    {
                        validActionKeys.Add("s");
                        Console.WriteLine("| S | Go South");
                    }
                    Console.WriteLine("| L | Look");
                    Console.WriteLine("| H | Help");
                    Console.WriteLine("| Q | Quit");

                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Playing = false;
                        return;
                    }
                    if (keyInfo.Key == ConsoleKey.H)
                    {
                        Console.Clear();
                        HelpScreen();
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.L)
                    {
                        Console.Clear();
                        Look();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if ((keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow) && validActionKeys.Contains("a"))
                    {
                        Console.Clear();
                        MovePlayer(0, -1);
                        break;
                    }
                    if ((keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow) && validActionKeys.Contains("w"))
                    {
                        Console.Clear();
                        MovePlayer(-1, 0);
                        break;
                    }
                    if ((keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow) && validActionKeys.Contains("d"))
                    {
                        Console.Clear();
                        MovePlayer(0, 1);
                        break;
                    }
                    if ((keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow) && validActionKeys.Contains("s"))
                    {
                        Console.Clear();
                        MovePlayer(1, 0);
                        break;
                    }
                    Console.WriteLine("Invalid action.");
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }


                // Run room action
                // Room.MainEffects(player?)
                // Get valid player actions based on room/player conditions
                // if player dead, route to game over
                // else
                // if player not in combat or otherwise immobilized (certain trap rooms?)
                // if valid room to x direction move x direction
                // check inventory
                // look - replay room description? Allow deeper inspection of room features?
                // use noncombat item
                // if room is crafting station
                // if alchemy station use alchemy station options
                // if weapon upgrade station use weapon upgrade options
                // else if player in combat
                // attack enemy
                // use combat item
                // quit game
                // return to start screen
                // Get player action
            }
        }

        public void UseItem(string itemName)
        {
            throw new System.NotImplementedException();
        }
        public void Look()
        {
            Console.WriteLine($"{CurrentRoom.Name}");
            Console.WriteLine($"{CurrentRoom.Description}");
        }
        public void MovePlayer(int dy, int dx)
        {
            CurrentPlayer.Y += dy;
            CurrentPlayer.X += dx;
            CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
            if (!CurrentRoom.VisitedByPlayer)
            {
                CurrentRoom.VisitedByPlayer = true;
            }
            Look();
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public Game()
        {
            MapTemplate = @"ER:TR:ER:TR:ER:ER.
                            TR:TR:TR:ER:TR:ER.
                            ER:TR:ER:TR:ER:ER.
                            TR:SR:TR:ER:TR:ER.
                            TR:ER:TR:ER:TR:ER.";
            StartScreen();
        }
    }
}