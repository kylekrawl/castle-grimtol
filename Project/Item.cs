using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Item : IItem
    {
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
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