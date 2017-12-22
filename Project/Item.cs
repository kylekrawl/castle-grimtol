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
}





/*
Items:
- Weapons:
    - for player and enemies (unlimited use)
- Materials:
    - can be combined with other items to modify item or create new item
- Protective:
    - bestow beneficial effects on user
- Offensive:
    - damage enemy, but limited use
- Quest:
    - used to advance game progression
 */