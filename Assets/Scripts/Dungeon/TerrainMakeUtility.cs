using GridMap;
using UnityEngine;

namespace Dungeon.Dungeons
{
    public static class TerrainMakeUtility
    {
        public static void SetTerrain(this GridMap<TerrainType> map, Vector2Int startPoint, Vector2Int endPoint, TerrainType terrain)
        {
            for (int x = startPoint.x; x <= endPoint.x; x++)
            for (int y = startPoint.y; y <= endPoint.y; y++)
                map[x, y] = terrain;
        }

        public static void SetRoomTerrain(this Room room)
        {
            room.Floor.Terrains.SetTerrain(room.StartPoint, room.EndPoint, TerrainType.Floor);
        }
    }
}