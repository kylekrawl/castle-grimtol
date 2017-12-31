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
        public Dictionary<string, string> MainQuestStage { get; set; } = new Dictionary<string, string>(){
            {"purification", "start"},
            {"corruption", "start"},
            {"transmutation", "start"}
        };
        public bool SavedProgress { get; set; }
        public List<Type> CraftableItems { get; set; } = new List<Type>()
        {
            typeof(IncendiaryPistol),
            typeof(VenomousPistol),
            typeof(WarpPistol),
            typeof(IncendiaryGrenade),
            typeof(VenomousGrenade),
            typeof(WarpGrenade),
            typeof(HealingElixir),
            typeof(MedicinalSalve),
            typeof(Panacea),
            typeof(PulseEmitter),
            typeof(ReactiveSolid)

        };
        public Dictionary<string, string> EnemyWeaknesses { get; set; } = new Dictionary<string, string>() {
            {"purification", "corrosive"},
            {"corruption", "burning"},
            {"transmutation", "chaos"}
        };
        public List<string> CombatItems { get; set; } = new List<string>() {
            "Incendiary Grenade",
            "Venomous Grenade",
            "Warp Grenade"
        };
        public List<string> HealingItems { get; set; } = new List<string>() {
            "Healing Elixir",
            "Medicinal Salve",
            "Panacea"
        };
        public bool Playing { get; set; }
        public bool ApplicationActive { get; set; }
        public ConsoleKeyInfo KeyInfo { get; set; }

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
                KeyInfo = Console.ReadKey(true);
                if (KeyInfo.Key == ConsoleKey.Q)
                {
                    Console.Clear();
                    ApplicationActive = false;
                    return;
                }
                if (KeyInfo.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    Setup();
                }
                if (KeyInfo.Key == ConsoleKey.C && SavedProgress)
                {
                    // Reset player position to room visited just before the room they died in / quit from
                    // Will eventually need to have more logic resetting the latter
                    CurrentPlayer.Y = CurrentPlayer.PreviousY;
                    CurrentPlayer.X = CurrentPlayer.PreviousX;
                    CurrentRoom = CurrentMap.Grid[CurrentPlayer.Y][CurrentPlayer.X];
                    CurrentPlayer.Health = CurrentPlayer.MaxHealth / 2;
                    Console.Clear();
                    Playing = true;
                    MainLoop();
                }
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
happenings in the vicinity of the castle grounds, there was definitely *something* going on. And now, 
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
            Console.Clear();
            CurrentRoom.Event(this, CurrentPlayer);
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);
        }

        public void HelpScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nChoose a topic (Type number and press <Enter>):\n");
                Console.WriteLine("1. Movement");
                Console.WriteLine("2. Basic Commands");
                Console.WriteLine("3. Crafting");
                Console.WriteLine("4. Combat");
                var choice = Console.ReadLine();
                var parsed = 0;
                var valid = int.TryParse(choice, out parsed);
                if (!valid || parsed < 1 || parsed > 4)
                {
                    Console.WriteLine("Invalid choice.");
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.Clear();
                    if (parsed == 1)
                    {
                        Console.WriteLine("\nUnless otherwise stated, all commands are performed by pressing a single key.");
                        Console.WriteLine("\n-------------------------------");
                        Console.WriteLine("MOVEMENT:");
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("\nOnly move directions that lead to valid passageways will be available.\n");
                        Console.WriteLine("Go West  | A or ←");
                        Console.WriteLine("Go North | W or ↑");
                        Console.WriteLine("Go East  | D or →");
                        Console.WriteLine("Go South | S or ↓");
                    }
                    if (parsed == 2)
                    {
                        Console.WriteLine("\nUnless otherwise stated, all commands are performed by pressing a single key.");
                        Console.WriteLine("\n-------------------------------");
                        Console.WriteLine("BASIC COMMANDS:");
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("\nLook | L");
                        Console.WriteLine("Review the description of the current room, including takeable items.\n");
                        Console.WriteLine("\nView Notebook | N");
                        Console.WriteLine("Review the notes you've collected.");
                        Console.WriteLine("\nView Inventory | I");
                        Console.WriteLine("View the items in your inventory.");
                        Console.WriteLine("\nUse Item | U");
                        Console.WriteLine("Brings up the Use Item menu, displaying a numbered list of items in your inventory.\n Type a number and press ENTER to use the corresponding item in the current room.");
                        Console.WriteLine("\nTake Item | T");
                        Console.WriteLine("Brings up the Take Item menu, displaying a numbered list of items in the room.\n Type a number and press ENTER to add the corresponding item to your inventory.");
                        Console.WriteLine("This command is only available if there are takeable items in the room.");
                        Console.WriteLine("\nCraft Item | C");
                        Console.WriteLine("Brings up the Craft Item menu (see CRAFTING below.)");
                        Console.WriteLine("This command is only available if there is an alchemical workstation in the current room.");
                        Console.WriteLine("\nQuit | Q");
                        Console.WriteLine("Exits the game, returning you to the Start Menu. \nYour current progress will be saved as long as you do not fully exit the application.");
                    }
                    if (parsed == 3)
                    {
                        Console.WriteLine("\nUnless otherwise stated, all commands are performed by pressing a single key.");
                        Console.WriteLine("\n-------------------------------");
                        Console.WriteLine("CRAFTING:");
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("\nThe Craft Item Menu allows you to attempt to combine two items.");
                        Console.WriteLine("Opening the Craft Item Menu will display a numbered list of items in your inventory.\n You will be prompted to select an item to use as the first crafting component,\n which can be done by typing a number from the list and pressing ENTER.");
                        Console.WriteLine("This process will be repeated for the second component.\n Once two components are selected, if they are a valid combination they will both be consumed, and a new item will be added to your inventory.");
                        Console.WriteLine("If the combination is invalid, the items will not be consumed.");
                    }
                    if (parsed == 4)
                    {
                        Console.WriteLine("\nUnless otherwise stated, all commands are performed by pressing a single key.");
                        Console.WriteLine("\n-------------------------------");
                        Console.WriteLine("COMBAT:");
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("todo");
                        Console.WriteLine("-------------------------------");
                    }
                    Console.WriteLine("\n| X | Exit Help Menu");
                    Console.WriteLine("\n<Press any other key to continue.>");
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.X)
                    {
                        Console.Clear();
                        break;
                    }
                }

            }
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
                    Console.WriteLine($"\n{CurrentPlayer.Name}: {CurrentPlayer.Health} / {CurrentPlayer.MaxHealth} HP");
                    Console.WriteLine($"\n{CurrentRoom.Name}");
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
                    if (CurrentPlayer.Notes.Count > 0)
                    {
                        validActionKeys.Add("n");
                        Console.WriteLine("| N | View Notebook");
                    }
                    if (CurrentPlayer.Inventory.Count > 0)
                    {
                        validActionKeys.Add("i");
                        validActionKeys.Add("u");
                        Console.WriteLine("| I | View Inventory");
                        Console.WriteLine("| U | Use Item");
                    }
                    if (CurrentRoom.Items.Count > 0)
                    {
                        validActionKeys.Add("t");
                        Console.WriteLine("| T | Take Item");
                    }
                    if (CurrentRoom.CraftingArea)
                    {
                        validActionKeys.Add("c");
                        Console.WriteLine("| C | Craft Item");
                    }
                    Console.WriteLine("| H | Help");
                    Console.WriteLine("| Q | Quit");

                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Playing = false;
                        return;
                    }
                    if (KeyInfo.Key == ConsoleKey.H)
                    {
                        Console.Clear();
                        HelpScreen();
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.L)
                    {
                        Console.Clear();
                        Look();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.N && validActionKeys.Contains("n"))
                    {
                        Console.Clear();
                        CheckNotes();
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.I && validActionKeys.Contains("i"))
                    {
                        Console.Clear();
                        CheckInventory();
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.T && validActionKeys.Contains("t"))
                    {
                        Console.Clear();
                        TakeItemInterface();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.U && validActionKeys.Contains("u"))
                    {
                        Console.Clear();
                        UseItemInterface();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if (KeyInfo.Key == ConsoleKey.C && validActionKeys.Contains("c"))
                    {
                        Console.Clear();
                        CraftItemInterface();
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        break;
                    }
                    if ((KeyInfo.Key == ConsoleKey.A || KeyInfo.Key == ConsoleKey.LeftArrow) && validActionKeys.Contains("a"))
                    {
                        Console.Clear();
                        MovePlayer(0, -1);
                        break;
                    }
                    if ((KeyInfo.Key == ConsoleKey.W || KeyInfo.Key == ConsoleKey.UpArrow) && validActionKeys.Contains("w"))
                    {
                        Console.Clear();
                        MovePlayer(-1, 0);
                        break;
                    }
                    if ((KeyInfo.Key == ConsoleKey.D || KeyInfo.Key == ConsoleKey.RightArrow) && validActionKeys.Contains("d"))
                    {
                        Console.Clear();
                        MovePlayer(0, 1);
                        break;
                    }
                    if ((KeyInfo.Key == ConsoleKey.S || KeyInfo.Key == ConsoleKey.DownArrow) && validActionKeys.Contains("s"))
                    {
                        Console.Clear();
                        MovePlayer(1, 0);
                        break;
                    }
                    Console.WriteLine("Invalid action.");
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }
            }
        }
        public void GetNote() {
            if (!(CurrentRoom.Note == null))
            {
                CurrentPlayer.Notes.Add(CurrentRoom.Note);
                Console.WriteLine($"'{CurrentRoom.Note.Name}' has been added to your notebook.");
                CurrentRoom.Note = null;
            }
        }
        public void TrapEvent()
        {
            if (CurrentRoom.Trap.Active)
            {
                Console.WriteLine(CurrentRoom.Trap.TriggeredText);
                var canDisable = false;
                Item disableItem = null;
                foreach (var item in CurrentPlayer.Inventory)
                {
                    if (item.Name == "Pulse Emitter")
                    {
                        canDisable = true;
                        disableItem = item;
                        break;
                    }
                }
                if (canDisable)
                {
                    Console.WriteLine("\n| U | Use Pulse Emitter");
                    Console.WriteLine("\n<Press any other key to continue.>");
                }
                else
                {
                    Console.WriteLine("\n<Press any key to continue.>");
                }
                KeyInfo = Console.ReadKey(true);
                if (KeyInfo.Key == ConsoleKey.U && canDisable)
                {
                    Console.Clear();
                    Console.WriteLine("\nYou quickly activate a Pulse Emitter and toss it into the center of the room. It emits a strange blue light, follwed by a piercing tone.");
                    Console.WriteLine(CurrentRoom.Trap.FailureText);
                    CurrentPlayer.Inventory.Remove(disableItem);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(CurrentRoom.Trap.SuccessText);
                    CurrentPlayer.Health -= CurrentRoom.Trap.Damage;
                    if (CurrentPlayer.Health <= 0)
                    {
                        Console.WriteLine($"\nYou have been killed by the {CurrentRoom.Trap.Name}.");
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        GameOver();
                    }
                    if (!Playing || !ApplicationActive)
                    {
                        return;
                    }
                }
                CurrentRoom.Trap.Active = false;
            }
            else
            {
                Console.WriteLine(CurrentRoom.Trap.DisabledText);
            }
        }
        public void Combat(Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine(enemy.ApproachDescription);
            Console.WriteLine("\n<Press any key to continue.>");
            Console.ReadKey(true);

            while (true)
            {
                var validCombatItems = new List<Item>();
                var validHealingItems = new List<Item>();
                Item chosenCombatItem = null;
                Item chosenHealingItem = null;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(enemy.CombatDescription);

                    for (var i = 0; i < CurrentPlayer.Inventory.Count; i++)
                    {
                        var item = CurrentPlayer.Inventory[i];
                        if (CombatItems.Contains(item.Name))
                        {
                            validCombatItems.Add(item);
                        }
                        if (HealingItems.Contains(item.Name))
                        {
                            validHealingItems.Add(item);
                        }
                    }
                    Console.WriteLine($"\n{CurrentRoom.Enemy.Name}: {CurrentRoom.Enemy.Health} / {CurrentRoom.Enemy.MaxHealth} HP");
                    Console.WriteLine($"\n{CurrentPlayer.Name}: {CurrentPlayer.Health} / {CurrentPlayer.MaxHealth} HP");
                    Console.WriteLine("\nPress key to choose an action:");
                    Console.WriteLine("| A | Pistol Attack");
                    if (validCombatItems.Count > 0)
                    {
                        Console.WriteLine("| C | Use Combat Item");
                    }
                    if (validHealingItems.Count > 0)
                    {
                        Console.WriteLine("| H | Use Healing Item");
                    }
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.A)
                    {
                        foreach (var item in CurrentPlayer.Inventory)
                        {
                            if (item.Name.Split(" ")[1] == "Pistol")
                            {
                                chosenCombatItem = item;
                            }
                        }
                        break;
                    }
                    else if (KeyInfo.Key == ConsoleKey.C && validCombatItems.Count > 0)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("\nChoose an item (Type number and press <Enter>):\n");
                            for (var i = 0; i < validCombatItems.Count; i++)
                            {
                                var item = validCombatItems[i];
                                Console.WriteLine($"{i + 1}. {item.Name}");
                                Console.WriteLine($"-----------------------------");
                                Console.WriteLine($"{item.Description}");
                                Console.WriteLine($"-----------------------------\n");
                            }
                            var choice = Console.ReadLine();
                            var parsed = 0;
                            var valid = int.TryParse(choice, out parsed);
                            if (!valid || parsed < 1 || parsed > validCombatItems.Count)
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                            else
                            {
                                chosenCombatItem = validCombatItems[parsed - 1];
                                CurrentPlayer.Inventory.Remove(chosenCombatItem);
                                break;
                            }
                        }
                        break;
                    }
                    else if (KeyInfo.Key == ConsoleKey.H && validHealingItems.Count > 0)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("\nChoose an item (Type number and press <Enter>):\n");
                            for (var i = 0; i < validHealingItems.Count; i++)
                            {
                                var item = validHealingItems[i];
                                Console.WriteLine($"{i + 1}. {item.Name}");
                                Console.WriteLine($"-----------------------------");
                                Console.WriteLine($"{item.Description}");
                                Console.WriteLine($"-----------------------------\n");
                            }
                            var choice = Console.ReadLine();
                            var parsed = 0;
                            var valid = int.TryParse(choice, out parsed);
                            if (!valid || parsed < 1 || parsed > validHealingItems.Count)
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                            else
                            {
                                chosenHealingItem = validHealingItems[parsed - 1];
                                CurrentPlayer.Inventory.Remove(chosenHealingItem);
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                    }
                }
                Console.Clear();
                if (!(chosenCombatItem == null))
                {
                    if (chosenCombatItem.Name.Split(" ")[1] == "Pistol")
                    {
                        Console.WriteLine($"\nYou carefully take aim at {enemy.Name} and fire a shot from your {chosenCombatItem.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"\nYou lob the {chosenCombatItem.Name} at {enemy.Name}.");
                    }
                    Random r = new Random();
                    int attackVal = r.Next(1, 21);
                    if (attackVal >= enemy.DefenseRating)
                    {
                        var damage = chosenCombatItem.Damage;
                        if (EnemyWeaknesses[enemy.Type] == chosenCombatItem.DamageType)
                        {
                            damage *= 1.5;
                        }
                        Console.WriteLine($"\n{enemy.Name} takes {damage} damage.");
                        enemy.Health -= damage;
                    }
                    else
                    {
                        Console.WriteLine($"\n{enemy.Name} dodges the attack.");
                    }
                }
                if (!(chosenHealingItem == null))
                {

                    Console.WriteLine($"\nYou use the {chosenHealingItem.Name} to restore {chosenHealingItem.HealAmount} health.");
                    CurrentPlayer.Health += chosenHealingItem.HealAmount;
                    if (CurrentPlayer.Health > CurrentPlayer.MaxHealth)
                    {
                        CurrentPlayer.Health = CurrentPlayer.MaxHealth;
                    }
                }
                if (enemy.Health <= 0)
                {
                    Console.WriteLine(enemy.DefeatedDescription);
                    var drop = enemy.DropItem();
                    CurrentPlayer.Inventory.Add(drop);
                    Console.WriteLine($"\n{enemy.Name} drops {drop.Name}. You pick it up.");
                    return;
                }
                else
                {
                    Attack attackChoice = null;
                    Random r = new Random();
                    double attackNum = r.Next(1, 101);
                    for (var i = 0; i < enemy.Attacks.Count; i++)
                    {
                        var attack = enemy.Attacks[i];
                        if (attackNum <= attack.Frequency)
                        {
                            attackChoice = attack;
                            break;
                        }
                    }
                    Console.WriteLine(attackChoice.Description);
                    Random r2 = new Random();
                    int attackVal = r2.Next(1, 21);
                    if (attackVal >= CurrentPlayer.DefenseRating)
                    {
                        var damage = attackChoice.Damage;
                        Console.WriteLine($"\nYou take {damage} damage.");
                        CurrentPlayer.Health -= damage;
                    }
                    else
                    {
                        Console.WriteLine($"\nYou dodge the attack.");
                    }
                    if (CurrentPlayer.Health <= 0)
                    {
                        Console.WriteLine(enemy.VictoriousDescription);
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        GameOver();
                    }
                    if (!Playing || !ApplicationActive)
                    {
                        return;
                    }
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }
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
        public void CraftItemInterface()
        {
            if (CurrentPlayer.Inventory.Count > 1)
            {
                Item component1 = CurrentPlayer.Inventory[0];
                Item component2 = CurrentPlayer.Inventory[1];
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nChoose first component (Type number and press <Enter>):\n");
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
                        Console.WriteLine("\n<Press any other key to continue.>");
                        KeyInfo = Console.ReadKey(true);
                    }
                    else
                    {
                        component1 = CurrentPlayer.Inventory[parsed - 1];
                        CurrentPlayer.Inventory.Remove(component1);
                        break;
                    }
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nChoose second component (Type number and press <Enter>):\n");
                    for (var i = 0; i < CurrentPlayer.Inventory.Count; i++)
                    {
                        var item = CurrentPlayer.Inventory[i];
                        Console.WriteLine($"{i + 1}. {item.Name}");
                        Console.WriteLine($"-----------------------------");
                        Console.WriteLine($"{item.Description}");
                        Console.WriteLine($"-----------------------------\n");
                    }
                    var choice2 = Console.ReadLine();
                    var parsed2 = 0;
                    var valid2 = int.TryParse(choice2, out parsed2);
                    if (!valid2 || parsed2 < 1 || parsed2 > CurrentPlayer.Inventory.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                        Console.WriteLine("\n<Press any other key to continue.>");
                        KeyInfo = Console.ReadKey(true);
                    }
                    else
                    {
                        component2 = CurrentPlayer.Inventory[parsed2 - 1];
                        break;
                    }
                }
                var validCombination = false;
                var validItem = (Item)Activator.CreateInstance(CraftableItems[0]);
                for (var i = 0; i < CraftableItems.Count; i++)
                {
                    var item = (Item)Activator.CreateInstance(CraftableItems[i]);
                    for (var j = 0; j < item.CraftingCombinations.Count; j++)
                    {
                        var combination = item.CraftingCombinations[j];
                        if (combination.Contains(component1.Name) && combination.Contains(component2.Name))
                        {
                            validCombination = true;
                            validItem = item;
                            break;
                        }
                    }
                }
                if (!validCombination)
                {
                    CurrentPlayer.Inventory.Add(component1);
                    Console.WriteLine("The components fail to produce a reaction.");
                }
                else
                {
                    CurrentPlayer.Inventory.Remove(component2);
                    CurrentPlayer.Inventory.Add(validItem);
                    Console.WriteLine($"{component1.Name} and {component2.Name} react successfully, producing {validItem.Name}.");
                }
            }
            else
            {
                Console.WriteLine("You at least two items for crafting.");
            }
        }
        public void CheckNotes()
        {
            while (true)
            {
                if (CurrentPlayer.Notes.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nChoose a note to read (Type number and press <Enter>):\n");
                    for (var i = 0; i < CurrentPlayer.Notes.Count; i++)
                    {
                        var note = CurrentPlayer.Notes[i];
                        Console.WriteLine($"{i + 1}. {note.Name}");
                    }
                    var choice = Console.ReadLine();
                    var parsed = 0;
                    var valid = int.TryParse(choice, out parsed);
                    if (!valid || parsed < 1 || parsed > CurrentPlayer.Notes.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(CurrentPlayer.Notes[parsed - 1].Name);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("\n" + CurrentPlayer.Notes[parsed - 1].Text);
                        Console.WriteLine("\n--------------------------------------");
                        Console.WriteLine("\n| X | Exit Notes Menu");
                        Console.WriteLine("\n<Press any other key to continue.>");
                        KeyInfo = Console.ReadKey(true);
                        if (KeyInfo.Key == ConsoleKey.X)
                        {
                            Console.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You do not have any notes.");
                    Console.WriteLine("\n<Press any other key to continue.>");
                    Console.ReadKey(true);
                    break;
                }
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
                KeyInfo = Console.ReadKey(true);
                if (KeyInfo.Key == ConsoleKey.U)
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
            MapTemplate = @"VR:MS:TR:FD:ML:GL:ER.
                            TR:RW:GV:ER:VL:TS:LL.
                            MW:ER:CG:TR:ER:DA:OB.
                            OL:FC:OR:CC:HL:CL:TR.
                            TR:RL:ER:FR:IG:ER:TM.
                            AS:MF:AL:TR:AP:SA:CS.";
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
