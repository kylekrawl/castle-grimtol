using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Attack
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public double Frequency { get; set; }

        public Attack(string name, string description, double damage, double frequency)
        {
            Name = name;
            Description = description;
            Damage = damage;
            Frequency = frequency;
        }
    }
}
