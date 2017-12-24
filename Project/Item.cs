using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public abstract class Item : IItem
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public Item()
        {
            Name = "Item";
            Description = "";
        }
    }

    public class TestItem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }

        public TestItem()
        {
            Name = "Test Item";
            Description = "A test item.";
        }
    }

    public class Weapon : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public int DamageType { get; set; }
        public int Damage { get; set; }
        public List<List<string>> CraftingCombinations {get; set;}
        public Weapon()
        {
            Name = "Weapon";
            Description = "";
        }
    }

    public class MaterialItem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public List<List<string>> CraftingCombinations {get; set;}
        public MaterialItem()
        {
            Name = "Material Item";
            Description = "";
        }
    }

    public class QuestItem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }

        public List<List<string>> CraftingCombinations {get; set;}

        public QuestItem()
        {
            Name = "Quest Item";
            Description = "";
        }
    }

    public class HealingItem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public List<Item> Components { get; set; }
        public int HealAmount { get; set; }
        public int NumUses { get; set; }
        public List<List<string>> CraftingCombinations {get; set;}
        public HealingItem()
        {
            Name = "Healing Item";
            Description = "";
        }
    }

    public class OffensiveItem : Item
    {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public List<Item> Components { get; set; }
        public string DamageType { get; set; }
        public int Damage { get; set; }
        public int NumUses { get; set; }
        public List<List<string>> CraftingCombinations {get; set;}
        public OffensiveItem()
        {
            Name = "Offensive Item";
            Description = "";
        }
    }
}
