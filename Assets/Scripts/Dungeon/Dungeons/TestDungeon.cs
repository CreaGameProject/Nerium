using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters;
using Assets.Scripts.Dungeon;
using GridMap;
using UnityEngine;

public class TestDungeon : Dungeon
{
    public TestDungeon()
    {
        Name = "test dungeon";
    }

    public override Floor MakeFloor(int floorNum)
    {
        var range = new Vector2Int(15, 10);
        var terrain = new GridMap<TerrainType>(range,
            (c, r) => c * r != 0 && c != 15 && r != 10 ? TerrainType.Floor : TerrainType.Wall).Matrix;
        var floor = new Floor(floorNum, terrain, new List<Room>(), new Player());
        return floor;
    }
}
