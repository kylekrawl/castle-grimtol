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
looks to be a human skeleton. In place of muscle and sinew it's bones are wrapped in charcoal-tinged clay, and long blades 
protrude from its arms. As the abomination turns to face you, you see that its ribcage is filled with some kind of strange machinery.
The creature stares at you, its eyeless sockets illuminated by an orange glow. Suddenly, it begins jerkily sprinting 
toward you, it's bony feet clacking against the stone floor.";
            CombatDescription = @"The abomination circles you menacingly, it's bladed arms raised in a combat stance.";
            DefeatedDescription = @"The creature suddenly freezes as the machinery in its chest grinds to a halt. The glow 
in its empty eyes fades as its body falls to the ground.";
            VictoriousDescription = @"The creature's attack cuts deep. As you clutch your stomach, the last thing you see 
is a blade hurtling toward you.";
            Type = "purification";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Blade Strike", $"\n{this.Name} slashes at you with a bladed arm.", 10, 50),
               new Attack("Blade Spin", $"\n{this.Name} begins whirling toward you, striking wildly with its blades.", 20, 100)
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
Facing you is a hulking suit of black armor, robed in a cloud of thick black smoke. A dull orange glow shines from within the 
towering construct, and you can feel the heat it radiates even at a distance. As the smoke around it shifts, you see that it's 
carrying a massive halberd with a wickedly curved blade. You can see that its metal faceplate has been worked into a terrifying 
mockery of a human face, it's expression somewhere between a rictus of pain and a malicious grin. With heavy, echoing footfall, 
the golem begins lumbbering toward you.";
            CombatDescription = @"The golem stands in a threatening stance, halberd at they ready.";
            DefeatedDescription = @"The construct lets out a roar as red smoke spews forth from it's armor. The glow within it 
quickly fades, and it falls to the ground with a loud crash.";
            VictoriousDescription = @"The golem's strike staggers you. As you struggle to regain your balance, it's halberd swings 
toward you in a lethal arc.";
            Type = "purification";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Halberd Strike", $"\n{this.Name} swings at you with it's halberd.", 15, 50),
               new Attack("Incinerating Breath", $"\n{this.Name} spews a glowing cloud of flame in your direction.", 25, 100)
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
You see a humanoid figure shambling toward you. In the dim light you can see that the creature is composed of some kind of sickening, dark yellow
fungus. Black, fist-size nodules protrude from it's body at odd intervals. As the horror gets closer, you realize that its face is featureless aside
from a gaping mouth lined with needle-like fang. Suddenly, the creature lets out horrific shriek and begins running directly toward you.";
            CombatDescription = @"The horror dashes around erratically, all the while moving closer to your position.";
            DefeatedDescription = @"The creature lets out a gurgling cry and falls to the ground, it's body slowly dissolving into a thick black ooze.";
            VictoriousDescription = @"In your weakened state, the creature grabs you. It's terrifying maw fills your vision as it lets out a triumphant shriek.";
            Type = "corruption";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Claw Strike", $"\n{this.Name} swings at you with it's piercing claws.", 10, 50),
               new Attack("Vicious Bite", $"\n{this.Name} lurches toward you with it's horrific mouth open wide.", 20, 100)
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
You hear a strange, mournful sound, almost like a woman sobbing. And then, you see the sound's source. A mass of writhing, wormlike vines sits at the center 
of the room. As your eyes adjust to the dim light, you can make out a single, gigantic eye at the center of the mass. Pale green tears leak from around it, 
hitting the floor with a hissing sound as they eat away at the stone. You realize the eye is staring directly at you, unblinking, as the fould creature moves 
forward, constantly emitting it's hideous cries.";
            CombatDescription = @"The terrifying beast moves closer, it's mournful cries echoing.";
            DefeatedDescription = @"The creature lets out a final, desperate wail as it crumbles to the floor. It's single eye stares dully into space as 
it finally stops moving.";
            VictoriousDescription = @"Weakened from your wounds, you fall forward. The creature grabs you with it's vinelike limbs. The last thing you 
hear is it's horrific wails.";
            Type = "corruption";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Vine Lash", $"\n{this.Name} strikes at you with a hideous, barbed vine.", 15, 50),
               new Attack("Acidic Burst", $"\n{this.Name} launches a blob of corrosive liquid in your direction.", 25, 100)
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
You hear a wet, slithering sound, and notice a strange creature in the room with you. It looks to be a flowing pile of translucent, silvery liquid 
roiling in constant motion. As it draws closer, you see that hundreds of glowing violet lights dot it's surface. The slime suddenly rises up off of the ground, 
looking much like a serpent ready to strike, and you realize that the lights are actually eyes. Soundlessly, the creature throws itself toward you.";
            CombatDescription = @"The slime shifts around eratically, moving almost too quickly to follow.";
            DefeatedDescription = @"The creature explodes into gelatinous chunks that evaporate into nothingness before they even hit the ground.";
            VictoriousDescription = @"Dazed and injured, you're unable to dodge as the creature lunges toward you, engulfing you completely.";
            Type = "transmutation";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Gelatinous Strike", $"\n{this.Name} attacks you with a clublike pseudopod.", 10, 50),
               new Attack("Gelatinous Bombardment", $"\n{this.Name} launches a mass of pulsating silver ooze directly at you.", 20, 100)
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
You feel a sudden, terrifying feeling that you're being watched. Suddenly, a strange figure appears in the center of the room, only to vanish 
almost immediately. You can just barely discern that it's some kind of spindly, many-limbed humanoid figure. Suddenly, it appears again, but closer. 
This time, it lingers for a second before disappearing. It resembles a gigantic insect with an oddly geometric, mirrored carapace. When it appears for
the third time, it's mere feet away, and you can see the wicked spines protruding from its limbs.";
            CombatDescription = @"The insectoid monstrosity blinks in and out of visibility as it darts around the room.";
            DefeatedDescription = @"The creature lets out a surprised shriek, then suddenly explodes, sending glassy bits of shell in every direction.";
            VictoriousDescription = @"In a moment of distraction, you lose sight of the creature completely. You feel a stinging pain in 
your back before your vision goes black.";
            Type = "transmutation";
            MaxHealth = 20;
            Health = MaxHealth;
            MaxDefenseRating = 10;
            DefenseRating = MaxDefenseRating;
            Attacks = new List<Attack>()
            {
               new Attack("Stinger Strike", $"\n{this.Name} thrusts one of it's spine-tipped limbs at you.", 15, 50),
               new Attack("Warp Strike", $"\n{this.Name} suddenly vanishes, only to reappear directly behind you, slashing furiously with it's spiny limbs.", 25, 100)
            };
            DropItems = new List<Item>(){
                new LuminousDust(), new TwistedCrystal(), new QuiveringOoze()
            };
        }
    }

}