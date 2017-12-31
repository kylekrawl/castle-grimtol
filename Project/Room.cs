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
        public virtual Enemy Enemy { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public bool PassagesBuilt { get; set; }
        public virtual bool CraftingArea { get; set; }
        public virtual bool VisitedByPlayer { get; set; }
        public List<string> Exits { get; set; } = new List<string>();
        public virtual string EventStage { get; set; }
        public virtual Trap Trap { get; set; }
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
            Enemy = null;
            Trap = null;
            EventStage = null;
        }
    }

    public class MainFoyer : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override bool VisitedByPlayer { get; set; }
        public override Note Note { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nNothing happens. This is just a start room after all.");
        }
        public MainFoyer(int y, int x) : base(y, x)
        {
            Name = "Main Foyer";
            Description = $@"
A large room crafted from dark, polished stone. A collection of tapestries and paintings adorn the walls, 
and a large, ornately carved table sits in the room's center. A makeshift alchemical workstation takes 
up most of the table's space...probably Dr. Rithbaun's handiwork.";
            Y = y;
            X = x;
            VisitedByPlayer = true;
            Note = new Note("Page from Rithbaun's Journal: Main Foyer", $@"

<It looks like Dr. Rithbaun's handwriting...>

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
            public override bool CraftingArea { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public AssemblyLab(int y, int x) : base(y, x)
            {
                Name = "Assembly Lab";
                Description = $@"
You're in a room lit by a dim orange glow. Dark metal plates are affixed to the walls, and the floorspace is almost entirely taken up by metal operating
tables aligned in neat, even rows. Several of them are occupied by what look to be human skeletons with strange bit of machinery attached to them. 
A tidy alchemical workstation sits in one coerner of the room.";
                Y = y;
                X = x;
                CraftingArea = true;
                RespawnItems = new List<Item>() { new BoneAsh(), new CrimsonOil() };
                Items = new List<Item>() { new BoneAsh(), new CrimsonOil() };
            }
        }

        public class StorageArea : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public StorageArea(int y, int x) : base(y, x)
            {
                Name = "Storage Area";
                Description = $@"
The room is lit only by a faintly glowing lamp on a counter at its center. The walls are completely covered with square metal lockers,
the majority of which are filled with various mechanical components. A few of them contain bones, neatly sorted by type.";
                Y = y;
                X = x;
                RespawnItems = new List<Item>() { new BoneAsh(), new MetalCore() };
                Items = new List<Item>() { new BoneAsh(), new MetalCore() };
            }
        }

        public class OvergrownLab : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override bool CraftingArea { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public OvergrownLab(int y, int x) : base(y, x)
            {
                Name = "Overgrown Lab";
                Description = $@"
You're in a room that looks like it may have once been a lab, but is now almost entirely covered by a strange black fungus or moss. Thorned vines
jut from cracks in the dark stone walls at odd intervals, and nearly all of the tables in the room have been overtaken by the growth. Only a small
alchemical workstation in one corner of the room appears to be undisturbed.";
                Y = y;
                X = x;
                CraftingArea = true;
                RespawnItems = new List<Item>() { new AcridPowder(), new PutridNodule() };
                Items = new List<Item>() { new AcridPowder(), new PutridNodule() };
            }
        }

        public class GerminationVats : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public GerminationVats(int y, int x) : base(y, x)
            {
                Name = "Germination Vats";
                Description = $@"
Aside from a metal walkway around its edges, the floor of this room appears to have been rather haphazardly dug out. The gaping pit at its center is
filled with rows of massive glass containers filled with a bright green, cloudy liquid. A few of them seem to have large, dark shapes suspended at their center.
They look vaguely organic, but beyond that you can't quite identify them.";
                Y = y;
                X = x;
                RespawnItems = new List<Item>() { new AcridPowder(), new YellowIchor() };
                Items = new List<Item>() { new AcridPowder(), new YellowIchor() };
            }
        }

        public class MakeshiftLab : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override bool CraftingArea { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public MakeshiftLab(int y, int x) : base(y, x)
            {
                Name = "Makeshift Lab";
                Description = $@"
The room appears to be some kind of kitchen and dining area that has been somewhat hapazardly repurposed as a lab. Pages and pages of notes cover almost every
available surface, and alchemical tools of all sorts seem to be scattered seemingly at random around the room.";
                Y = y;
                X = x;
                CraftingArea = true;
                RespawnItems = new List<Item>() { new LuminousDust(), new QuiveringOoze() };
                Items = new List<Item>() { new LuminousDust(), new QuiveringOoze() };
            }
        }

        public class HaphazardLibrary : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public HaphazardLibrary(int y, int x) : base(y, x)
            {
                Name = "Hapahazard Library";
                Description = $@"
The room looks to have been a pantry or cellar of some sort. The wooden shelves that line it's walls, however, are now filled entirely with books. They all look to be
fairly advanced alchemical texts, along with a few tomes on cross-planar travel and even more esoteric topics.";
                Y = y;
                X = x;
                RespawnItems = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
                Items = new List<Item>() { new LuminousDust(), new TwistedCrystal() };
            }
        }

        public class RefiningLab : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public RefiningLab(int y, int x) : base(y, x)
            {
                Name = "Refining Lab";
                Description = $@"
Sturdy wooden counters lined with various alchemical and blackmithing tools are arranged along the room's walls. At it's center lies
a large ceramic kiln. Neat rows of copper wiring link the kiln's base to a mechanical device beside it. Probably an alchemical battery of some sort.
The whole apparatus looks functional, and in fact appears to have been recently used. A powdery residue lines its interior.";
                Y = y;
                X = x;
                Items = new List<Item>() { new AlchemicalResidue() };
            }
        }

        public class FurnaceRoom : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public FurnaceRoom(int y, int x) : base(y, x)
            {
                Name = "Furnace Room";
                Description = $@"
The entire room is lined with thick metal plating affixed to that walls with rivets. It's empty save for a large metal cylinder at its center. On closer inspection,
the object appears to be an alchemical furnace of some sort, it's hatch ajar. The interior is room temperature...it's clearly not working. The console next to it has
a hemispherical indentation.";
                Y = y;
                X = x;
                Items = new List<Item>();
            }
        }

        public class AbandonedParlor : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public AbandonedParlor(int y, int x) : base(y, x)
            {
                Name = "Abandoned Parlor";
                Description = $@"
The room's walls are decorated with a wallpaper bearing a repeating geometric pattern, and the floor is covered with an expensive-looking rug. Two couches sit before
an elaborate stone fireplace at one edge of the room, both covered in a thin layer of dust. In one corner of the room the wallpaper has been peeled aside, revealing a 
a large safe set into the wall. A keyhole sits conspicuously above its handle.";
                Y = y;
                X = x;
                Items = new List<Item>();
            }
        }

        public class ConvertedSunroom : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public ConvertedSunroom(int y, int x) : base(y, x)
            {
                Name = "Converted Sunroom";
                Description = $@"
The outside-facing walls of the room feature large windows that have since been neatly boarded up. The skylight in the ceiling has been given a similar treatment.
The room itelf is completely empty save for two features. Eight iron braziers are arranged along the room's edges, one in each corner and one at the midpoint of each wall. 
A raised stone platform sit at the room's center. The platform has a strange, eight-pointed indentation at its center. Around this indentation, the phrase 'post tenebras lux' 
has been carved in large, Gothic letters.";
                Y = y;
                X = x;
                Items = new List<Item>();
            }
        }

        public class ImmaculateGallery : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public ImmaculateGallery(int y, int x) : base(y, x)
            {
                Name = "Immaculate Gallery";
                Description = $@"
The room is crafted almost entirely from white stone, and lit by lanterns tucked into wall sconces at regular intervals. The walls
are lined with paintings, most of them rendered in a stunningly realistic style. Statues of human figures reminiscent of the work
of the great Classical sculptors are arranged neatly around the room's edges. At the center of the room is a massive jade statue of
an Eastern dragon. It's eyes seem to stare directly at you. The statues's tail is wrapped around an egg seemingly carved from solid gold. 
At the base of the statue is an elaborate arrangement of thick copper wires, all of them connected to a cylindrical indentation that currently 
lies empty.";
                Y = y;
                X = x;
                Items = new List<Item>();
            }
        }

        public class AldricsStudy : Room, IRoom
        {
            public override string Name { get; set; }
            public override string Description { get; set; }
            public override List<Item> Items { get; set; }
            public override List<Item> RespawnItems { get; set; }
            public override void UseItem(Item item)
            {
                Console.WriteLine($"{item.Name} fails to be of any use.");
            }
            public override void Event(Game game, Player player)
            {
                Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
            }
            public AldricsStudy(int y, int x) : base(y, x)
            {
                Name = "Aldric's Study";
                Description = $@"
A wood-panelled room decorated with tapestries and sculptures, all placed with seemingly painstaking precision. A small desk lies in one corner, the papers on its 
surface neatly organized. Several bookshelves line the walls, and a quick glance suggests the books have been arranged alphabetically. The only thing out of place 
in the room is the glowing red disc hovering in midair at the room's center. Strange runes flicker across it's surface.";
                Y = y;
                X = x;
                Items = new List<Item>();
                Note = new Note("Page from Rithbaun's Journal: Aldric's Study", $@"

<It looks like Dr. Rithbaun's handwriting...>

This is worse than I thought. If that portal in his study is any indication, Aldric has found a way to travel between planes. No telling what sorts of power he could
gain.

He was a promising enough student. But it would appear that alchemy was only a means to an end for him. He was obsessed with honing his body and mind, and saw the discipline of
alchemical Purification as the path to perfection. It would've been fine if he'd stopped there, but once he started ranting about elevating humanity itself to a more ideal form,
I knew there'd be trouble. I don't quite know why, but that man despised human nature. I'm not sure how those infernal things he created play into his crazed vision, but I'd rather 
not wait around to find out.

If I can reopen that portal, I might be able to pull him back to this plane. For a portal like that, he'd have to have hidden an Anchor somewhere...if I can find it, opening this
portal will be a trivial matter.");
            }
        }
    }

    public class FetidCourtyard : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public FetidCourtyard(int y, int x) : base(y, x)
        {
            Name = "Fetid Courtyard";
            Description = $@"
You're in an open courtyard that looks to have been abandoned long ago. The remnants of cobblestone pathways are barely visible beneath overgrown shrubs. The pool at the 
courtyard's center is clogged with thick slime that appears to be slowly eating away at the marble tiles around it. You can barely make out an object beneath the 
corrosive muck.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class MacabreWorkroom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public MacabreWorkroom(int y, int x) : base(y, x)
        {
            Name = "Macabre Workroom";
            Description = $@"
The room is dimly lit by a faint green glow. As your eyes adjust to the light, you see that the walls of the room are covered with sketches, all depicting horrific
figures. Some appear to be horrifically deformed skeletons, while others resemble carnivorous, plantlike creatures. Hanging in the center of the room is a collection 
of bones wired together into a terrifiying skeletal form. The grim sculpture appears to be missing a head.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class ClockworkGarden : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public ClockworkGarden(int y, int x) : base(y, x)
        {
            Name = "Clockwork Garden";
            Description = $@"
The room is dominated by a massive structure composed of a multitude of gears and mechanical limbs that protude from the walls, ceiling and floor to form a veritable
maze of strange clockwork parts. On closer inspection, the contraption appears to have been designed with a plant motif -- the gears resemble thorned flowers, while the 
mechanical limbs mimic vines and branches. The centerpoint of the device appears to be a 'trunk' at the center of the room. It's covered with gears of varying sizes. You
notice that only one of the gears is spinning...the gear that would go next to it appears to be missing.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class RansackedWorkroom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public RansackedWorkroom(int y, int x) : base(y, x)
        {
            Name = "Ransacked Workroom";
            Description = $@"
The room looks to have been used for artistic work at some point, but now lies in complete disarray. Discarded sketches cover the floor alongside spilled
containers of ink and paint. An overturned easel lies in one corner next to a wall of empty shelves. Only one item appears undisturbed: A large painting of
a young girl hanging on one of the walls. Beneath the painting a small brass plate with the name 'Vivian Grimtol' inscribed on it. On closer inspection, there appears to
be a hole gouged in the portrait where the subject's left eye should be.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class ViviansRoom : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public ViviansRoom(int y, int x) : base(y, x)
        {
            Name = "Vivian's Room";
            Description = $@"
You're in what looks to be a child's room. The wallpaper features a pink, floral design, and the ceiling has been painted to resemble a blue sky with a few
puffy clouds. The bed looks unmade, yet the thin layer of dust coating every surface in the room suggest it hasn't been disturbed in awhile. A few dolls lie scattered
on the floor.";
            Y = y;
            X = x;
            Items = new List<Item>();
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
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public MirandasStudio(int y, int x) : base(y, x)
        {
            Name = "Miranda's Studio";
            Description = $@"
The wall of the room are covered with half-finished sketches and paintings, most of them in a decidely grotesque style. Aside from the artwork,
the room is unfurnished save for a sleeping cushion and blanket thrown into one of the corners, seemingly as an afterthought. Hovering in the 
center of the room is a glowing green disc, it's surface covered in strange runes.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Rithbaun's Journal: Miranda's Studio", $@"

<It looks like Dr. Rithbaun's handwriting...>

So, it would appear that Miranda has jumped to another plane like the others. There's no way giving that woman access to that much power could ever end well.

She was always more of an artist than a scholar, but that approach made her into one of the most capable students I've had so far. She hadn't been able to create so much as a
sketch since Vivian died, so alchemy must have been a welcome distraction. If only I'd realized how hard Vivian's death had hit her, perhaps I could've stopped this. But it 
would appear that in her despair, Miranda sought meaning in the discipline of alchemical Corruption. And those abominations are her magnum opus, a pefect manifestation of her despair.
Given access to extraplanar resources, I'm not sure what kind of horrific works she'd create...

I'm running out of time. If I can find that portal's Anchor, I should be able to wrench it back open and retrieve Miranda.");
        }
    }

    public class CrystallographyLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public CrystallographyLab(int y, int x) : base(y, x)
        {
            Name = "Crystallography Lab";
            Description = $@"
The walls of the room are completely taken up by shelves, each one packed haphazardly with crystals of varying shape, size, and color. Jut under half of them appear to be accompanied
by an identifying label. At the center of the room is a contraption that resembles an hourglass. At it's base appears to be a kind of alchemical furnace but it's more advanced than
any you've seen before.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class OpticsLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public OpticsLab(int y, int x) : base(y, x)
        {
            Name = "Optics Lab";
            Description = $@"
The room is composed almost completely of white stone. Several tables line the walls, all of them covered in a disorganized array of lenses, crystals, and assorted alchemical
components. A strange device in the center of the room's ceiling projects a beam of light directly downward, illuminating a platform at the room's center.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class LiquefactionLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public LiquefactionLab(int y, int x) : base(y, x)
        {
            Name = "Liquefaction Lab";
            Description = $@"
The room is empty except for a large device at its center. The contraption is a cylindrical container with a removable lid connected
to a series of pipes that lead up to the ceiling. A single panel next to it is covered with poorly labelled buttons and valves that appear to
control the operation of the device.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class VaporizationLab : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public VaporizationLab(int y, int x) : base(y, x)
        {
            Name = "Vaporization Lab";
            Description = $@"
The room is a mess of tables and shelves strewn with a combination of alchemical, handwritten notes, and mechanical components of all sorts. On a
table at the center of the room sits a device consisting of a glass chamber with a removable door. A thin pipe connects the interior of the chamber
to a metal box on a seperate table. The device appears to be operated by a nearby panel of levers and valves, a few of them labelled with nearly
illegible handwriting.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }

    public class GrandLibrary : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public GrandLibrary(int y, int x) : base(y, x)
        {
            Name = "Grand Library";
            Description = $@"
The first thing you notice is that the walls of the room are completely covered in bookshelves, all haphazardly 
packed with tomes of all sorts. The second thing you notice is that both the ceiling and floor of the room have been carved out,
and filled with a complex mess of rickety wooden walkways and...even *more* bookshelves. The 'floor' of the level on which you're currently standing
is little more than a haphazardly constructed platform itself. Even stranger, there doesn't even appear to be a way to reach any of the bookshelves 
aside from those on the level where you're currently standing.";
            Y = y;
            X = x;
            Items = new List<Item>();
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
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public TheodoresStudy(int y, int x) : base(y, x)
        {
            Name = "Theodores's Study";
            Description = $@"
The room is essentially a maze of bookshelves leading to a desk at the center. Open books and scattered notes lay everywhere. It's so cluttered that
you almost don't notice the overing in the glowing violet disc floating just above the desk. The surface of the disk is marked with luminous runes from
a language you don't recognize.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Rithbaun's Journal: Theodore's Study", $@"

<It looks like Dr. Rithbaun's handwriting...>

Well, I can't say I'm that surprised to find a portal here. Out of the three Grimtol heirs, Theodore would be the most likely to dabble in interplanar travel. Not that that's
at all comforting.

That man was easily my greatest pupil. A true scholar seeking to unlock the mysteries of the unvierse. His grasp of the alchemical discipline of Transmutation is positively
astounding. I should have guessed that he was one of those students who would walk to close to the edge in their pursuit of knowledge. At any rate, his thirst for knowledge 
appears to have driven him into an even deeper madness than his siblings. The damage he could cause with planar travel in his toolkit is almost immeasurable.

I'll have to find the Anchor he used to create thi portal, and quickly. If I bring it here, I should be able to bring him back to this plane before he breaks reality.");
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
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public CentralChamber(int y, int x) : base(y, x)
        {
            Name = "Central Chamber";
            Description = $@"
You're in an unfurnished chamber made from roughly hewn stone. It seems that the castle residents never got around to
using it for anything in particular. Curiously, however, there's a strange, glowing white disc floating in the center of the room,
it' surface dotted with unfamiliar runes.";
            Y = y;
            X = x;
            Items = new List<Item>();
            Note = new Note("Page from Rithbaun's Journal: Central Chamber", $@"

<It looks like Dr. Rithbaun's handwriting...>

It would appear I underestimated those three. They had a trap waiting for me the whole time. No sense running now, I can already see the 
portal forming before my eyes. It's more advanced than the others. I'd wager all three of them had a hand in building it...no telling where the
Anchor is hidden...

What a nuisance. I just hope that letter I sent out reaches its destination. It's my only hope at this rate.");
        }
    }

    public class ForgottenDungeon : Room, IRoom
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<Item> Items { get; set; }
        public override List<Item> RespawnItems { get; set; }
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public ForgottenDungeon(int y, int x) : base(y, x)
        {
            Name = "Forgotten Dungeon";
            Description = $@"
The stone room is lined with cells, their iron bars coated with rust. Look to be some sort of dungeon...fitting for a castle. Although'
a few of the cells have a few bones strewn about, it doesn't look like thi dungeon has been getting much use as of late.";
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
        public override void UseItem(Item item)
        {
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public OperatingRoom(int y, int x) : base(y, x)
        {
            Name = "Operating Room";
            Description = $@"
The room is bare save for a row of operating tables and a large, metal locker. A medicinal smell fills the air. The door to the locker lies open, revealing a 
pile of strange corpses resembling creatures out of a nightmare. On one of the operating tables lies the body of something that looks vaguely humanoid, save for 
the gray skin and extra limbs. A black circle has been drawn on its chest, about where it's heart would be if it were human. A simple alchemical workstation has 
been set up in one of the room's corners.";
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
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public TrophyRoom(int y, int x) : base(y, x)
        {
            Name = "Trophy Room";
            Description = $@"
You're in a simple room with wood-panelled walls. It appears to be some sort of hunter's trophy room, and is filled with taxidermied animals of all sorts.
Once majestic elk now gaze at you with glass eyes, and a ferociously positioned bear rears up at the center of the room.";
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
            Console.WriteLine($"{item.Name} fails to be of any use.");
        }
        public override void Event(Game game, Player player)
        {
            Console.WriteLine("\nYou don't detect any threats, but still feel a bit unsettled.");
        }
        public DustyArmory(int y, int x) : base(y, x)
        {
            Name = "Dusty Armory";
            Description = $@"
The walls of the room are covered in weapons straight out of medieval Europe, alongside tapestries depicting historic battles. The floorspace is devoted to
immaculately maintained suits of armor from various nations.";
            Y = y;
            X = x;
            Items = new List<Item>();
        }
    }
}
