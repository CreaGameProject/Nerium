using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters;
using Assets.Scripts.Dungeon;
using Assets.Scripts.Systems;
using GridMap;
using UnityEngine;

public class TestDungeon : Dungeon
{
    public TestDungeon()
    {
        Name = "test dungeon";
        MaxFloorNum = 1;
    }

    public override Floor MakeFloor(int floorNum, Player player)
    {
        var range = new Vector2Int(15, 10);
        var terrain = new GridMap<TerrainType>(range,
            (c, r) => c == 0 || r == 0 || c == 14 || r == 9 ? TerrainType.Wall : TerrainType.Floor).Matrix;
        var floor = new Floor(floorNum, 500, terrain, new List<Room>(), player);
        player.Position = new Vector2Int(1,1);
        return floor;
    }
}
