using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters;
using Assets.Scripts.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Dungeon
{
    public class Room
    {
        private Floor floor;
        private Vector2Int startPoint;
        private Vector2Int range;
        
        // access to local position
        public Room(Floor floor, Vector2Int startPoint, Vector2Int range)
        {
            this.floor = floor;
            this.startPoint = startPoint;
            this.range = range;
        }

        // access to local position
        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0|| x > range.x || y > range.y) return null;
                return floor[startPoint.x + x, startPoint.y + y];
            }
        }

        public Cell this[Vector2Int vec] => this[vec.x, vec.y];

        public IEnumerable<IDungeonCharacter> Characters => floor.Characters.Where(x => InRange(x.Position));

        public IEnumerable<IItem> Items => floor.Items.Where(x=>InRange(x.Position));

        public bool InRange(Vector2Int position)
        {
            position -= startPoint;
            return position.x >= 0 &&
                   position.y >= 0 &&
                   position.x < range.x &&
                   position.y < range.y;
        }

        // 軽量化のため、すでにキャラクターが要る座標が選ばれた場合召喚は行わない
        public void RandomPopEnemy(IDungeonCharacter character)
        {
            var cellCount = range.x * range.y;
            var rand = Random.Range(0, cellCount);
            floor.Summon(character, startPoint + new Vector2Int(rand % range.x, rand / range.x));
        }
    }
}
