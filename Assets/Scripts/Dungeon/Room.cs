using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems;
using Characters;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeon
{
    public class Room: IEnumerable<(IItem item, IDungeonCharacter character, TerrainType terrain)>
    {
        public readonly Floor Floor;
        public Vector2Int StartPoint;
        public Vector2Int EndPoint;
        public Vector2Int Range => EndPoint - StartPoint + Vector2Int.one;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Room(Floor floor, Vector2Int startPoint, Vector2Int endPoint)
        {
            this.Floor = floor;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }
        
        public bool InRange(Vector2Int position)
        {
            return position.x >= StartPoint.x && position.x <= EndPoint.x &&
                   position.y >= StartPoint.y && position.y <= EndPoint.y;
        }

        // 軽量化のため、すでにキャラクターが要る座標が選ばれた場合召喚は行わない
        public void RandomPopEnemy(GameObject character)
        {
            var x = Random.Range(StartPoint.x, EndPoint.x + 1);
            var y = Random.Range(StartPoint.y, EndPoint.y + 1);
            Floor.Summon(character, x, y);
        }

        public IEnumerator<(IItem item, IDungeonCharacter character, TerrainType terrain)> GetEnumerator()
        {
            for (int i = StartPoint.x; i <= EndPoint.x; i++)
            for (int j = StartPoint.y; j <= EndPoint.y; j++)
                yield return Floor[i,j];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
