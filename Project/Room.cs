using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{

    public abstract class Room : IRoom
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
        public virtual Note Note { get; set; }
        public virtual List<Item> RespawnItems { get; set; }
        public virtual List<Item> RemoveItems { get; set; } = new List<Item>();
        public virtual Enemy Enemy { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public bool PassagesBuilt { get; set; }
        public virtual bool CraftingArea { get; set; }
        public virtual bool DeathFlag { get; set; }
        public virtual bool VisitedByPlayer { get; set; }
        public List<string> Exits { get; set; } = new List<string>();
        public virtual string Stage { get; set; }
        public virtual Trap Trap { get; set; }
        public ConsoleKeyInfo KeyInfo { get; set; }
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
            PassagesBuilt = false;
            VisitedByPlayer = false;
            Exits = new List<string>();
            CraftingArea = false;
            DeathFlag = false;
            Note = null;
            Enemy = null;
            Trap = null;
            Stage = null;
        }
    }

    public class MainFoyer : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override bool CraftingArea { get; set; }
        public override Note Note { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a page that looks like it's been wripped out of a journal. It's in Dr. Rithbaun's handwriting.");
                game.GetNote();
            }
        }
        public MainFoyer(int y, int x) : base(y, x)
        {
            Name = "Main Foyer";
            Description = $@"
A large room crafted from dark, polished stone. A collection of tapestries 
and paintings adorn the walls, and a large, ornately carved table sits in the 
room's center. A makeshift alchemical workstation takes up most of the table's 
space...probably Dr. Rithbaun's handiwork.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            CraftingArea = true;
            Note = new Note("Page from Dr. Rithbaun's Journal [Main Foyer]", $@"

What a fine mess I'm in! It's been nearly a week days since those three went
absolutely mad and I'm at a complete loss as to how to escape this place. Good
thing I got that letter out before the whole place sealed up. I've set up a work
station at the main entry, as it seems those vile things don't come in here. The
decor must not be to their liking (can't say I blame them...those tapestries are
just garish).

It's just a basic setup, but it should be good enough for alchemical recipes that require
only two components. It seems that the bits left behind by the creatures those three
created are reasonably reactive, thankfully, so I should have plenty of resources as 
long as I can keep killing them.");
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
            if (game.MainQuestStage["purification"] == "catalyst")
            {
                Console.WriteLine($"\nYou notice a strange object sitting at the edge of the room.");
                Items.Add(new EnergeticCatalyst());
                game.MainQuestStage["purification"] = "fuel";
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
            Console.WriteLine($"{item.Name} fails to be of any use.");
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
        public override Note Note { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on one of the operating tables.");
                game.GetNote();
            }
        }
        public AssemblyLab(int y, int x) : base(y, x)
        {
            Name = "Assembly Lab";
            Description = $@"
You're in a room lit by a dim orange glow. Dark metal plates are affixed to the walls, 
and the floorspace is almost entirely taken up by metal operating tables aligned in neat, 
even rows. Several of them are occupied by what look to be human skeletons with strange bit 
of machinery attached to them. A tidy alchemical workstation sits in one corner of the room.";
            Y = y;
            X = x;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new BoneAsh(), new CrimsonOil(), new PulseCrystal() };
            Items = new List<Item>() { new BoneAsh(), new CrimsonOil(), new PulseCrystal() };
            Note = new Note("On Gem Liquefaction: A Series [Assembly Lab]", $@"

<This note seems to be just one in a series...the writing is completely disorganized>

Just look at this place! Aldric's taste keeps getting weirder and weirder.

Anyway, continuing our little trip into the process of liquefying gems, once you've let the
gem-fluid into the matter condenser and brought it out of superposition...or whatever..you'll
want to hit the Red Button to flush the ignition chamber. This is crucial. I've caused so
many explosions by forgetting this...

- Theodore Grimtol");
        }
    }

    public class StorageArea : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to one of the lockers.");
                game.GetNote();
            }
        }
        public StorageArea(int y, int x) : base(y, x)
        {
            Name = "Storage Area";
            Description = $@"
The room is lit only by a faintly glowing lamp on a counter at its center. The walls are completely 
covered with square metal lockers, the majority of which are filled with various mechanical components. 
A few of them contain bones, neatly sorted by type.";
            Y = y;
            X = x;
            RespawnItems = new List<Item>() { new BoneAsh(), new MetalCore() };
            Items = new List<Item>() { new BoneAsh(), new MetalCore() };
            Note = new Note("Alchemical Properties of the Creatures [Storage Area]", $@"

I've started to figure out a few ways to use the remains of the creatures those three unleashed. It 
seems that it's possible to harvest useful alchemical Substrates from quite a few of them. I've found
that combining two substrates will almost invariably produce a highly reactive substance. This substance 
is quite useful...combining it with alchemical Powders seems to produce a nice explosive, for example.

- Dr. Damian Rithbaun");
        }
    }

    public class OvergrownLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note next to the workstation.");
                game.GetNote();
            }
        }
        public OvergrownLab(int y, int x) : base(y, x)
        {
            Name = "Overgrown Lab";
            Description = $@"
You're in a room that looks like it may have once been a lab, but is now almost entirely covered by 
a strange black fungus or moss. Thorned vines jut from cracks in the dark stone walls at odd intervals, 
and nearly all of the tables in the room have been overtaken by the growth. Only a small alchemical 
workstation in one corner of the room appears to be undisturbed.";
            Y = y;
            X = x;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new AcridPowder(), new PutridNodule(), new PulseCrystal() };
            Items = new List<Item>() { new AcridPowder(), new PutridNodule(), new PulseCrystal() };
            Note = new Note("Page from Dr. Rithbaun's Journal [Overgrown Lab]", $@"

I'm really glad I brought along these Pulse Crystals. Turns out that you can combine them with a sufficiently 
reactive alchemical solid to create a device that makes short work of all the traps those three rigged up.
Creating the base alchemical solid isn't even that hard...just have to combine two alchemical Substrates of
different type. At least, that seems to usually work...");
        }
    }

    public class GerminationVats : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note next to one of the glass containers.");
                game.GetNote();
            }
        }
        public GerminationVats(int y, int x) : base(y, x)
        {
            Name = "Germination Vats";
            Description = $@"
Aside from a metal walkway around its edges, the floor of this room appears to have been rather 
haphazardly dug out. The gaping pit at its center is filled with rows of massive glass containers 
filled with a bright green, cloudy liquid. A few of them seem to have large, dark shapes suspended 
at their center. They look vaguely organic, but beyond that you can't quite identify them.";
            Y = y;
            X = x;
            RespawnItems = new List<Item>() { new AcridPowder(), new YellowIchor() };
            Items = new List<Item>() { new AcridPowder(), new YellowIchor() };
            Note = new Note("On Gem Liquefaction: A Series [Germination Vats]", $@"

<Oddly, the note appears to be written on the back of a child's drawing in an almost illegile scribble>

So *this* is where Miranda grows those scary little guys. Yeesh.

Anyway, back to business. I'm leaving this as a record of this nice little process I devised for
low-temperature fusion of silicates. Essentially, liquefying gems. For...various uses, probably. And
I'm going to leave the first step here in this random room because...well...reasons. You know how 
this works.

Anyway, first thing you want to do after putting the Gem (I'm just gonna capitalize it, because why 
not?) in the fusion chamber is hit the ignition switch...sorry, the Ignition Switch. This will activate
the matter destabilizer, triggering...you know what, the details don't matter. Just hit the Ignition 
Switch.

...I just realized I'm writing this on the back on one of Vivian's drawings. Oof. Miranda's not gonna
be happy...

- Theodore Grimtol");
        }
    }

    public class MakeshiftLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override bool CraftingArea { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note sitting in a realtively clean area of the room.");
                game.GetNote();
            }
        }
        public MakeshiftLab(int y, int x) : base(y, x)
        {
            Name = "Makeshift Lab";
            Description = $@"
The room appears to be some kind of kitchen and dining area that has been somewhat hapazardly repurposed 
as a lab. Pages and pages of notes cover almost every available surface, and alchemical tools of all 
sorts seem to be scattered seemingly at random around the room.";
            Y = y;
            X = x;
            CraftingArea = true;
            RespawnItems = new List<Item>() { new LuminousDust(), new QuiveringOoze(), new PulseCrystal() };
            Items = new List<Item>() { new LuminousDust(), new QuiveringOoze(), new PulseCrystal() };
            Note = new Note("On Gem Liquefaction: A Series [Makeshift Lab]", $@"

<This note seems to be just one in a series...the writing is completely disorganized>

Okay, we're onto the penultimate and possibly most important step in the gem liquefaction process...
Once you've purged the ignition chamber (hopefully preventing unwanted explosions), you'll want to
open the Small Valve and let the now-stable gem fluid flow into the filtering chamber. Piece of cake.

- Theodore Grimtol");
        }
    }

    public class HaphazardLibrary : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note sitting on the floor next to one of the shelves.");
                game.GetNote();
            }
        }
        public HaphazardLibrary(int y, int x) : base(y, x)
        {
            Name = "Hapahazard Library";
            Description = $@"
The room looks to have been a pantry or cellar of some sort. The wooden shelves that line it's walls, 
however, are now filled entirely with books. They all look to be fairly advanced alchemical texts, 
along with a few tomes on cross-planar travel and even more esoteric topics.";
            Y = y;
            X = x;
            RespawnItems = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
            Items = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
            Note = new Note("On Gem Liquefaction: A Series [Haphazard Library]", $@"

<This note seems to be just one in a series...the writing is completely disorganized>

Pardon the mess, dear reader. This is just how I work.

But where was I? Ah, right, low-temperature gem liquefaction! Okay, so after the ignition's complete
you'll want to open the Large Valve so the destabilized gem...stuff can flow into the matter condenser.
This just takes it out of superposition. Is that right? That doesn't sound right. Whatever. Just open the
valve.

- Theodore Grimtol
");
        }
    }

    public class RefiningLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Fuel Orb")
            {
                Console.WriteLine($"Even with a fuel source, this kiln is completely wrecked. This is probably better used elsewhere.");
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to the kiln.");
                game.GetNote();
                game.MainQuestStage["purification"] = "catalyst";
            }
        }
        public RefiningLab(int y, int x) : base(y, x)
        {
            Name = "Refining Lab";
            Description = $@"
Sturdy wooden counters lined with various alchemical and blackmithing tools are arranged along 
the room's walls. At it's center lies a large kiln. It looks to have been recently used...a 
powdery residue lines its interior. The last user of the device apparently didn't want anyone 
else to have access to it, however, as the control panel next to it has been smashed to pieces 
with something heavy. It's likely beyond repair.";
            Y = y;
            X = x;
            Items = new List<Item>() { new AlchemicalResidue() };
            Note = new Note("Missing Catalyst [Refining Lab]", $@"
The catalyst has gone missing again. I assume that one of those inferior beasts my siblings 
created wandered off with it. I'm going to have to find something else to power the furnace.

This is infuriating! If I happen across Miranda or Theodore I'm going to strangle them with 
my bare hands!

- Aldric Grimtol");
        }
    }

    public class FurnaceRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string Stage { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override List<Item> RemoveItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Fuel Orb")
            {
                Console.WriteLine($"You place the Fuel Orb into the indentation on the furnace console. The machine roars to life, giving off an intense heat.");
                RemoveItems.Add(item);
                Stage = "furnace on";
                Description = $@"
The entire room is lined with thick metal plating affixed to that walls with rivets. The 
furnace at the center of the room gives off an intense heat as the Fuel Orb in the console 
next to it pulses with orange light.";
            }
            else
            {
                if (item.Name == "Wooden Effigy")
                {
                    if (Stage == "furnace on")
                    {
                        Console.WriteLine($"Using a nearby pair of tongs, you place the Wooden Effigy into the furnace. It quickly burns, revealing the Steel Key that had been sealed inside.");
                        RemoveItems.Add(item);
                        Items.Add(new SteelKey());
                    }
                    else
                    {
                        Console.WriteLine($"It's certainly flammable, but with the furnace powered off there's not much you can do with it right now.");
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} fails to be of any use.");
                }
            }
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public FurnaceRoom(int y, int x) : base(y, x)
        {
            Name = "Furnace Room";
            Description = $@"
The entire room is lined with thick metal plating affixed to that walls with rivets. It's 
empty save for a large metal cylinder at its center. On closer inspection, the object appears 
to be an alchemical furnace of some sort, it's hatch ajar. The interior is room temperature...
it's clearly not working. The console next to it has a hemispherical indentation.";
            Y = y;
            X = x;
            Items = new List<Item>();
            RemoveItems = new List<Item>();
            Stage = null;
        }
    }

    public class AbandonedParlor : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string Stage { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override List<Item> RemoveItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Steel Key")
            {
                Console.WriteLine($@"
The Steel Key fits perfectly into the safe's keyhole. You turn the key and swing open the door 
to reveal...a concrete wall. Somebody clearly didn't want anyone to get into this safe. On closer 
inspection, the wall is cracked, and there seems to be a space behind it. It'll still take a bit 
of force to break through, but you're an alchemist after all...");
                RemoveItems.Add(item);
                Stage = "safe open";
                Description = $@"
The room's walls are decorated with a wallpaper bearing a repeating geometric pattern, and the 
floor is covered with an expensive-looking rug. Two couches sit before an elaborate stone 
fireplace at one edge of the room, both covered in a thin layer of dust. The wall safe at 
the corner of the room lies open, but the concrete wall inside presents an annoying obstacle.";
            }
            else
            {
                if (item.Name.Split(" ")[1] == "Grenade")
                {
                    if (Stage == "safe open")
                    {
                        Console.WriteLine($@"
You lob an {item.Name} at the concrete wall. The explosion easily destroys it's target, revealing 
a small space at the very back of the safe. Sitting amidst the rubble of the wall is a 
strange, pointed object.");
                        RemoveItems.Add(item);
                        Items.Add(new SunCrest());
                        Description = $@"
The room's walls are decorated with a wallpaper bearing a repeating geometric pattern, and the 
floor is covered with an expensive-looking rug. Two couches sit before an elaborate stone 
fireplace at one edge of the room, both covered in a thin layer of dust. The wall safe at 
the corner of the room lies open, and the concrete wall inside has been reduced to rubble.";
                    }
                    else
                    {
                        Console.WriteLine($"It's certainly flammable, but with the furnace powered off there's not much you can do with it right now.");
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} fails to be of any use.");
                }
            }
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public AbandonedParlor(int y, int x) : base(y, x)
        {
            Name = "Abandoned Parlor";
            Description = $@"
The room's walls are decorated with a wallpaper bearing a repeating geometric pattern, and 
the floor is covered with an expensive-looking rug. Two couches sit before an elaborate stone 
fireplace at one edge of the room, both covered in a thin layer of dust. In one corner of the 
room the wallpaper has been peeled aside, revealing a large safe set into the wall. A keyhole 
sits conspicuously above its handle.";
            Y = y;
            X = x;
            Items = new List<Item>(){
                new WoodenEffigy()
            };
            RemoveItems = new List<Item>();
            Stage = null;
        }
    }

    public class ConvertedSunroom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override List<Item> RemoveItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Sun Crest")
            {
                Console.WriteLine($@"
You place the Sun Crest in the indentation on the stone platform. The gems on its surface begin 
to glow as they catch the ray of sun peeking through the skylight. You hear a click, and suddenly 
all eight of the braziers in the room light up with a burst of pale orange flame. A compartment 
in the platform has opened to reveal some kind of metal lid.

You notice that glowing orange letters have appeared on the platfrom beneath the crest. They spell 
out a cryptic message: 

'Only with darkness shall the path to light be known.'

");
                Stage = "braziers lit";
                RemoveItems.Add(item);
                Items.Add(new BrazierLid());
                Description = $@"
The outside-facing walls of the room feature large windows that have since been neatly boarded up. 
The skylight in the ceiling has been given a similar treatment. The eight iron braziers along the 
room's edges are now lit, giving off a pale orange glow. The platform at the room's center now bears 
a cryptic message: 

'Only in deepest darkness shall the path to light be known.'";
            }
            else if (item.Name == "Incendiary Pistol")
            {
                Console.WriteLine($@"
Thinking incendiary bullets might do the trick, you fire a shot from your pistol at the nearest 
brazier. It ignites briefly, but the flame quickly dies. This method probably won't be of much use.");
            }
            else if (item.Name == "Incendiary Grenade")
            {
                Console.WriteLine($@"
That would probably do quite a bit more than just lighting a few braziers. There has to be a better 
way...");
            }
            else if (item.Name == "Brazier Lid")
            {
                List<string> litTorches = new List<string>() {
                    "n", "ne", "e", "se", "s", "sw", "w", "nw"
                };
                List<string> correctOrder = new List<string>() {
                    "ne", "s", "se", "w", "sw", "nw", "n", "e"
                };
                List<string> chosenOrder = new List<string>() { };
                Console.WriteLine("The lid seems like it could be useful for putting out the braziers. Worth a shot.");
                Console.WriteLine("\n<Press any key to continue.>");
                Console.ReadKey(true);
                while (litTorches.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nThe following braziers are still lit:\n");
                    Console.WriteLine("\nChoose a brazier to extinguish (Type number and press <Enter>):\n");
                    string torch = null;
                    string torchLocation = null;
                    for (var i = 0; i < litTorches.Count; i++)
                    {
                        torch = litTorches[i];
                        torchLocation = torch.Length == 1 ? "Wall" : "Corner";
                        Console.WriteLine($"{i + 1}. {torch.ToUpper()} {torchLocation}");
                    }
                    var choice = Console.ReadLine();
                    var parsed = 0;
                    var valid = int.TryParse(choice, out parsed);
                    if (!valid || parsed < 1 || parsed > litTorches.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                    else
                    {
                        torch = litTorches[parsed - 1];
                        torchLocation = torch.Length == 1 ? "Wall" : "Corner";
                        Console.WriteLine($"You use the Brazier Lid to extinguish the brazier at the room's {torch.ToUpper()} {torchLocation}");
                        litTorches.Remove(torch);
                        chosenOrder.Add(torch);
                    }
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }
                bool isCorrectOrder = true;
                for (var i = 0; i < chosenOrder.Count; i++)
                {
                    var chosen = chosenOrder[i];
                    var correct = correctOrder[i];
                    if (chosen != correct)
                    {
                        isCorrectOrder = false;
                        break;
                    }
                }
                if (isCorrectOrder)
                {
                    Console.Clear();
                    Console.WriteLine("You hear a mechanical click. Another compartment in the central platform appears to have opened, revealing a strange cylindrical object.");
                    RemoveItems.Add(item);
                    Items.Add(new ArcaneFuse());
                    Description = $@"
The outside-facing walls of the room feature large windows that have since been neatly boarded up. 
The skylight in the ceiling has been given a similar treatment. The eight iron braziers along the 
room's edges remain extinguished.";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("After a few seconds, the braziers suddenly light up once again. Looks like that wasn't quite the correct solution.");
                }
            }

            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }

        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to the platform.");
                game.GetNote();
            }
        }
        public ConvertedSunroom(int y, int x) : base(y, x)
        {
            Name = "Converted Sunroom";
            Description = $@"
The outside-facing walls of the room feature large windows that have since been neatly boarded up. 
The skylight in the ceiling has been given a similar treatment. The room itelf is completely empty 
save for two features. Eight iron braziers are arranged along the room's edges, one in each corner 
and one at the midpoint of each wall. A raised stone platform sit at the room's center. The platform 
has a strange, eight-pointed indentation at its center. Around this indentation, the phrase 
'post tenebras lux' has been carved in large, Gothic letters.";
            Y = y;
            X = x;
            Items = new List<Item>();
            RemoveItems = new List<Item>();
            Note = new Note("Discarded Manifesto [Converted Sunroom]", $@"
I can't stand looking at the sun anymore. What need do I have for it, when I have the pure, beautiful 
light of alchemical flame! And that hideous crest that once had a place of honor in this room...it 
shall remain locked away, just like the light it celebrates. Indeed, the sun is a MOCKERY of my glorious 
creations! 

But Rithbaun. Rithbaun has other ideas. I know he's hunting me. He's jealous of my work! He tried to tell 
me I was going too far, but I've SEEN what the glorious light of purity can do. If he gets to me, the 
world will need a record of my journey. Of my VISION.

I've known it since I was young. There's something wrong with the world. Greed, war, destruction...the 
genesis of it all lies within us. Mankind is a disease, and as a young man I set out to find a cure. 

I left Grimtol heading northeast, until after many weeks of travel I found myself in the wastes of Siberia. 
I sought to conquer my own mental weakness through exposure to the elements, but it left me feeling empty. 
I continued my search, heading south into China. I spoke to many of the scholars there, but none could help 
me in my search. Losing hope, I headed southeast through Laos and Vietnam,eventually making my way to the 
Phillippines. It was there that rumors of a revered sage brought me west to India. 

I studied under many excellent teachers there, but never found the person or the answer I sought. Falling 
deeper into despair, I turned my sights to the southwest, crossing the Atlantic until I reached South America. 
From there, I wandered northwest through Central America. I was barely lucid, but I felt something powerful 
driving me onward. I soon reached the United States, and heard a beautiful voice calling me to the north. 
And it was in the middle of the Canadian wilderness that I saw it. A beautiful vison of the world, cleansed 
by fire, and humanity elevated to its ideal form. No war, no disease, no suffering...the path lay before 
me with astonishing clarity. When I finally sailed east, returning to Grimtol, I knew that I had finally found 
it.

The CURE. The cure for humanity.

- Aldric Grimtol

<After this are several pages of mostly incomprehensible ranting>");
        }
    }

    public class ImmaculateGallery : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Infernal Elixir")
            {
                Console.WriteLine($"You drink the Infernal Elixir. Suddenly, you realize that you can now longer feel the ambient temperature.");
                RemoveItems.Add(item);
                Stage = "elixir drunk";
            }
            else
            {
                if (item.Name == "Arcane Fuse")
                {
                    if (Stage == "elixir drunk")
                    {
                        Console.WriteLine($@"
You insert the Arcane Fuse into the indentation at the statue's base. Suddenly, fires spews forth from the statue's 
mouth, blanketing you with infernal heat. Except...you can't feel it. Your body is completely unaffected.

Jut as you start to feel the elixr wear off, the flames subside. You notice that the golden egg nestled within the 
statue's tail has been melted, revealing a strange, luminous object at its core.");
                        RemoveItems.Add(item);
                        Items.Add(new AnchorOfPurification());
                    }
                    else
                    {
                        DeathFlag = true;
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} fails to be of any use.");
                }
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to the statue.");
                game.GetNote();
            }
            if (DeathFlag)
            {
                Console.WriteLine($@"
You insert the Arcane Fuse into the indentation at the statue's base. Suddenly, fires spews forth from the statue's 
mouth, blanketing you with infernal heat. With nothing to protect you from the fiery onslaught, you are immediately
incinerated.");
            }
        }
        public ImmaculateGallery(int y, int x) : base(y, x)
        {
            Name = "Immaculate Gallery";
            Description = $@"
The room is crafted almost entirely from white stone, and lit by lanterns tucked into wall sconces at regular 
intervals. The walls are lined with paintings, most of them rendered in a stunningly realistic style. Statues 
of human figures reminiscent of the work of the great Classical sculptors are arranged neatly around the room's 
edges. At the center of the room is a massive jade statue of an Eastern dragon. It's eyes seem to stare directly 
at you. The statues's tail is wrapped around an egg seemingly carved from solid gold. At the base of the statue 
is an elaborate arrangement of thick copper wires, all of them connected to a cylindrical indentation that currently 
lies empty.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("A Wondrous Elixir [Immaculate Gallery]", $@"

Every day I come closer to discovering the purest form of alchemical fire...the Fire that will cure humanity.
In pursuit of this, I've devised an elixir to test the raw potential of an alchemical flame.

I discovered that by combining some of that disgusting Ichor dropped by Miranda's awful creations
with Ash derived from human bones produces a pitch-black Marrow with powerful alchemical properties.

Also, by combining some of the sulfurous Powder those foul beasts drop with some of the Oil I've developed for 
use in my soldiers, I was able to produce a wonderfully pure Extract.

Combining this Marrow and Extract should produce an Elixir that imbues a living creature with incredible, though 
short-lived, resistance to heat. This will be the tool I use find the true Alchemical Fire. If it can overcome the
protection of the Elixir, then it may be powerful enough to elevate humanity to its highest form...

- Aldric Grimtol");
        }
    }

    public class AldricsStudy : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a page that looks like it's been wripped out of a journal. It's in Dr. Rithbaun's handwriting.");
                game.GetNote();
            }
        }
        public AldricsStudy(int y, int x) : base(y, x)
        {
            Name = "Aldric's Study";
            Description = $@"
A wood-panelled room decorated with tapestries and sculptures, all placed with seemingly painstaking precision. 
A small desk lies in one corner, the papers on its surface neatly organized. Several bookshelves line the walls, 
and a quick glance suggests the books have been arranged alphabetically. The only thing out of place in the room 
is the glowing red disc hovering in midair at the room's center. Strange runes flicker across it's surface.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Dr. Rithbaun's Journal [Aldric's Study]", $@"

This is worse than I thought. If that portal in his study is any indication, Aldric has found a way to travel 
between planes. No telling what sorts of power he could gain.

He was a promising enough student. But it would appear that alchemy was only a means to an end for him. He was 
obsessed with honing his body and mind, and saw the discipline of alchemical Purification as the path to perfection. 
It would've been fine if he'd stopped there, but once he started ranting about elevating humanity itself to a more 
ideal form, I knew there'd be trouble. I don't quite know why, but that man despised human nature. I'm not sure 
how those infernal things he created play into his crazed vision, but I'd rather not wait around to find out.

If I can reopen that portal, I might be able to pull him back to this plane. For a portal like that, he'd have to 
have hidden an Anchor somewhere...if I can find it, opening this portal will be a trivial matter.");
        }
    }

    public class FetidCourtyard : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override List<Item> RemoveItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name.Split(" ")[0] == "Incendiary")
            {
                Console.WriteLine($@"You use the {item.Name} to set the strange murk aflame. It gradually burns away, revealing a strange-looking skull.");
                if (item.Name.Split(" ")[1] == "Grenade")
                {
                    RemoveItems.Add(item);
                }
                Items.Add(new MisshapenSkull());
                Description = $@"
You're in an open courtyard that looks to have been abandoned long ago. The remnants of cobblestone pathways are barely 
visible beneath overgrown shrubs. The pool at the courtyard's center now lies empty.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the ground next to the pool.");
                game.GetNote();
            }
        }
        public FetidCourtyard(int y, int x) : base(y, x)
        {
            Name = "Fetid Courtyard";
            Description = $@"
You're in an open courtyard that looks to have been abandoned long ago. The remnants of cobblestone pathways are barely 
visible beneath overgrown shrubs. The pool at the courtyard's center is clogged with thick slime that appears to be slowly 
eating away at the marble tiles around it. You can barely make out an object beneath the corrosive muck.";
            Y = y;
            X = x;
            Items = new List<Item>();
            RemoveItems = new List<Item>();
            Note = new Note("the pool [Fetid Courtyard]", $@"

She would always play here. She loved the garden...the pool...

This place is just a symbol now. A symbol of youth eaten away. That's why I transformed it.

Aldric keeps trying to burn this courtyard down. He says I've turned it into something disgusting.

He can burn all he wants. I'll simply create my art anew. Perhaps one of these days he'll become my next piece.

It's probably the only potential he has.

Vivi...I dedicate this to you.

- Miranda Grimtol");
        }
    }

    public class MacabreWorkroom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Misshapen Skull")
            {
                Console.WriteLine($@"
You place the Misshapen Skull onto the neck of the skeletal figure. You hear a click, and several of the wires 
holding the bones snap, causing the grim sculpture to lurch toward you. Its left arm swings in a wild arc, 
the hand stopping just before your face. The fingers of the hand snap open, something. You hear a metallic 
clang as the object hits the floor.

You notice for the first time that nestled within the sculpture's ribcage is a small iron box, exaclty where 
it's heart would be. The lid is open, as if daring you to place something inside.");
                RemoveItems.Add(item);
                Items.Add(new SurgicalKnife());
                Stage = "skull placed";
                Description = $@"
The room is dimly lit by a faint green glow. The walls of the room are covered with sketches, all depicting 
horrific figures. The grim sculpture in the center leans forward, almost menacingly. It it's ribcage where
is a strange iron box, the lid open and beckoning.";
            }
            else
            {
                if (item.Name == "Malformed Heart")
                {
                    if (Stage == "skull placed")
                    {
                        Console.WriteLine($@"
You place the Malformed Heart inside the box in the skeletal scuplture's rincage. It immediately slams shut. 
Suddenly, the sculpture's skull bursts apart, scattering fragments of bone in every direction. You hear something
heavy fall to the floor.");
                        RemoveItems.Add(item);
                        Items.Add(new ThornGear());
                    }
                    else
                    {
                        Console.WriteLine($"It seems to fit with the room's grisly theme, but there's no obvious place for it.");
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} fails to be of any use.");
                }
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the ground next to the pool.");
                game.GetNote();
            }
        }
        public MacabreWorkroom(int y, int x) : base(y, x)
        {
            Name = "Macabre Workroom";
            Description = $@"
The room is dimly lit by a faint green glow. As your eyes adjust to the light, you see that the walls of the 
room are covered with sketches, all depicting horrific figures. Some appear to be horrifically deformed skeletons, 
while others resemble carnivorous, plantlike creatures. Hanging in the center of the room is a collection 
of bones wired together into a terrifiying skeletal form. The grim sculpture appears to be missing a head.
It's bony hands are clenched tightly.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("suffering [Macabre Workroom]", $@"

Aldric rants on and on about the flaws of humanity. About how his grand vision would remake us 
into something greater.

He's a fool. Suffering is the essence of humanity. And I've grown to appreciate it's beauty.

He simply can't stomach the truth that my art conveys. It would ruin him to admit his 
blindness.

Suffering is all we have. I can finally see...

But...Vivi...why did I have to learn it like this...

- Miranda Grimtol");
        }
    }

    public class ClockworkGarden : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Thorn Gear")
            {
                Console.WriteLine($@"
You place the Thorn Gear into its place on the mechanical sculpture. Suddenly, all of the gears on the
device begin spinning rapidly, and the contraption's 'limbs' begin sweeping around the room. And then, you
see it. Each limb is tipped with a massive blade. They seem to be moving in a prescribed pattern, but you 
can't quite make it out. A blade sails through the air just over your head...you're going to have to
move quickly to escape.");

                List<string> correctOrder = new List<string>() {
                    "a", "w", "s", "a", "d", "w"
                };
                int index = 0;
                Console.WriteLine("\n<Press any key to continue.>");
                Console.ReadKey(true);
                while (index < correctOrder.Count)
                {
                    string currentMove = null;
                    Console.Clear();
                    Console.WriteLine("\nThe blades of the contraption slice through the air around you.\n");
                    Console.WriteLine("\nChoose an action:\n");
                    Console.WriteLine("| A | Dodge Left");
                    Console.WriteLine("| D | Dodge Right");
                    Console.WriteLine("| W | Jump");
                    Console.WriteLine("| S | Duck");
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.A || KeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You quickly roll to the left.");
                        currentMove = "a";
                    }
                    else if (KeyInfo.Key == ConsoleKey.W || KeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You quickly leap into the air");
                        currentMove = "w";
                    }
                    else if (KeyInfo.Key == ConsoleKey.D || KeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You quickly roll to the right.");
                        currentMove = "d";
                    }
                    else if (KeyInfo.Key == ConsoleKey.S || KeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You quickly crouch to the ground.");
                        currentMove = "s";
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Rather than taking an evasive action, you for some reason choose to stand still.");
                    }
                    if (currentMove == correctOrder[index])
                    {
                        Console.WriteLine("\nYou just manage to avoid a blade that was sweeping directly toward you.");
                        index++;
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        DeathFlag = true;
                        break;
                    }
                }
                if (!DeathFlag)
                {
                    Console.Clear();
                    Console.WriteLine($@"
Suddenly, the contraption grinds to a halt. Out of the corner of your eye, you see a small object roll along the floor, 
seemingly from out of nowhere. It comes to a stop near the center of the room.");
                    RemoveItems.Add(item);
                    Items.Add(new GlassEye());
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the ground next to the contraption.");
                game.GetNote();
            }
            if (DeathFlag)
            {
                Console.WriteLine($@"
Unfortunately, your evasive action takes you right into the path of one of the contraption's mechanical limbs. A
gleaming blade sweeps directly toward you...");
            }
        }
        public ClockworkGarden(int y, int x) : base(y, x)
        {
            Name = "Clockwork Garden";
            Description = $@"
The room is dominated by a massive structure composed of a multitude of gears and mechanical limbs that protude 
from the walls, ceiling and floor to form a veritable maze of strange clockwork parts. On closer inspection, 
the contraption appears to have been designed with a plant motif -- the gears resemble thorned flowers, while the 
mechanical limbs mimic vines and branches. The centerpoint of the device appears to be a 'trunk' at the center of 
the room. It's covered with gears of varying sizes. You notice that only one of the gears is spinning...the gear 
that would go next to it appears to be missing.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Child's Drawing [Clockwork Garden]", $@"

<A drawing clearly done by a young child. It seems to depict a stick figure at play. 
The clumsily scribbled signature at the bottom reads 'Vivian'.>

        ________________________________________________________________________________
       |             |             |             |            |            |            |
       |             |   \     /   |             |            |            |  \     /   | 
       |             |    \ o /    |             |            |            |   \ o /    |
       |    o ___    |      |      |             |   o ___    |   ___ o    |     |      |
       |  \/ \       |     / \     |      o      |  \/ \      |      / \/  |    / \     |
       |     / \__   |    /   \    |   __/|\__   |     / \__  |   __/ \    |   /   \    |
       |_____\_______|_____________|__ _--^--_ __|_____\______|_______/____|____________|            

");
        }
    }

    public class RansackedWorkroom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Glass Eye")
            {
                Console.WriteLine($@"
You insert the Glass Eye into the hole in the portrait. Suddenly, you hear a mechanical click, and the painting 
slides to the left to reveal a lockbox set into the wall. Four dials are set into the front of the box, each
bearing the roman numerals for the numbers one through ten...a combination lock.");
                List<string> correctCombination = new List<string>() {
                    "v", "i", "v", "i"
                };
                List<string> chosenCombination = new List<string>() { };
                Console.WriteLine("\n<Press any key to continue.>");
                Console.ReadKey(true);
                var dialPositions = new Dictionary<int, string>(){
                        {1, "first"}, {2, "second"},
                        {3, "third"}, {4, "final"}
                };
                var numerals = new List<string>(){
                    "i", "ii", "iii", "iv", "v", "vi", "vii", "viii", "ix", "x"
                };
                while (chosenCombination.Count < correctCombination.Count)
                {
                    var dialNumber = chosenCombination.Count + 1;

                    Console.Clear();
                    Console.WriteLine($"\nYou're currently on the {dialPositions[dialNumber]} lock.");
                    Console.WriteLine($"\nCombination so far:\n");
                    var combinationString = "";
                    foreach (var numeral in chosenCombination)
                    {
                        combinationString += $" {numeral.ToUpper()} ";
                    }
                    Console.WriteLine($"{combinationString}");
                    Console.WriteLine($"\nChoose a numeral to set the {dialPositions[dialNumber]} dial to (Type number and press <Enter>):\n");
                    Console.WriteLine($@"
1.   I     6.    VI
2.  II     7.   VII
3. III     8.  VIII
4.  IV     9.    IX
5.   V     10.    X");
                    var choice = Console.ReadLine();
                    var parsed = 0;
                    var validNum = int.TryParse(choice, out parsed);
                    var validStr = false;
                    if (!validNum)
                    {
                        validStr = numerals.Contains(choice.ToLower());
                    }
                    if (validNum)
                    {
                        chosenCombination.Add(numerals[parsed - 1]);
                    }
                    else if (validStr)
                    {
                        chosenCombination.Add(choice.ToLower());
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                    Console.WriteLine("\n<Press any key to continue.>");
                    Console.ReadKey(true);
                }
                bool isCorrectCombination = true;
                for (var i = 0; i < chosenCombination.Count; i++)
                {
                    var chosen = chosenCombination[i];
                    var correct = correctCombination[i];
                    if (chosen != correct)
                    {
                        isCorrectCombination = false;
                        break;
                    }
                }
                if (isCorrectCombination)
                {
                    Console.Clear();
                    Console.WriteLine($@"
You pull on the safe door. It swings open easily. Inside is a vial of liquid.");
                    RemoveItems.Add(item);
                    Items.Add(new LethalVenom());
                    Description = $@"
The room looks to have been used for artistic work at some point, but now lies in complete disarray. Discarded 
sketches cover the floor alongside spilled containers of ink and paint. An overturned easel lies in one corner 
next to a wall of empty shelves. A large painting of Vivian Grimtol stares at you with a single glass eye.";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($@"
You pull on the safe door to test the combination, but it doesn't budge. After a few seconds, the 
Glass Eye pops out of the portrait. You catch it and place it back in your pocket. The painting
slides back into place, covering the lockbox.");
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note beneath the portrait.");
                game.GetNote();
            }
        }
        public RansackedWorkroom(int y, int x) : base(y, x)
        {
            Name = "Ransacked Workroom";
            Description = $@"
The room looks to have been used for artistic work at some point, but now lies in complete disarray. Discarded 
sketches cover the floor alongside spilled containers of ink and paint. An overturned easel lies in one corner 
next to a wall of empty shelves. Only one item appears undisturbed: A large painting of a young girl hanging on 
one of the walls. Beneath the painting is a small brass plate with the name 'Vivian Grimtol' inscribed on it. On 
closer inspection, there appears to be a hole gouged in the portrait where the subject's left eye should be.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("they took her eye [Ransacked Workroom]", $@"

Who would do this to her?

Why would they take poor Vivi's eye?

Oh, Vivi. I don't know why it's all so cruel.

I'll make you a new eye. I promise.");
        }
    }

    public class ViviansRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Vivian's Charm")
            {
                Console.WriteLine($@"
You hold up the Charm. Suddenly a confused, inhuman wail echoes throughout the room. 
The strange runes slowly fade from the walls. The horrific cries slowly die down, and
the voice utters a final word.

Voice: 'Vivi...'

The Charm shatters into fragments. The source of the voice appears to have retreated.

The room now appears to be filled with a faint glow. A feeling of calm washes over you.
Then, you hear the quiet voice of a young girl.

Girl: 'You have to drink it now. It's the only way. I'll protect you...I promise.'");
                RemoveItems.Add(item);
                Stage = "charm used";
                Description = $@"
You're in what looks to be a child's room. A soft, calming white light blankets the area.";
            }
            else
            {
                if (item.Name == "Lethal Venom")
                {
                    if (Stage == "charm used")
                    {
                        Console.WriteLine($@"
You hesitantly remove the stopper from the vial and swallow the Lethal Venom. Your vision immediately
goes black. When you wake up, you see that you're still in the same room, but it's...changed.
Everything is colored in shades of gray, and faded, as if shrouded in a thin fog.

You look around, then stop suddenly when your eyes reach a strange shape on the ground. It's your body.
It's at this point you realize that you can't feel your heartbeat.

You sense a prescence nearby, and turn around to see the little girl from before. She looks at you with
sad eyes.

Girl: 'It's okay. You're safe. You're still...there, but you're here too. You're in the between place. 
That's where mommy hid the dark thing.'

As if to emphasize, she points at the area behind you. As you turn around, you see it. A small sphere
of pulsing green light and swirling shadows, hovering in midair.

Girl: 'I tried to get it, but I can't. I'm too far away.'

You reach your hand out toward the orb. The room suddenly fills with green light, and your vision 
once again fades to black. As you fade away, you hear the girl's voice...

Girl: 'Please help mommy. She didn't mean to hurt anyone...'
");

                        RemoveItems.Add(item);
                        Items.Add(new AnchorOfCorruption());
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                        Console.Clear();

                        Console.WriteLine($@"
You wake up on the floor of the room. Impulsively, you put a hand to your chest. Thankfully, your heart 
appears to be beating.

A familiar orb of green light and shadow sits on the floor at the center of the room.
");

                    }
                    else
                    {
                        DeathFlag = true;
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} fails to be of any use.");
                }
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to the bed.");
                game.GetNote();
            }
            if (DeathFlag)
            {
                Console.WriteLine($@"
You rip the stopper off of the vial and swallow the Lethal Venom. Your body is immediately wracked by pain, and
your vision grows hazy. As you fall to the ground, you can hear a sickening laughter ringing in your ears.");
            }
            if (game.MainQuestStage["corruption"] == "start")
            {
                var hasPoison = false;
                for (var i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    if (item.Name == "Lethal Venom")
                    {
                        hasPoison = true;
                        break;
                    }
                }
                if (hasPoison)
                {
                    game.MainQuestStage["corruption"] = "has poison";
                    Console.WriteLine($@"
You feel the room grow suddenly colder. Strange patterns of bright green light and shadow begin crawling
across the walls. It almost looks like writing, but it's unlike any language you've seen.");
                    Description = $@"
You're in what looks to be a child's room. The floral wallpaper is now riddled with a strange patchwork of shadows 
and glowing green runes. You hear a faint whispering that seems to emanate from everywhere at once.

Something about this room makes you feel incredibly tired.";
                }
            }
            if (game.MainQuestStage["corruption"] == "has poison" && !DeathFlag)
            {
                Console.WriteLine($@"
You suddenly hear a voice that manages to be both rasping and gurgling at the same time.

Voice: 'Drriink iiit...'

Before you realize it, you're holding the vial of Lethal Venom. You unconsciouly begin reaching for the stopper, 
then quickly jerk your hand away. A sound resembling pained laughter echoes around you. You quickly tuck the vial 
back in your pocket, but it's almost as if it'scalling out to you.

You suddenly feel incredibly tired. The fatigue isn't helping your mental state. It might be best to leave this 
place and get some rest.");
            }
        }
        public ViviansRoom(int y, int x) : base(y, x)
        {
            Name = "Vivian's Room";
            Description = $@"
You're in what looks to be a child's room. The wallpaper features a pink, floral design, and the ceiling has been 
painted to resemble a blue sky with a few puffy clouds. The bed looks unmade, yet the thin layer of dust coating 
every surface in the room suggest it hasn't been disturbed in awhile. A few dolls lie scattered on the floor.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("death and rebirth [Vivian's Room]", $@"

Sometimes, Vivi, I can still hear you. I can't bear the thought of never seeing you again.

If I die, will we be reunited? I wondered for so long...I made something that would let me try...
hid it away in my workroom, in the secret place.

But I found a better way. A way to conquer the natural laws that shackle this plane...

A way to bring you back.

- Miranda Grimtol");
        }
    }

    public class MirandasStudio : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a page that looks like it's been wripped out of a journal. It's in Dr. Rithbaun's handwriting.");
                game.GetNote();
            }
        }
        public MirandasStudio(int y, int x) : base(y, x)
        {
            Name = "Miranda's Studio";
            Description = $@"
The wall of the room are covered with half-finished sketches and paintings, most of them in a decidely grotesque 
style. Aside from the artwork, the room is unfurnished save for a sleeping cushion and blanket thrown into one 
of the corners, seemingly as an afterthought. Hovering in the center of the room is a glowing green disc, it's 
surface covered in strange runes.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Dr. Rithbaun's Journal [Miranda's Studio]", $@"

So, it would appear that Miranda has jumped to another plane like the others. There's no way giving that woman 
access to that much power could ever end well.

She was always more of an artist than a scholar, but that approach made her into one of the most capable students 
I've had so far. She hadn't been able to create so much as a sketch since Vivian died, so alchemy must have been 
a welcome distraction. If only I'd realized how hard Vivian's death had hit her, perhaps I could've stopped this. 
But it would appear that in her despair, Miranda sought meaning in the discipline of alchemical Corruption. And 
those abominations are her magnum opus, a pefect manifestation of her despair.Given access to extraplanar resources, 
I'm not sure what kind of horrific works she'd create...

I'm running out of time. If I can find that portal's Anchor, I should be able to wrench it back open and retrieve 
Miranda.");
        }
    }

    public class CrystallographyLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Prismatic Dust")
            {
                Console.WriteLine($@"
You pour the Prismatic Dust into the top of the hourglass contraption. As it slowly falls, a brilliant blue
flame ignites within the furnace at the device's base. Once all of the dust has fallen, the flame vanishes, 
revealing an impossibly smooth crystalline prism.");
                RemoveItems.Add(item);
                Items.Add(new UnnaturalPrism());
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the floor next to the hourglass contraption.");
                game.GetNote();
            }
        }
        public CrystallographyLab(int y, int x) : base(y, x)
        {
            Name = "Crystallography Lab";
            Description = $@"
The walls of the room are completely taken up by shelves, each one packed haphazardly with crystals of varying 
shape, size, and color. Jut under half of them appear to be accompanied by an identifying label. At the center 
of the room is a contraption that resembles an hourglass. At it's base appears to be a kind of alchemical furnace 
but it's more advanced than any you've seen before.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Reminder: Make Prism [Crystallography Lab]", $@"

Just remembered that I need to synthesize another Prism for all those experiments on crossplanar optics I've been
putting off. Let's see, a Prism should be two components. They can most likely be combined at a regular alchemical
workstation, and then I'd just have to run the resulting product through this hourglass-filter/furnace combo. 
Been awhile since I've used this thing.

Now, what two components to use? Most likely alchemical Powders. But there are lot of different Powders. Oh, wait, I've 
got it! Probably a corruption Powder and a purification Powder! Yes, that'll do! Do I have any of those lying around?
Might have to go hunting. Hope Miranda and Aldric don't get too peeved if a few of their gribblies go missing...

- Theodore Grimtol");
        }
    }

    public class OpticsLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Unnatural Prism")
            {
                Console.WriteLine($@"
You place the Prism in the beam of light at the center of the platform. It splits the light into three seperate beams,
each directed at a different wall in the room. You notice that at the end of each beam an image is illuminated on the wall.

One image shows several suits of armor lined up in rows.

Another image shows a bear rearing up in an aggressive pose.

The third image shows an odd assortment of chairs, vases, and various treasures.

You also notice that a compartment in the platform has opened, revealing a strange handheld contraption.");
                Stage = "prism placed";
                RemoveItems.Add(item);
                Items.Add(new OpticalDisruptor());
                Description = $@"
The room is composed almost completely of white stone. Several tables line the walls, all of them covered in a 
disorganized array of lenses, crystals, and assorted alchemical components. The prism at the room's center illuminates
the walls with three images.

One image shows several suits of armor lined up in rows.

Another image shows a bear rearing up in an aggressive pose.

The third image shows an odd assortment of chairs, vases, and various treasures.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on one of the tables.");
                game.GetNote();
            }

            if (Stage == "prism placed")
            {
                var deviceCount = 0;
                for (var i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    if (item.Name == "Mysterious Device")
                    {
                        deviceCount++;
                    }
                }
                if (deviceCount == 3)
                {
                    for (var i = 0; i < player.Inventory.Count; i++)
                    {
                        var item = player.Inventory[i];
                        if (item.Name == "Mysterious Device" || item.Name == "Optical Disruptor")
                        {
                            player.Inventory.Remove(item);
                            i--;
                        }
                    }
                    Items.Add(new ShimmeringGem());
                    Console.WriteLine($@"
As you step into the room, you hear a strange beeping noise emanating from your pockets. You reach in and pull out
one of the Mysterious Devices you found. The three of them are definitely the source of the beeping, and they're 
temperatures appear to be rising.

Reacting quickly, you pull all three of the strange machines out of your pockets and hurl them away. You're greeted
by an explosion of dazzling lights to rival any fireworks display. As the smoke clears, you notice a shiny object on
the floor of the room.

On a whim, you pull out the Optical Disruptor. It immediately disintegrates into a pile of metallic dust.

{player.Name}: 'Well, that was odd.'");
                }
            }
        }
        public OpticsLab(int y, int x) : base(y, x)
        {
            Name = "Optics Lab";
            Description = $@"
The room is composed almost completely of white stone. Several tables line the walls, all of them covered in a 
disorganized array of lenses, crystals, and assorted alchemical components. A strange device in the center of 
the room's ceiling projects a beam of light directly downward, illuminating a platform at the room's center.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Fun With Optical Camouflage! [Optics Lab]", $@"

Finally figured out that optical camouflage idea I've been playing around with. And just in time! It seems
*someone* found out about the little treasure I've hidden away in this room, and couldn't resist digging 
around a bit. Probably Dr. Rithbaun. Oh, and if you're reading this, good Doctor, hello! Although I wouldn't 
put it past Aldric and Miranda to come snooping around in here too.

So, whoever you are, I've set up a fun little game for you. Just to give you a hint, I finally found a use for
all of father's old haunts. Well, my father anyway, not yours, Doctor. Or if you're Aldric or Miranda, then yes,
your father too. And if you're one of the gribblies running about...how are you even reading this note? All clear? 
Great.

Oh, hint number two: On it's own, hint number one is woefully insufficient. See first sentence of this note for 
reference. Good luck!

- Theodore Grimtol");
        }
    }

    public class LiquefactionLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Shimmering Gem")
            {
                Console.WriteLine($@"
You place the Shimmering Gem into the device's main container and close the lid. The machine immediately
whirs to life, and the console next to it lights up.");

                List<string> correctOrder = new List<string>() {
                    "i", "l", "r", "s", "b"
                };
                int index = 0;
                Console.WriteLine("\n<Press any key to continue.>");
                Console.ReadKey(true);
                while (index < correctOrder.Count)
                {
                    string currentAction = null;
                    Console.Clear();
                    Console.WriteLine("\nThe machine emits a faint hum.\n");
                    Console.WriteLine("\nChoose an action:\n");
                    Console.WriteLine("| I | Flip Ignition Switch");
                    Console.WriteLine("| S | Turn Small Valve");
                    Console.WriteLine("| L | Turn Large Valve");
                    Console.WriteLine("| B | Press Blue Button");
                    Console.WriteLine("| R | Press Red Button");
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.I)
                    {
                        Console.Clear();
                        Console.WriteLine("You flip the Ignition Switch.");
                        currentAction = "i";
                    }
                    else if (KeyInfo.Key == ConsoleKey.S)
                    {
                        Console.Clear();
                        Console.WriteLine("You turn the Small Valve.");
                        currentAction = "s";
                    }
                    else if (KeyInfo.Key == ConsoleKey.L)
                    {
                        Console.Clear();
                        Console.WriteLine("You turn the Large Valve.");
                        currentAction = "l";
                    }
                    else if (KeyInfo.Key == ConsoleKey.B)
                    {
                        Console.Clear();
                        Console.WriteLine("You press the Blue Button.");
                        currentAction = "b";
                    }
                    else if (KeyInfo.Key == ConsoleKey.R)
                    {
                        Console.Clear();
                        Console.WriteLine("You press the Red Button.");
                        currentAction = "r";
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You accidentally press some strange, unlabelled button.");
                    }
                    if (currentAction == correctOrder[index])
                    {
                        Console.WriteLine($@"
The machine starts emitting a horrifying clacking sound...and then you hear a soft chime.
A green light flickers slowly on the console.");
                        index++;
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        DeathFlag = true;
                        break;
                    }
                }
                if (!DeathFlag)
                {
                    Console.Clear();
                    Console.WriteLine($@"
The metal box at the end of the contraption suddenly snaps open, revealing a vial of sparkling liquid.");
                    RemoveItems.Add(item);
                    Items.Add(new IridescentFluid());
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on floor next to the strange device.");
                game.GetNote();
            }
            if (DeathFlag)
            {
                Console.WriteLine($@"
The machine suddenly grows quiet. You notice that a red light is flashing on the console.
It's hard to read the writing next to it, but it looks like it says 'Exposition Warring'.

...on closer inspection, it almost certainly says 'Explosion Warning'.

{player.Name}: 'Oh, you gotta be kidding m--'");
            }
        }
        public LiquefactionLab(int y, int x) : base(y, x)
        {
            Name = "Liquefaction Lab";
            Description = $@"
The room is empty except for a large device at its center. The contraption is a cylindrical container with a 
removable lid connected to a series of pipes that lead to a secondary chamber, which in turn is connected via 
a single tube to a small metal box. A single panel next to it is covered with poorly labelled buttons and 
valves that appear to control the operation of the device.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("The Delights of Low-Temperature Gem Fusion [Liquefaction Lab]", $@"

Honestly, the look on Aldric's face when I told him I have a lab *just* for liquefaction. What a rube.

But where was I? And why am I writing like this? It seems almost conversational...

Nope, nope, focus Theodore, focus! Okay. Right. I was devising a low-temperature process for converting 
that unbelievably shiny Gem into a liquid. Because why not?

But wait, dear reader! Yes, I know you're there. Why else would I be leaving all these notes around? Who has
such a terrible journal that it just drops pages at convenient locations?

But I digress. Dr. Rithbaun, or Aldric, or Miranda, or whoever you are...I'm going to share something with you. 
Turns out I already devised the very process I was describing! I just can't quite recall where I left all my 
notes.

Eh, I'm sure someone will track them down. Good luck finding the Gem, I have no idea where that thing went 
off to.

I will give you one hint: The fifth and final step in the prcoess is pressing the Blue Button. That activates the
filtering device. Not that it really matters, you probably just want to get something out of this room so
you can move on.

- Theodore Grimtol");
        }
    }

    public class VaporizationLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Extraplanar Fluid")
            {
                Console.WriteLine($@"
You pour the Extraplanar Fluid into the glass chamber of the device and shut the door.
After you flip the switch on the nearby panel the machine emits a shrill tone. You
see the liquid inside the chamber slowly transform into a cloud of glowing violet gas.

After a few minutes, the gas is sucked though a tube into the connected metal box.
The box snaps open, revealing a stoppered glass bottle filled with the strange vapor.");
                RemoveItems.Add(item);
                Items.Add(new GlowingVapor());
            }
            else if (item.Name == "Iridescent Fluid")
            {
                Console.WriteLine($"That seems to be a logical idea, but it can't be used in its current form.");
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on the table next to the strange device.");
                game.GetNote();
            }
        }
        public VaporizationLab(int y, int x) : base(y, x)
        {
            Name = "Vaporization Lab";
            Description = $@"
The room is a mess of tables and shelves strewn with a combination of alchemical, handwritten notes, and 
mechanical components of all sorts. On a table at the center of the room sits a device consisting of a 
glass chamber with a removable door. A thin pipe connects the interior of the chamber to a metal box on 
a seperate table. The device appears to be operated by a nearby panel with a single switch.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Vaporizer Instructions [Vaporization Lab]", $@"

Welcome back, mysterious reader! Now, if you're not carrying around a liquified gem right
now, don't even bother reading this. Or do, I don't really care.

Now, if you *are* carrying around some sort of shiny, gem-derived fluid right now, then
I'm sorry about what you just went through. I really need to write up better instructions 
for that machine.

But now I'm sure, like any true alchemist, you want to *vaporize* that stuff. That'd be my
first thought, anyway. The device in this room will take care of it. You just have to do 
a few things beforehand to prep that gem-liquid you're lugging around.

First things first: You're gonna need to create an extraplanar fluid. It's not as hard as 
it sounds...you can actually do it at a basic alchemy workstation. Weirdly enough, all you 
need to do is combine the gem-fluid you already have with an Advanced Alchemical Substrate.
You know how to craft those, right?

Once you have that extraplanar fluid, all you need to do is plop it in the machine, flip
a switch, and you're golden.

At least, I think that's how it works. It's been awhile...

- Theodore Grimtol");
        }
    }

    public class GrandLibrary : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Glowing Vapor")
            {
                Console.WriteLine($@"
You uncork the bottle of Glowing Vapor and watch as it fills the sprawling library. You find that you can 
swim through the gas almost like water. This should make it easy to access previously out-of-reach areas.

The downside is that the thick vapor makes it a bit hard to see. Also, it's getting a little harder to 
breathe...might want to make this fast.");
                List<string> directions = new List<string>() {
                    "u", "n", "d", "w", "e", "s"
                };
                Dictionary<string, string> clues = new Dictionary<string, string>()
                {
                    //TODO: finish clues
                    {"n", "It's an engraving of a starry sky. One star seems slightly larger than the others. The label next to it reads 'Polaris'."},
                    {"e", "It's a <east clue>"},
                    {"s", "It's a <south clue>"},
                    {"w", "It's a <west clue>"},
                    {"u", "It's a statue of an angelic figure carved from alabaster."},
                    {"d", "It's a statue of a horned demon carved from basalt."}
                };

                List<string> correctOrder = new List<string>() {
                    "u", "n", "d", "w", "e", "s"
                };
                for (var i = 0; i < 20; i++)
                {
                    Random r = new Random();
                    int randIndex = r.Next(0, directions.Count);
                    var direction = correctOrder[randIndex];
                    correctOrder.Remove(direction);
                    correctOrder.Add(direction);
                }
                //TODO: Better randomization method
                int index = 0;
                Console.WriteLine("\n<Press any key to continue.>");
                Console.ReadKey(true);
                while (index < correctOrder.Count)
                {
                    string currentMove = null;
                    Console.Clear();
                    Console.WriteLine("\nThrough the thick haze, you see an object before you...\n");
                    Console.WriteLine($"\n{clues[correctOrder[index]]}\n");
                    Console.WriteLine("\nChoose an action:\n");
                    Console.WriteLine("| W | Move North");
                    Console.WriteLine("| D | Move East");
                    Console.WriteLine("| S | Move South");
                    Console.WriteLine("| A | Move West");
                    Console.WriteLine("| Z | Move Up");
                    Console.WriteLine("| X | Move Down");
                    KeyInfo = Console.ReadKey(true);
                    if (KeyInfo.Key == ConsoleKey.A || KeyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You float to the west.");
                        currentMove = "w";
                    }
                    else if (KeyInfo.Key == ConsoleKey.W || KeyInfo.Key == ConsoleKey.UpArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You float to the north.");
                        currentMove = "n";
                    }
                    else if (KeyInfo.Key == ConsoleKey.D || KeyInfo.Key == ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You float to the east.");
                        currentMove = "e";
                    }
                    else if (KeyInfo.Key == ConsoleKey.S || KeyInfo.Key == ConsoleKey.DownArrow)
                    {
                        Console.Clear();
                        Console.WriteLine("You float to the south.");
                        currentMove = "s";
                    }
                    else if (KeyInfo.Key == ConsoleKey.Z)
                    {
                        Console.Clear();
                        Console.WriteLine("You float upward.");
                        currentMove = "u";
                    }
                    else if (KeyInfo.Key == ConsoleKey.X)
                    {
                        Console.Clear();
                        Console.WriteLine("You float downward.");
                        currentMove = "d";
                    }
                    else
                    {
                        Console.Clear();
                        Random r = new Random();
                        int randIndex = r.Next(0, directions.Count);
                        currentMove = directions[randIndex];
                        Console.WriteLine("You're not sure what direction you just went in, but hopefully it's the right one.");
                    }
                    if (currentMove == correctOrder[index])
                    {
                        Console.WriteLine("\nYou float onward through the glowing vapor..\n");
                        index++;
                        Console.WriteLine("\n<Press any key to continue.>");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        //TODO: Allow multiple mistakes before death flag
                        DeathFlag = true;
                        break;
                    }
                }
                if (!DeathFlag)
                {
                    Console.Clear();
                    Console.WriteLine($@"
You find yourself standing in front of an a shelf probably located in some deeply hidden section 
of the library. Nestled in the space between two books is a strange orb flickering with blue 
and violet light.

As you reach out to grab it, it suddenly falls from its perch and vanishes into the clouds below...
Except, you immediately hear a dull thud. You notice the vapor beginning to disperse...and notice
that both you and the orb are both resting on the main walkway at the center of the library.

Apparently the orb was right next to you at the start. Go figure.");
                    RemoveItems.Add(item);
                    Items.Add(new AnchorOfTransmutation());
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a note on one of the walkways.");
                game.GetNote();
            }
            if (DeathFlag)
            {
                Console.WriteLine($@"
You continue floating through the vapor for what feels like ages, but fail to come across
anything of use. You feel youself getting lightheaded.

lLoks like you made a wrong turn somewhere...");
            }
        }
        public GrandLibrary(int y, int x) : base(y, x)
        {
            Name = "Grand Library";
            Description = $@"
The first thing you notice is that the walls of the room are completely covered in bookshelves, all 
haphazardly packed with tomes of all sorts. The second thing you notice is that both the ceiling and 
floor of the room have been carved out, and filled with a complex mess of rickety wooden walkways and...
even *more* bookshelves. The 'floor' of the level on which you're currently standing is little more 
than a haphazardly constructed platform itself. Even stranger, there doesn't even appear to be a way to 
reach any of the bookshelves aside from those on the level where you're currently standing.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Anchor Hiding Place [Grand Library]", $@"

Ah, my favorite room in the castle. A shame that over half the books are all but inaccessible now.
What to do about that...

Wait, here's an idea: A portable propulsion system! No, too elaborate. Hmm...think Theodore,
think! It's not like I can just swim through the air...Oh. OH. But what if I were to *replace* 
the air with something a bit more amenable to flotation...some kind of alchemical gas might
do the trick...

I mean, I have to find some place to hide this Anchor. A virtually inaccessable alcove somewhere in
the library should be perfect.

But I'll leave clues, of course. What if I need to find it again? Can't rely on my memory...I can't even 
remember what I ate for breakfast. Wait, did I even eat breakfast today? Hmm...

- Theodore Grimtol");
        }
    }

    public class TheodoresStudy : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a page that looks like it's been wripped out of a journal. It's in Dr. Rithbaun's handwriting.");
                game.GetNote();
            }
        }
        public TheodoresStudy(int y, int x) : base(y, x)
        {
            Name = "Theodores's Study";
            Description = $@"
The room is essentially a maze of bookshelves leading to a desk at the center. Open books and scattered notes 
lay everywhere. It's so cluttered that you almost don't notice the overing in the glowing violet disc floating 
just above the desk. The surface of the disk is marked with luminous runes from a language you don't recognize.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Dr. Rithbaun's Journal [Theodore's Study]", $@"

Well, I can't say I'm that surprised to find a portal here. Out of the three Grimtol heirs, Theodore would be 
the most likely to dabble in interplanar travel. Not that that's at all comforting.

That man was easily my greatest pupil. A true scholar seeking to unlock the mysteries of the unvierse. His 
grasp of the alchemical discipline of Transmutation is positively astounding. I should have guessed that he 
was one of those students who would walk to close to the edge in their pursuit of knowledge. At any rate, his 
thirst for knowledge appears to have driven him into an even deeper madness than his siblings. The damage he 
could cause with planar travel in his toolkit is almost immeasurable.

I'll have to find the Anchor he used to create thi portal, and quickly. If I bring it here, I should be able 
to bring him back to this plane before he breaks reality.");
        }
    }

    public class CentralChamber : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Note Note { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            if (!(Note == null))
            {
                Console.WriteLine("\nYou spot a page that looks like it's been wripped out of a journal. It's in Dr. Rithbaun's handwriting.");
                game.GetNote();
            }
        }
        public CentralChamber(int y, int x) : base(y, x)
        {
            Name = "Central Chamber";
            Description = $@"
You're in an unfurnished chamber made from roughly hewn stone. It seems that the castle residents never 
got around to using it for anything in particular. Curiously, however, there's a strange, glowing white 
disc floating in the center of the room, its surface dotted with unfamiliar runes.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Dr. Rithbaun's Journal [Central Chamber]", $@"

It would appear I underestimated those three. They had a trap waiting for me the whole time. No sense 
running now, I can already see the portal forming before my eyes. It's more advanced than the others. 
I'd wager all three of them had a hand in building it...no telling where the Anchor is hidden...

What a nuisance. I just hope that letter I sent out reaches its destination. It's my only hope at 
this rate.");
        }
    }

    public class ForgottenVault : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Optical Disruptor")
            {
                Console.WriteLine($@"
You press the switch on the Optical Disruptor. It gives off a pulse of violet light.

You look around the room. You notice that in the portrait of Augustus Grimtol, the man's
fur coat has been replaced by bright red long johns. A live racoon appears to be perched on his head.

Additionally, you now notice a strange object beneath the painting that you hadn't seen before.");
                Items.Add(new MysteriousDevice());
                Description = $@"
The stone room is filled with expensive-looking antiques and furniture, all arranged hapahazardly. 
Most likely just a storage area. The copious amounts of dust suggest it doesn't get
many visitors.

The portrait of Augustus Grimtol now depicts the man wearing red long johns, a live racoon perched 
atop his head.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public ForgottenVault(int y, int x) : base(y, x)
        {
            Name = "Forgotten Vault";
            Description = $@"
The stone room is filled with expensive-looking antiques and furniture, all arranged hapahazardly. 
Most likely just a storage area. The copious amounts of dust suggest it doesn't get
many visitors.

Hanging prominently on one wall is a painting of a man with a bushy mustache standing in a snowy tableu,
robed in a majestic fur coat. Below the painting is a plaque reading 'Augustus Grimtol'.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class OperatingRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override bool CraftingArea { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Surgical Knife")
            {
                Console.WriteLine($@"
You use the Surgical Knife to cut into the strange corpse on the table. It's surprisingly
easy. It looks as if there is, in fact, a heart inside the horrific thing. Given the crazy
state of this castle it might actually be of use somewhere.");
                RemoveItems.Add(item);
                Items.Add(new MalformedHeart());
                Description = $@"
The room is bare save for a row of operating tables and a large, metal locker. A medicinal smell fills 
the air. The door to the locker lies open, revealing a pile of strange corpses resembling creatures 
out of a nightmare. On one of the operating tables lies the body of something that looks vaguely humanoid, 
a gaping hole in it chest as a result of your slapdash surgical technique. A simple alchemical workstation 
has been set up in one of the room's corners.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }

        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public OperatingRoom(int y, int x) : base(y, x)
        {
            Name = "Operating Room";
            Description = $@"
The room is bare save for a row of operating tables and a large, metal locker. A medicinal smell fills 
the air. The door to the locker lies open, revealing a pile of strange corpses resembling creatures 
out of a nightmare. On one of the operating tables lies the body of something that looks vaguely humanoid, 
save for the gray skin and extra limbs. A black circle has been drawn on its chest, about where it's heart 
would be if it were human. A simple alchemical workstation has been set up in one of the room's corners.";
            CraftingArea = true;
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class TrophyRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Optical Disruptor")
            {
                Console.WriteLine($@"
You press the switch on the Optical Disruptor. It gives off a pulse of violet light.

As you look around the room again, you notice a few changes. In the portrait of Augustus Grimtol,
the rifle has been replaced with a comically oversized lollipop, and the once stern-looking man
now appears to be crying like a spoiled child.

Additionally, you now notice a strange object beneath the painting that you hadn't seen before.");
                Items.Add(new MysteriousDevice());
                Description = $@"
You're in a simple room with wood-panelled walls. It appears to be some sort of hunter's trophy room, and is 
filled with taxidermied animals of all sorts. Once majestic elk now gaze at you with glass eyes, and a 
ferociously positioned bear rears up at the center of the room.

The portrait of Augustus Grimtol now depicts the man holding a comically oversized lollipop and crying like
a spoiled child.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public TrophyRoom(int y, int x) : base(y, x)
        {
            Name = "Trophy Room";
            Description = $@"
You're in a simple room with wood-panelled walls. It appears to be some sort of hunter's trophy room, and is 
filled with taxidermied animals of all sorts. Once majestic elk now gaze at you with glass eyes, and a 
ferociously positioned bear rears up at the center of the room.

Hanging prominently on one wall is a painting of a man with a bushy mustache holding an old-looking rifle, 
a look of mild annoyance on his face. Below the painting is a plaque reading 'Augustus Grimtol'.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class DustyArmory : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            if (item.Name == "Optical Disruptor")
            {
                Console.WriteLine($@"
You press the switch on the Optical Disruptor. It gives off a pulse of violet light.

You look around the room. You notice that in the portrait of Augustus Grimtol, the man
is now dressed in a ridiculous clown outfit, complete with makeup.

Additionally, you now notice a strange object beneath the painting that you hadn't seen before.");
                Items.Add(new MysteriousDevice());
                Description = $@"
The walls of the room are covered in weapons straight out of medieval Europe, alongside tapestries depicting 
historic battles. The floorspace is devoted to immaculately maintained suits of armor from various nations.

The portrait of Augustus Grimtol now depicts the man in a ridiculous clown outfit.";
            }
            else
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public DustyArmory(int y, int x) : base(y, x)
        {
            Name = "Dusty Armory";
            Description = $@"
The walls of the room are covered in weapons straight out of medieval Europe, alongside tapestries depicting 
historic battles. The floorspace is devoted to immaculately maintained suits of armor from various nations.

Hanging prominently on one wall is a painting of a man in a military uniform and a bushy mustache. Below the
painting is a plaque reading 'Augustus Grimtol'.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }
}
