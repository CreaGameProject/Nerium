using System.Collections.Generic;
using Characters;
using GridMap;
using UnityEngine;

namespace Dungeon.Dungeons
{
    public class RandomDungeon : Dungeon
    {
        private int floorSizeX = 400;
        private int floorSizeY = 300;
        
        private int maxRoomNum = 10;
        private int minRoomNum = 2;
        private int minSectionSizeX = 5;
        private int minSectionSizeY = 5;
        private int minRoomSizeX = 3;
        private int minRoomSizeY = 3;
        private int minPadding = 1;

        public override string Name => "Random";
        public override int MaxFloorNum => 1;
        public override int ItemLimit => 20;

        public override Floor MakeFloor(int floorNum, Player player)
        {
            var terrain = new UnConvertibleMap<TerrainType>(new TerrainType[floorSizeX, floorSizeY]);
            var rooms = new List<Room>();
            var roomNum = Random.Range(minRoomNum, maxRoomNum + 1);
            var divideRoom = new int[4] {0, 0, floorSizeX - 1, floorSizeY - 1}; // 左下, 右上
            for (int i = 0; i < roomNum; i++)
            {
                var border = new Vector2Int();
                border[i % 2] = Random.Range(divideRoom[0 + i % 2], divideRoom[2 + i % 2]);
                if (border[i % 2] >= (divideRoom[2 + i % 2] - divideRoom[0 + i % 2]) / 2)
                {
                    
                }
            }
            return null;
        }
    }
}