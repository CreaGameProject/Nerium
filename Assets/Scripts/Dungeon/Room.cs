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
    public class Room: IEnumerable<Cell>
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
        /// <param name="range"></param>
        public Room(Floor floor, Vector2Int startPoint, Vector2Int endPoint)
        {
            this.Floor = floor;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        // access to local position
        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0|| x > Range.x || y > Range.y) return null;
                return Floor[StartPoint.x + x, StartPoint.y + y];
            }
        }

        public Cell this[Vector2Int vec] => this[vec.x, vec.y];

        public IEnumerable<IDungeonCharacter> Characters => Floor.Characters.Where(x => InRange(x.Position));

        public IEnumerable<IItem> Items => Floor.Items.Where(x=>InRange(x.Position));

        public bool InRange(Vector2Int position)
        {
            return position.x >= StartPoint.x && position.x <= EndPoint.x &&
                   position.y >= StartPoint.y && position.y <= EndPoint.y;
        }

        // 軽量化のため、すでにキャラクターが要る座標が選ばれた場合召喚は行わない
        public void RandomPopEnemy(GameObject character)
        {
            var cellCount = Range.x * Range.y;
            var rand = Random.Range(0, cellCount);
            GameManager.CurrentFloor.Summon(character, StartPoint + new Vector2Int(rand % Range.x, rand / Range.x));
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = StartPoint.x; i <= EndPoint.x; i++)
            for (int j = StartPoint.y; j <= EndPoint.y; j++)
                yield return Floor[i,j];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
