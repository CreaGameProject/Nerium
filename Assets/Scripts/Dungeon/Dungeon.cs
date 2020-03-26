using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dungeon
{
    public abstract class Dungeon
    {
        public string Name { get; }

        public int MaxFloorNum { get; }

        public abstract Floor MakeFloor(int floorNum);


    }
}
