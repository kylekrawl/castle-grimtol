using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Item : IItem
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string DamageType { get; set; }
        public virtual double Damage { get; set; }
        public virtual double HealAmount { get; set; }
        public virtual List<List<string>> CraftingCombinations { get; set; }

        public Item()
        {
            Name = "Item";
            Description = "";
            DamageType = "";
            Damage = 0;
            HealAmount = 0;
            CraftingCombinations = new List<List<string>>();
        }
    }

    public class FlintlockPistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public FlintlockPistol()
        {
            Name = "Flintlock Pistol";
            Description = "A basic sidearm.";
            DamageType = "basic";
            Damage = 5;
        }
    }

    public class DivinePistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public DivinePistol()
        {
            Name = "Divine Pistol";
            Description = "A sacred relic capable of great destruction.";
            DamageType = "basic";
            Damage = 50;
        }
    }

    public class IncendiaryPistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public IncendiaryPistol()
        {
            Name = "Incendiary Pistol";
            Description = "A pistol capable of firing incendiary rounds.";
            DamageType = "burning";
            Damage = 7;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Flintlock Pistol", "Bone Ash"},
                new List<string>(){ "Venomous Pistol", "Bone Ash"},
                new List<string>(){ "Warp Pistol", "Bone Ash"}
            };
        }
    }

    public class VenomousPistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public VenomousPistol()
        {
            Name = "Venomous Pistol";
            Description = "A pistol capable of firing corrosive rounds.";
            DamageType = "corrosive";
            Damage = 7;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Flintlock Pistol", "Acrid Powder"},
                new List<string>(){ "Incendiary Pistol", "Acrid Powder"},
                new List<string>(){ "Warp Pistol", "Acrid Powder"}
            };
        }
    }

    public class WarpPistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public WarpPistol()
        {
            Name = "Warp Pistol";
            Description = "A pistol capable of firing chaotic rounds.";
            DamageType = "chaos";
            Damage = 7;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Flintlock Pistol", "Luminous Dust"},
                new List<string>(){ "Incendiary Pistol", "Luminous Dust"},
                new List<string>(){ "Venomous Pistol", "Luminous Dust"}
            };
        }
    }

    public class IncendiaryGrenade : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public IncendiaryGrenade()
        {
            Name = "Incendiary Grenade";
            Description = "An incendiary explosive projectile.";
            DamageType = "burning";
            Damage = 10;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Bone Ash"}
            };
        }
    }

    public class VenomousGrenade : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public VenomousGrenade()
        {
            Name = "Venomous Grenade";
            Description = "An explosive projectile with an acidic payload.";
            DamageType = "corrosive";
            Damage = 10;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Acrid Powder"}
            };
        }
    }

    public class WarpGrenade : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override double Damage { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public WarpGrenade()
        {
            Name = "Warp Grenade";
            Description = "An explosive projectile capable of destabilizing matter.";
            DamageType = "chaos";
            Damage = 10;
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Luminous Dust"}
            };
        }
    }

    public class YellowIchor : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public YellowIchor()
        {
            Name = "Yellow Ichor";
            Description = "[Alchemical Fluid] A viscous amber liquid. Slightly acidic.";
        }
    }

    public class PutridNodule : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public PutridNodule()
        {
            Name = "Putrid Nodule";
            Description = "[Alchemical Substrate] A dark chitinous lump covered in slime.";
        }
    }

    public class AcridPowder : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public AcridPowder()
        {
            Name = "Acrid Powder";
            Description = "[Alchemical Powder] A fine yellow-green powder with a strong sulfurous odor.";
        }
    }

    public class CrimsonOil : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public CrimsonOil()
        {
            Name = "Crimson Oil";
            Description = "[Alchemical Fluid] A thin liquid that looks disturbingly similar to blood.";
        }
    }

    public class MetalCore : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public MetalCore()
        {
            Name = "Metal Core";
            Description = "[Alchemical Substrate] A dense sphere of gleaming white metal. It gives off a faint warmth.";
        }
    }

    public class BoneAsh : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public BoneAsh()
        {
            Name = "Bone Ash";
            Description = "[Alchemical Powder] A fine ash derived from incinerated bones.";
        }
    }

    public class QuiveringOoze : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public QuiveringOoze()
        {
            Name = "Quivering Ooze";
            Description = "[Alchemical Fluid] A shimmering silver liquid that seems to be in constant motion.";
        }
    }

    public class TwistedCrystal : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public TwistedCrystal()
        {
            Name = "Twisted Crystal";
            Description = "[Alchemical Substrate] A faintly glowing crystal warped into a shape that defies explanation.";
        }
    }

    public class LuminousDust : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public LuminousDust()
        {
            Name = "Luminous Dust";
            Description = "[Alchemical Powder] A violet powder that pulsates with a strange light.";
        }
    }

    public class PulseCrystal : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public PulseCrystal()
        {
            Name = "Pulse Crystal";
            Description = "A small, clear crystal. It emits a weak electrical field.";
        }
    }

    public class ReactiveSolid : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public ReactiveSolid()
        {
            Name = "Reactive Solid";
            Description = "[Advanced Alchemical Substrate] A highly volatile alchemical substrate. Useful for crafting more advanced recipes.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Putrid Nodule", "Metal Core"},
                new List<string>(){ "Putrid Nodule", "Twisted Crytal"},
                new List<string>(){ "Twisted Crystal", "Metal Core"}
            };
        }
    }

    public class MoltenExtract : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public MoltenExtract()
        {
            Name = "Molten Extract";
            Description = "A dimly glowing red-orange liquid that emits a faint heat.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Crimson Oil"}
            };
        }
    }

    public class ScarletMarrow : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public ScarletMarrow()
        {
            Name = "Scarlet Marrow";
            Description = "A soft, bright red paste that gives off a strange steam.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Bone Dust"}
            };
        }
    }

    public class PulseEmitter : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public PulseEmitter()
        {
            Name = "Pulse Emitter";
            Description = "A device capable of deactivating mechanical traps.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Pulse Crystal"}
            };
        }
    }

    public class HealingElixir : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public override double HealAmount { get; set; }
        public HealingElixir()
        {
            Name = "Healing Elixir";
            Description = "A small vial of restorative liquid.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Yellow Ichor", "Crimson Oil"},
                new List<string>(){ "Yellow Ichor", "Quivering Ooze"},
                new List<string>(){ "Quivering Ooze", "Crimson Oil"}
            };
            HealAmount = 10;
        }
    }

    public class MedicinalSalve : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public override double HealAmount { get; set; }
        public MedicinalSalve()
        {
            Name = "Medicinal Salve";
            Description = "A thick paste with healing properties.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Crimson Oil"},
                new List<string>(){ "Yellow Ichor", "Reactive Solid"},
                new List<string>(){ "Quivering Ooze", "Reactive Solid"}
            };
            HealAmount = 20;
        }
    }

    public class Panacea : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public override double HealAmount { get; set; }
        public Panacea()
        {
            Name = "Panacea";
            Description = "A draught to cure almost any wound or ailment.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Healing Elixir"}
            };
            HealAmount = 30;
        }
    }

    public class AlchemicalResidue : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public AlchemicalResidue()
        {
            Name = "Alchemical Residue";
            Description = "The byproduct of a high-temperature alchemical reaction. Possibly still reactive if combined with the right component.";
        }
    }

    public class EnergeticCatalyst : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public EnergeticCatalyst()
        {
            Name = "Energetic Catalyst";
            Description = "A vial of bright yellow-orange liquid. Can be combined with spent alchemical fuel to produce a new power source.";
        }
    }

    public class FuelOrb : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public FuelOrb()
        {
            Name = "Fuel Orb";
            Description = "A smooth, spherical solid emitting a faint orange glow. Can be used by certain alchemical machines as a power source.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Energetic Catalyst", "Alchemical Residue"}
            };
        }
    }

    public class WoodenEffigy : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public WoodenEffigy()
        {
            Name = "Wooden Effigy";
            Description = "A chunk of wood carved into a vaguely human figure. It's been lightly singed in a few places.";
        }
    }

    public class SteelKey : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public SteelKey()
        {
            Name = "Steel Key";
            Description = "A heavy key made from polished steel.";
        }
    }

    public class SunCrest : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public SunCrest()
        {
            Name = "Sun Crest";
            Description = "A slightly tarnished bronze crest shaped like an eight-pointed star. A single, orange gem has been inset at each of the points, with a larger gem occupying the center.";
        }
    }

    public class BrazierLid : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public BrazierLid()
        {
            Name = "Brazier Lid";
            Description = "A mechanical lid meant to be placed on top of a brazier. Tiny fans line the inside. It seems to be designed to efficiently extinguish flames";
        }
    }

    public class ArcaneFuse : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ArcaneFuse()
        {
            Name = "Arcane Fuse";
            Description = "A copper cylinder etched with black, geometric runes. It seems to be emitting a faint electrical field.";
        }
    }

    public class InfernalElixir : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public InfernalElixir()
        {
            Name = "Infernal Elixir";
            Description = "A vial of glowing red liquid. Its temperature seems to alternate between hot and cold.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Scarlet Marrow", "Molten Extract"}
            };
        }
    }

    public class AnchorOfPurification : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public AnchorOfPurification()
        {
            Name = "Anchor Of Purification";
            Description = "A crystal orb glowing with a shifting pattern of red, yellow and orange light. It radiates an aura of incredible power.";
        }
    }

    public class IconOfRage : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public IconOfRage()
        {
            Name = "Icon of Rage";
            Description = "A small, cystalline cube filled with a crackling orange light. While holding it, you can sense an aura of intense hatred.";
        }
    }

    public class MisshapenSkull : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public MisshapenSkull()
        {
            Name = "Misshapen Skull";
            Description = "A badly deformed skull of unknown origin.";
        }
    }

    public class SurgicalKnife : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public SurgicalKnife()
        {
            Name = "Surgical Knife";
            Description = "A small knife with a razor-sharp edge.";
        }
    }

    public class MalformedHeart : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public MalformedHeart()
        {
            Name = "Malformed Heart";
            Description = "An organ resembling a human heart, but the shape is horrily wrong.";
        }
    }

    public class ThornGear : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ThornGear()
        {
            Name = "Thorn Gear";
            Description = "A wought-iron gear formed by an elaborate arrangement of precisely welded metal rods decorated to look like vines and branches.";
        }
    }

    public class GlassEye : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public GlassEye()
        {
            Name = "Glass Eye";
            Description = "A faithful recreation of a human eye in colored glass. It was clearly designed by a skilled artist.";
        }
    }

    public class BlackKey : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public BlackKey()
        {
            Name = "Black Key";
            Description = "A small, dark metal key decorated with a plant motif.";
        }
    }

    public class LethalVenom : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public LethalVenom()
        {
            Name = "Lethal Venom";
            Description = "A vial of thin, black liquid. You feel uncomfortable even holding it.";
        }
    }

    public class ViviansCharm : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ViviansCharm()
        {
            Name = "Vivian's Charm";
            Description = "A small, silver locket. Inside is a small scrap of parchment with a series of cuneiform letters. On the back is an inscription: 'For my precious Vivi. I will always protect you.'";
        }
    }

    public class AnchorOfCorruption : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public AnchorOfCorruption()
        {
            Name = "Anchor Of Corruption";
            Description = "A crystal orb glowing with a shifting pattern of green light and impossibly dark shadows. It radiates an aura of incredible power.";
        }
    }

    public class IconOfDespair : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public IconOfDespair()
        {
            Name = "Icon of Despair";
            Description = "A small, cystalline cube filled with a crackling green light. While holding it, you can sense an aura of deep sadness.";
        }
    }

    public class PrismaticDust : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override List<List<string>> CraftingCombinations { get; set; }
        public PrismaticDust()
        {
            Name = "Prismatic Dust";
            Description = "A glassy, coarse-grained powder. A strange mix of light and shadow seem to dance within each particle.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){"Acrid Powder", "Bone Dust"}
            };
        }
    }

    public class UnnaturalPrism : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public UnnaturalPrism()
        {
            Name = "Unnatural Prism";
            Description = "A crystal with perfectly smooth surfaces. Light passing through it seems to behave oddly.";
        }
    }

    public class OpticalDisruptor : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public OpticalDisruptor()
        {
            Name = "Optical Disruptor";
            Description = "A small handheld device with a dazzling array of lenses and a single switch.";
        }
    }

    public class ShimmeringGem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ShimmeringGem()
        {
            Name = "Shimmering Gem";
            Description = "An elaborately carved gemstone. It seems to be a slightly different color each time you look at it.";
        }
    }

    public class IridescentFluid : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public IridescentFluid()
        {
            Name = "Iridescent Fluid";
            Description = "A vial of impossibly bright silver liquid.";
        }
    }

    public class GlowingVapor : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public GlowingVapor()
        {
            Name = "Glowing Vapor";
            Description = "A corked bottle filled with a faintly glowing bluish gas.";
        }
    }

    public class AnchorOfTransmutation : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public AnchorOfTransmutation()
        {
            Name = "Anchor Of Transmutation";
            Description = "A crystal orb glowing with a shifting pattern of blue and violet light. It radiates an aura of incredible power.";
        }
    }

    public class IconOfMadness : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public IconOfMadness()
        {
            Name = "Icon of Madness";
            Description = "A small, cystalline cube filled with a crackling violet light. While holding it, you can sense an aura of unhinged insanity.";
        }
    }

    public class ImpossibleAnchor : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ImpossibleAnchor()
        {
            Name = "Impossible Anchor";
            Description = "A crystal orb glowing with an intense white light. It feels as if it could tear apart the very fabric of reality.";
        }
    }
}
