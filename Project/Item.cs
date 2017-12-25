using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Item : IItem
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string DamageType { get; set; }
        public virtual int Damage { get; set; }
        public virtual int HealAmount { get; set; }
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
        public override int Damage { get; set; }
        public FlintlockPistol()
        {
            Name = "Flintlock Pistol";
            Description = "A basic sidearm.";
            DamageType = "basic";
            Damage = 5;
        }
    }

    public class IncendiaryPistol : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override string DamageType { get; set; }
        public override int Damage { get; set; }
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
        public override int Damage { get; set; }
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
        public override int Damage { get; set; }
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
        public override int Damage { get; set; }
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
        public override int Damage { get; set; }
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
        public override int Damage { get; set; }
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
            Description = "A highly volatile alchemical substrate. Useful for crafting more advanced recipes.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Putrid Nodule", "Metal Core"},
                new List<string>(){ "Putrid Nodule", "Twisted Crytal"},
                new List<string>(){ "Twisted Crystal", "Metal Core"}
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
        public override int HealAmount { get; set; }
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
        public override int HealAmount { get; set; }
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
        public override int HealAmount { get; set; }
        public Panacea()
        {
            Name = "Panacea";
            Description = "A draught to cure almost any wound or ailment.";
            CraftingCombinations = new List<List<string>>(){
                new List<string>(){ "Reactive Solid", "Healing Elixir"},
            };
            HealAmount = 30;
        }
    }
}
