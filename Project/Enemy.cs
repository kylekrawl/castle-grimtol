using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Enemy
    {
        public virtual string Name { get; set; }
        public virtual string ApproachDescription { get; set; }
        public virtual string CombatDescription { get; set; }
        public virtual string DefeatedDescription { get; set; }
        public virtual string VictoriousDescription { get; set; }
        public virtual string Type { get; set; }
        public virtual double Health { get; set; }
        public virtual double MaxHealth { get; set; }
        public virtual double DefenseRating { get; set; }
        public virtual double MaxDefenseRating { get; set; }
        public virtual List<Attack> Attacks { get; set; }
        public virtual List<Item> DropItems { get; set; }

        public Item DropItem()
        {
            Random r = new Random();
            int randIndex = r.Next(0, DropItems.Count);
            return DropItems[randIndex];
        }

        public Enemy()
        {
            Name = "Enemy";
            ApproachDescription = "";
            CombatDescription = "";
            DefeatedDescription = "";
            VictoriousDescription = "";
            Type = "";
            Health = 0;
            MaxHealth = 0;
            DefenseRating = 0;
            MaxDefenseRating = 0;
            Attacks = new List<Attack>();
            DropItems = new List<Item>();
        }
    }

    public class KilnbornSentinel : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public KilnbornSentinel()
        {
            Name = "Kilnborn Sentinel";
            ApproachDescription = @"
The smell of burning metal fills your nostrils. Standing at the center of the room is what 
looks to be a human skeleton. In place of muscle and sinew it's bones are wrapped in charcoal-tinged 
clay, and long blades protrude from its arms. As the abomination turns to face you, you see that its 
ribcage is filled with some kind of strange machinery.

The creature stares at you, its eyeless sockets illuminated by an orange glow. Suddenly, it begins 
jerkily sprinting toward you, it's bony feet clacking against the stone floor.";
            CombatDescription = @"
The abomination circles you menacingly, it's bladed arms raised in 
a combat stance.";
            DefeatedDescription = @"

The creature suddenly freezes as the machinery in its chest 
grinds to a halt. The glow in its empty eyes fades as its body falls to the ground.";
            VictoriousDescription = @"

The creature's attack cuts deep. As you clutch your stomach, 
the last thing you see is a blade hurtling toward you.";
            Type = "purification";
            MaxHealth = 50;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Blade Strike", $"\n{this.Name} slashes at you with a bladed arm.", 5, 50),
               new Attack("Blade Spin", $"\n{this.Name} begins whirling toward you, striking wildly with its blades.", 10, 100)
            };
            DropItems = new List<Item>(){
                new BoneAsh(), new MetalCore(), new CrimsonOil()
            };
        }
    }

    public class CinderGolem : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public CinderGolem()
        {
            Name = "Cinder Golem";
            ApproachDescription = @"
Facing you is a hulking suit of black armor, robed in a cloud of thick black smoke. A dull orange 
glow shines from within the towering construct, and you can feel the heat it radiates even at a 
distance. As the smoke around it shifts, you see that it's carrying a massive halberd with a wickedly 
curved blade. You can see that its metal faceplate has been worked into a terrifying mockery of a 
human face, its expression somewhere between a rictus of pain and a malicious grin. With heavy, 
echoing footfall, the golem begins lumbering toward you.";
            CombatDescription = @"
The golem stands in a threatening stance, halberd at they ready.";
            DefeatedDescription = @"

The construct lets out a roar as red smoke spews forth from it's 
armor. The glow within it quickly fades, and it falls to the ground with a loud crash.";
            VictoriousDescription = @"

The golem's strike staggers you. As you struggle to regain your 
balance, it's halberd swings toward you in a lethal arc.";
            Type = "purification";
            MaxHealth = 75;
            Health = MaxHealth;
            MaxDefenseRating = 12;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Halberd Strike", $"\n{this.Name} swings at you with it's halberd.", 7, 50),
               new Attack("Incinerating Breath", $"\n{this.Name} spews a glowing cloud of flame in your direction.", 15, 100)
            };
            DropItems = new List<Item>(){
                new BoneAsh(), new MetalCore(), new CrimsonOil()
            };
        }
    }

    public class RavenousHusk : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public RavenousHusk()
        {
            Name = "Ravenous Husk";
            ApproachDescription = @"
You see a humanoid figure shambling toward you. In the dim light you can see that the creature is composed 
of some kind of sickening, dark yellow fungus. Black, fist-size nodules protrude from it's body at odd 
intervals. As the horror gets closer, you realize that its face is featureless aside from a gaping mouth 
lined with needle-like fang. Suddenly, the creature lets out horrific shriek and begins running directly 
toward you.";
            CombatDescription = @"
The horror dashes around erratically, all the while moving closer to your position.";
            DefeatedDescription = @"

The creature lets out a gurgling cry and falls to the ground, it's body slowly dissolving into a thick black ooze.";
            VictoriousDescription = @"

In your weakened state, the creature grabs you. It's terrifying maw fills your vision as it lets out a triumphant shriek.";
            Type = "corruption";
            MaxHealth = 40;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Claw Strike", $"\n{this.Name} swings at you with it's piercing claws.", 5, 50),
               new Attack("Vicious Bite", $"\n{this.Name} lurches toward you with it's horrific mouth open wide.", 15, 100)
            };
            DropItems = new List<Item>(){
                new AcridPowder(), new PutridNodule(), new YellowIchor()
            };
        }
    }

    public class WeepingHorror : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public WeepingHorror()
        {
            Name = "Weeping Horror";
            ApproachDescription = @"
You hear a strange, mournful sound, almost like a woman sobbing. And then, you see the sound's source. 
A mass of writhing, wormlike vines sits at the center of the room. As your eyes adjust to the dim light, 
you can make out a single, gigantic eye at the center of the mass. Pale green tears leak from around it, 
hitting the floor with a hissing sound as they eat away at the stone. You realize the eye is staring 
directly at you, unblinking, as the fould creature moves forward, constantly emitting it's hideous cries.";
            CombatDescription = @"
The terrifying beast moves closer, it's mournful cries echoing.";
            DefeatedDescription = @"

The creature lets out a final, desperate wail as it crumbles to the floor. It's single eye stares dully into space as 
it finally stops moving.";
            VictoriousDescription = @"

Weakened from your wounds, you fall forward. The creature grabs you with it's vinelike limbs. The last thing you 
hear is it's horrific wails.";
            Type = "corruption";
            MaxHealth = 65;
            Health = MaxHealth;
            MaxDefenseRating = 12;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Vine Lash", $"\n{this.Name} strikes at you with a hideous, barbed vine.", 10, 50),
               new Attack("Acidic Burst", $"\n{this.Name} launches a blob of corrosive liquid in your direction.", 15, 100)
            };
            DropItems = new List<Item>(){
                new AcridPowder(), new PutridNodule(), new YellowIchor()
            };
        }
    }

    public class ShiftingSlime : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public ShiftingSlime()
        {
            Name = "Shifting Slime";
            ApproachDescription = @"
You hear a wet, slithering sound, and notice a strange creature in the room with you. It looks to be a 
flowing pile of translucent, silvery liquid roiling in constant motion. As it draws closer, you see that 
hundreds of glowing violet lights dot it's surface. The slime suddenly rises up off of the ground, 
looking much like a serpent ready to strike, and you realize that the lights are actually eyes. 
Soundlessly, the creature throws itself toward you.";
            CombatDescription = @"
The slime shifts around eratically, moving almost too quickly to follow.";
            DefeatedDescription = @"

The creature explodes into gelatinous chunks that evaporate into nothingness before they even hit the ground.";
            VictoriousDescription = @"

Dazed and injured, you're unable to dodge as the creature lunges toward you, engulfing you completely.";
            Type = "transmutation";
            MaxHealth = 40;
            Health = MaxHealth;
            MaxDefenseRating = 12;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Gelatinous Strike", $"\n{this.Name} attacks you with a clublike pseudopod.", 5, 50),
               new Attack("Gelatinous Bombardment", $"\n{this.Name} launches a mass of pulsating silver ooze directly at you.", 10, 100)
            };
            DropItems = new List<Item>(){
                new LuminousDust(), new TwistedCrystal(), new QuiveringOoze()
            };
        }
    }

    public class FlickeringTerror : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public FlickeringTerror()
        {
            Name = "Flickering Terror";
            ApproachDescription = @"
You feel a sudden, terrifying feeling that you're being watched. Suddenly, a strange figure appears 
in the center of the room, only to vanish almost immediately. You can just barely discern that 
it's some kind of spindly, many-limbed humanoid figure. Suddenly, it appears again, but closer. 
This time, it lingers for a second before disappearing. It resembles a gigantic insect with an 
oddly geometric, mirrored carapace. When it appears for the third time, it's mere feet away, 
and you can see the wicked spines protruding from its limbs.";
            CombatDescription = @"
The insectoid monstrosity blinks in and out of visibility as it darts around the room.";
            DefeatedDescription = @"

The creature lets out a surprised shriek, then suddenly explodes, sending glassy bits of shell in every direction.";
            VictoriousDescription = @"

In a moment of distraction, you lose sight of the creature completely. You feel a stinging pain in 
your back before your vision goes black.";
            Type = "transmutation";
            MaxHealth = 70;
            Health = MaxHealth;
            MaxDefenseRating = 14;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Stinger Strike", $"\n{this.Name} thrusts one of it's spine-tipped limbs at you.", 7, 75),
               new Attack("Warp Strike", $"\n{this.Name} suddenly vanishes, only to reappear directly behind you, slashing furiously with it's spiny limbs.", 20, 100)
            };
            DropItems = new List<Item>(){
                new LuminousDust(), new TwistedCrystal(), new QuiveringOoze()
            };
        }
    }

    public class AvatarOfRage : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public AvatarOfRage()
        {
            Name = "Avatar of Rage";
            ApproachDescription = @"
Aldric suddenly bursts into flames, leaving behind a massive pile of ash. And then a figure rises from the still-warm dust. It's
vaugely humanoid, but it's body is covered in wickedly spined armor that looks to be crafted from scorched bone. It's draconic skull 
resembles something out of a myth, and it gazes at you with glowing orange eyes.

The creature slowly kneels to ground, and removes a massive sword of iron and bone from the pile of ash. Flames begin to arise from
the demonic figure and it's blade. It lets out a roar, then begins to move quickly toward you.";
            CombatDescription = @"
The skeletal creature circles you, it's flaming sword at the ready.";
            DefeatedDescription = @"

The creature lets out a horrifc roar. It's body begins to slowly crumble into ash and cinder.

As the remains of the monster slowly burn away, you hear a voice fill the room.

Voice: 'The Fire...the cleansing, glorious Fire...so beautiful...'

";
            VictoriousDescription = @"

The creature lets out a triumphant roar as your body is consumed by flames.";
            Type = "purification";
            MaxHealth = 200;
            Health = MaxHealth;
            MaxDefenseRating = 12;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Burning Strike", $"\n{this.Name} slashes at you with its sword of flame.", 10, 75),
               new Attack("Eyes of Wrath", $"\n{this.Name} emits two beams of glowing orange light from it's eyes.", 20, 100)
            };
            DropItems = new List<Item>(){
                new IconOfRage()
            };
        }
    }

    public class AvatarOfDespair : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public AvatarOfDespair()
        {
            Name = "Avatar of Despair";
            ApproachDescription = @"
A cloud of green fog surrounds Miranda, hiding her from view. As the fog fades, you see that she's been transformed into
a terrifying creature. It's little more than a vaguely humanoid mass of black thorns. It's misshapen head gazes at you
with three dull, yellow eyes that seem to be leaking a thick, yellow fluid that sends up thin trails of smoke as it
hit the ground.

The creature suddenly lets out an inhuman wail. Then, with a jerky, unnatural gait, it advances slowly toward you.";
            CombatDescription = @"
The thorned abomination lurches toward, weeping acidic tears.";
            DefeatedDescription = @"

The creature lets out a terrifying cry, then falls to the ground. It's body slowly dissolves, giving off a pale
green smoke.

As the remains of the monster melt away, you hear a voice fill the room.

Voice: 'Vivi...'

";
            VictoriousDescription = @"

The creature lets out a gurgling laugh as your body begins to dissolve.";
            Type = "corruption";
            MaxHealth = 175;
            Health = MaxHealth;
            MaxDefenseRating = 15;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Thorn Strike", $"\n{this.Name} tears at you with its thorn-like claws.", 8, 80),
               new Attack("Corrosive Cloud", $"\nA cloud of corrosive, pale green vapor emanates from {this.Name}.", 25, 100)
            };
            DropItems = new List<Item>(){
                new IconOfDespair()
            };
        }
    }

    public class AvatarOfMadness : Enemy
    {
        public override string Name { get; set; }
        public override string ApproachDescription { get; set; }
        public override string CombatDescription { get; set; }
        public override string DefeatedDescription { get; set; }
        public override string VictoriousDescription { get; set; }
        public override string Type { get; set; }
        public override double Health { get; set; }
        public override double MaxHealth { get; set; }
        public override double DefenseRating { get; set; }
        public override double MaxDefenseRating { get; set; }
        public override List<Attack> Attacks { get; set; }
        public override List<Item> DropItems { get; set; }

        public AvatarOfMadness()
        {
            Name = "Avatar of Madness";
            ApproachDescription = @"
Theodore is suddenly consumed by a flash of bluish light. As the light fades, an unnatural creature hovers in his place.
It seems to be a collection of irregularlly shaped, crystalline forms orbiting a glowing core of glowing blue gas. A strange
electricity crackles around it.

The eldritch horror begins floating slowly toward you, the air around it rippling as it moves.";
            CombatDescription = @"
The unnatural creature hovers in place, distorting the area around it with a strange light.";
            DefeatedDescription = @"

The core at the center of the creature suddenly turns pure black. The crystalline masses orbiting it
are suddenly drawn into it's now-dark center. And then, with a flash of blue light, the feature vanishes.

You hear a voice fill the room.

Voice: 'Ah, so this is all there is? How disappointing...'

";
            VictoriousDescription = @"
            
The creature's core flashes almost mockingly as your body fades into nothingness.'";
            Type = "transmutation";
            MaxHealth = 150;
            Health = MaxHealth;
            MaxDefenseRating = 16;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Crackling Burst", $"\nA burst of electricity arcs out of {this.Name}.", 10, 60),
               new Attack("Crystal Bombardment", $"\n{this.Name} launches a torrent of jagged crystal shards directly at you.", 20, 100)
            };
            DropItems = new List<Item>(){
                new IconOfMadness()
            };
        }
    }

}