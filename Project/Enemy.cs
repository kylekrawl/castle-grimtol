using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Enemy
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int DefenseRating { get; set; }
        public int MaxDefenseRating { get; set; }
        public int BaseDamage { get; set; }
        public string Type { get; set; }
        public List<Item> DropItems { get; set; }

        public virtual void DropItem()
        {
            throw new System.NotImplementedException();
        }

        public Enemy()
        {

        }
    }
}