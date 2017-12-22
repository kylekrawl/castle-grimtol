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
            while (true)
            {
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
            CurrentPlayer = new Player();
            CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
            MainLoop();
            //Console.Clear();
        }

        public void Intro()
        {
            // Print intro text, keypress to advance
            // Choose player name?
        }

        public void MainLoop()
        {
            while (Playing)
            {
                while (true)
                {
                    var validActionKeys = new List<string>(){};
                    Console.Clear();
                    CurrentMap.PrintMap(CurrentPlayer);
                    Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
                    Console.WriteLine("\nPress key to choose an action:");
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y, CurrentPlayer.X - 1) && CurrentRoom.Exits.Contains("w"))
                    {
                        validActionKeys.Add("a");
                        Console.WriteLine("| a | Go West");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y - 1, CurrentPlayer.X) && CurrentRoom.Exits.Contains("n"))
                    {
                        validActionKeys.Add("w");
                        Console.WriteLine("| w | Go North");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y, CurrentPlayer.X + 1) && CurrentRoom.Exits.Contains("e"))
                    {
                        validActionKeys.Add("d");
                        Console.WriteLine("| d | Go East");
                    }
                    if (CurrentMap.ValidRoom(CurrentPlayer.Y + 1, CurrentPlayer.X) && CurrentRoom.Exits.Contains("s"))
                    {
                        validActionKeys.Add("s");
                        Console.WriteLine("| s | Go South");
                    }
                    Console.WriteLine("| q | Quit");
                    foreach (var key in validActionKeys)
                    {
                        Console.WriteLine($"valid: {key}");
                    }
                    keyInfo = Console.ReadKey(true);
                    Console.WriteLine("key pressed: " + keyInfo.Key);
                    if (keyInfo.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Playing = false;
                        return;
                    }
                    if (keyInfo.Key == ConsoleKey.A && validActionKeys.Contains("a"))
                    {
                        Console.Clear();
                        MovePlayer(0, -1);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.W && validActionKeys.Contains("w"))
                    {
                        Console.Clear();
                        MovePlayer(-1, 0);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.D && validActionKeys.Contains("d"))
                    {
                        Console.Clear();
                        MovePlayer(0, 1);
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.S && validActionKeys.Contains("s"))
                    {
                        Console.Clear();
                        MovePlayer(1, 0);
                        break;
                    }
                    Console.WriteLine("Invalid action.");
                    Console.WriteLine("\nPress any key...");
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

        public void MovePlayer(int dy, int dx)
        {
            CurrentPlayer.Y += dy;
            CurrentPlayer.X += dx;
            CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
        }

        public Game()
        {
            MapTemplate = @"ER:TR:ER:TR.
                            TR:ER:TR:ER.
                            ER:TR:ER:TR.
                            TR:ER:TR:ER.";
            //StartScreen();
            Setup(); // For testing, switch to StartScreen() later
        }
    }
}