using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dungeon
{
    public abstract class Dungeon
    {
        public string Name { get; protected set; }

        public int MaxFloorNum { get; protected set; }

        public abstract Floor MakeFloor(int floorNum);


    }
}
