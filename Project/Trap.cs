using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Trap
    {
        public virtual string Name { get; set; }
        public virtual string TriggeredText { get; set; }
        public virtual string DisabledText { get; set; }
        public virtual string SuccessText { get; set; }
        public virtual string FailureText { get; set; }
        public virtual double Damage { get; set; }
        public bool Active { get; set; }

        public Trap()
        {
            Name = "Trap";
            Damage = 0;
            TriggeredText = "";
            DisabledText = "";
            SuccessText = "";
            FailureText = "";
            Active = true;
        }
    }

    public class FlameTrap : Trap
    {
        public override string Name { get; set; }
        public override string TriggeredText { get; set; }
        public override string DisabledText { get; set; }
        public override string SuccessText { get; set; }
        public override string FailureText { get; set; }
        public override double Damage { get; set; }

        public FlameTrap()
        {
            Name = "Flame Trap";
            Damage = 20;
            TriggeredText = "Suddenly, jets of fire spew from carefully concealed openings in the walls and ceiling of the room.\n";
            DisabledText = $"The {Name}, thankfully, is still disabled.";
            SuccessText = $"You find yourself caught by a blast of flame, and take {Damage} damage!";
            FailureText = $"You successfully deactivate the {Name}.";
            
        }
    }
    public class AcidTrap : Trap
    {
        public override string Name { get; set; }
        public override string TriggeredText { get; set; }
        public override string DisabledText { get; set; }
        public override string SuccessText { get; set; }
        public override string FailureText { get; set; }
        public override double Damage { get; set; }

        public AcidTrap()
        {
            Name = "Acid Trap";
            Damage = 20;
            TriggeredText = "Suddenly, droplets of corrosive liquid egin to leak from the ceiling.\n";
            DisabledText = $"The {Name}, thankfully, is still disabled.";
            SuccessText = $"The acid lands on you, dealing {Damage} damage!";
            FailureText = $"You successfully deactivate the {Name}.";
            
        }
    }

    public class RiftTrap : Trap
    {
        public override string Name { get; set; }
        public override string TriggeredText { get; set; }
        public override string DisabledText { get; set; }
        public override string SuccessText { get; set; }
        public override string FailureText { get; set; }
        public override double Damage { get; set; }

        public RiftTrap()
        {
            Name = "Rift Trap";
            Damage = 20;
            TriggeredText = "Suddenly, a glowing rift appears in the air at the center of the room, emitting a sickening violet light.\n";
            DisabledText = $"The {Name}, thankfully, is still disabled.";
            SuccessText = $"The light from the rift strikes you, dealing {Damage} damage!";
            FailureText = $"You successfully deactivate the {Name}.";
            
        }
    }
}
