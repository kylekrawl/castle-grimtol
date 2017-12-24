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
        public string MainQuestStage { get; set; }
        public bool SavedProgress { get; set; }
        public bool Playing { get; set; }
        public bool ApplicationActive { get; set; }
        ConsoleKeyInfo keyInfo;

        public void StartScreen()
        {
            SetConsoleColors();
            while (ApplicationActive)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine(@"  ____ ____ ____ ___ _    ____    ____ ____ _ _  _ ___ ____ _    
  |    |__| [__   |  |    |___    | __ |__/ | |\/|  |  |  | |    
  |___ |  | ___]  |  |___ |___    |__] |  \ | |  |  |  |__| |___ 
                                                               ");
                Console.WriteLine(@" 
                                                
                       

                 .|_
                  :;|_|        |_|_|_|
                 |_|_|_|        | = |
                  | = | :.,.  |_|_|_|_|
                |_|_|_|_|:;:   | = = |
                 | = | | = |   | = = |
               ';.;.|_|_|_|_|_|_|_|_|_|_|
                :;:.  =  =  =  =  =  = |
                |; = [-] =  =  =  =  = | 
              ..'''''''''''''''''''''''''.,
          ..''                            '.
       .''                                  ;
     .'                                      '.
  .''                                          ''.");
                Console.WriteLine("\n\n\n\n| N | New Game");
                if (SavedProgress)
                {
                    Console.WriteLine("| C | Continue");
                }
                Console.WriteLine("| Q | Exit");
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    ApplicationActive = false;
                    return;
                }
                if (keyInfo.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    Setup();
                }
                if (keyInfo.Key == ConsoleKey.C && SavedProgress)
                {
                    // Reset player position to room visited just before the room they died in / quit from
                    // Will eventually need to have more logic resetting the latter
                    CurrentPlayer.Y = CurrentPlayer.PreviousY;
                    CurrentPlayer.X = CurrentPlayer.PreviousX;
                    CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
                    Console.Clear();
                    Playing = true;
                    MainLoop();
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

        public void GameOver()
        {
            Playing = false;
            Console.Clear();
            SetConsoleColors("gameOver");
            Console.Clear();
            Console.WriteLine(@"____ ____ _  _ ____    ____ _  _ ____ ____ 
| __ |__| |\/| |___    |  | |  | |___ |__/ 
|__] |  | |  | |___    |__|  \/  |___ |  \ 
                                           ");
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
            StartScreen();
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
            SavedProgress = true;
            Intro();
            MainLoop();
            //Console.Clear();
        }

        public void Intro()
        {
            // Print intro text, keypress to advance
            // Choose player name

            Console.WriteLine("Choose a name:");
            var choice = Console.ReadLine();
            if (choice != "")
            {
                CurrentPlayer.Name = choice;
            }
            Console.Clear();
            Console.WriteLine(@"
After a week of traveling, you had finally arrived at Castle Grimtol. It had all started with a
hastily scrawled note from Dr. Damian Rithbaun, the man who had taught you everything you knew
about the art of alchemy. A note that said only 'I am in danger. Come to Grimtol.' After talking
with a few locals in the nearby town, you'd managed to piece together that the good Doctor had
been staying at the castle to teach the three Grimtol heirs in the arts of alchemy. Nothing all
that suspicious, except he hadn't been seen for weeks. Coupled with all the stories of strange 
happeningsin the vicinity of the castle grounds, there was definitely *something* going on. And now, 
standing before the castle doors, it was your job to figure it out.");

            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine($@"
{CurrentPlayer.Name}: 'What have you gotten yourself into this time, old man?'

You knock on the front door. No answer. A quick tug on the handle reveals that it's locked.

{CurrentPlayer.Name}: 'Figures.'

Just then, you hear a strange, almost mechanical scuffling sound from inside. After a few seconds, 
the door swings slowly open.

{CurrentPlayer.Name}: 'Well, that looks promising. Definitely not a trap. That old man is lucky I 
owe him one.'");

            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine($@"
You walk inside, and find yourself in a large room crafted from dark, polished stone. A collection
of tapestries and paintings adorn the walls, and a large, ornately carved table sits in the room's center.
A makeshift alchemical workstation takes up most of the table's space...probably Dr. Rithbaun's handiwork.
            
Suddenly, you hear the door slam shut behind you.

{CurrentPlayer.Name}: 'Knew it.'

As you turn around, you notice the door isn't just locked. It's gone. The wall is a smooth, stone surface
devoid of doors of any sort.

{CurrentPlayer.Name}: 'Huh...well, that's new.'");
            
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
                    Console.WriteLine("| I | View Inventory");
                    Console.WriteLine("| U | Use Item");
                    Console.WriteLine("| T | Take Item");
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
                    if (keyInfo.Key == ConsoleKey.I)
                    {
                        Console.Clear();
                        CheckInventory();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.T)
                    {
                        Console.Clear();
                        TakeItemInterface();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.U)
                    {
                        Console.Clear();
                        UseItemInterface();
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
            Console.Clear();
            for (var i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                var item = CurrentPlayer.Inventory[i];
                if (item.Name == itemName)
                {
                    Console.WriteLine($"You use the {item.Name}.\n");
                    CurrentRoom.UseItem(item);
                    break;
                    // may need to retool this if have multiple instances of same item with multiple (but limited) uses 
                }
            }

        }
        public void TakeItem(string itemName)
        {
            for (var i = 0; i < CurrentRoom.Items.Count; i++)
            {
                var item = CurrentRoom.Items[i];
                if (itemName == item.Name)
                {
                    CurrentPlayer.Inventory.Add(item);
                    CurrentRoom.Items.Remove(item);
                    break;
                }
            }
        }
        public void UseItemInterface()
        {
            if (CurrentPlayer.Inventory.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("\nChoose an item (Type number and press <Enter>):\n");
                for (var i = 0; i < CurrentPlayer.Inventory.Count; i++)
                {
                    var item = CurrentPlayer.Inventory[i];
                    Console.WriteLine($"{i + 1}. {item.Name}");
                    Console.WriteLine($"-----------------------------");
                    Console.WriteLine($"{item.Description}");
                    Console.WriteLine($"-----------------------------\n");
                }
                var choice = Console.ReadLine();
                var parsed = 0;
                var valid = int.TryParse(choice, out parsed);
                if (!valid || parsed < 1 || parsed > CurrentPlayer.Inventory.Count)
                {
                    Console.WriteLine("Invalid choice.");
                }
                else
                {
                    UseItem(CurrentPlayer.Inventory[parsed - 1].Name);
                }
            }
            else
            {
                Console.WriteLine("There are no items to use.");
            }
        }
        public void TakeItemInterface()
        {
            if (CurrentRoom.Items.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("\nChoose an item (type number and press <Enter>):\n");
                for (var i = 0; i < CurrentRoom.Items.Count; i++)
                {
                    var item = CurrentRoom.Items[i];
                    Console.WriteLine($"{i + 1}. {item.Name}");
                    Console.WriteLine($"-----------------------------");
                    Console.WriteLine($"{item.Description}");
                    Console.WriteLine($"-----------------------------\n");
                }
                var choice = Console.ReadLine();
                var parsed = 0;
                var valid = int.TryParse(choice, out parsed);
                if (!valid || parsed < 1 || parsed > CurrentRoom.Items.Count)
                {
                    Console.WriteLine("Invalid choice.");
                }
                else
                {
                    Console.WriteLine($"You take the {CurrentRoom.Items[parsed - 1].Name}.");
                    TakeItem(CurrentRoom.Items[parsed - 1].Name);

                }
            }
            else
            {
                Console.WriteLine("There are no items to take.");
            }
        }
        public void CheckInventory()
        {
            if (CurrentPlayer.Inventory.Count > 0)
            {
                Console.WriteLine("\nThe following items are in your inventory:\n");
                for (var i = 0; i < CurrentPlayer.Inventory.Count; i++)
                {
                    var item = CurrentPlayer.Inventory[i];
                    Console.WriteLine($"{item.Name}");
                    Console.WriteLine($"-----------------------------");
                    Console.WriteLine($"{item.Description}");
                    Console.WriteLine($"-----------------------------\n");
                }
                Console.WriteLine("| U | Use Item");
                Console.WriteLine("\n<Press any other key to continue.>");
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.U)
                {
                    Console.Clear();
                    UseItemInterface();
                }
            }
            else
            {
                Console.WriteLine("There are no items in your inventory.");
            }
        }
        public void Look()
        {
            Console.WriteLine($"{CurrentRoom.Name}");
            Console.WriteLine($"{CurrentRoom.Description}");
            if (CurrentRoom.Items.Count > 0)
            {
                Console.WriteLine("\nThe following items are in the room:\n");
                for (var i = 0; i < CurrentRoom.Items.Count; i++)
                {
                    var item = CurrentRoom.Items[i];
                    Console.WriteLine($"{item.Name}");
                    Console.WriteLine($"-----------------------------");
                    Console.WriteLine($"{item.Description}");
                    Console.WriteLine($"-----------------------------\n");
                }
            }
        }
        public void MovePlayer(int dy, int dx)
        {
            CurrentPlayer.PreviousY = CurrentPlayer.Y;
            CurrentPlayer.PreviousX = CurrentPlayer.X;
            CurrentPlayer.Y += dy;
            CurrentPlayer.X += dx;
            CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
            if (!CurrentRoom.VisitedByPlayer)
            {
                CurrentRoom.VisitedByPlayer = true;
            }
            Look();
            CurrentRoom.Event(this, CurrentPlayer);
            //Fix for bug in which after Event an causes Game Over screen, game loop does not exit properly when player quits after Continuing Game
            if (!Playing || !ApplicationActive)
            {
                return;
            }
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public Game()
        {
            ApplicationActive = true;
            MapTemplate = @"ER:TR:ER:TR:ER:ER:TR.
                            TR:TR:TR:ER:TR:ER:TR.
                            ER:TR:ER:TR:ER:ER:TR.
                            TR:TR:TR:DR:TR:ER:TR.
                            TR:TR:TR:DR:TR:ER:TR.
                            TR:MF:TR:ER:TR:ER:TR.";
            StartScreen();
        }
    }
}
/*
 
                                                
                       

                 .|_
                  :;|_|        |_|_|_|
                 |_|_|_|        | = |
                  | = | :.,.  |_|_|_|_|
                |_|_|_|_|:;:   | = = |
                 | = | | = |   | = = |
               ';.;.|_|_|_|_|_|_|_|_|_|_|
                :;:.  =  =  =  =  =  = |
                |; = [-] =  =  =  =  = | 
              ..'''''''''''''''''''''''''.,
          ..''                            '.
       .''                                  ;
     .'                                      '.
  .''                                          ''.
 */
