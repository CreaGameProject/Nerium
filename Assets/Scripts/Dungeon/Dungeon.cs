using System.Collections.Generic;
using Characters;
using GridMap;
using UnityEngine;

namespace Dungeon
{
    public abstract class Dungeon
    {
        public abstract string Name { get; }
        public abstract int MaxFloorNum { get; }
        public abstract int ItemLimit { get; }
        
        public abstract Floor MakeFloor(int floorNum, Player player);
    }
}
