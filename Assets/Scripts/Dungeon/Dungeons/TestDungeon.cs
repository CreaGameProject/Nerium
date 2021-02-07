using System.Collections.Generic;
using Characters;
using Dungeon;
using Characters;
using GridMap;
using UnityEngine;

namespace Dungeon.Dungeons
{
    public class TestDungeon :Dungeon
    {
        public override string Name => "test dungeon";
        public override int MaxFloorNum => 1;
        public override int ItemLimit => 20;

        public override Floor MakeFloor(int floorNum, Player player)
        {
            var floor = new Floor(1, 300, floorNum, 500, player.gameObject, new Vector2Int(120, 90));
            floor.AddRoom(2,40, 2, 40);
            floor.AddRoom(50, 100, 5, 60);
            // floor.SetTerrain(new Vector2Int(40, 20), new Vector2Int(50, 20), TerrainType.Floor);
            floor.SetTerrain(40, 50, 20, 20, TerrainType.Floor);
            player.Position = new Vector2Int(2,2);
            return floor;
        }
    }
}
