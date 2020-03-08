using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters;
using Assets.Scripts.Items;
using GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using UnityEngine.XR.WSA.Persistence;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Dungeon
{
    // Dungeon.MakeFloorによって生成
    public class Floor
    {
        private Room[] rooms;
        private List<IDungeonCharacter> characters;
        private List<IItem> items;
        public Dictionary<EnemyID, float> EnemyTable { get; }
        public float EnemyPopProbability { get; set; }

        public int Number { get; }

        public int NowTurn { get; }

        public TerrainType[,] Terrains { get; set; }

        public IEnumerable<Room> Rooms => rooms;

        public IEnumerable<IDungeonCharacter> Characters => characters;

        public IEnumerable<IDungeonCharacter> Enemies => characters.Skip(1);

        public IDungeonCharacter Player => characters.First();

        public Cell this[int x, int y] => this[new Vector2Int(x,y)];

        public Cell this[Vector2Int v] => new Cell(this, v);

        public Floor(int number, TerrainType[,] terrains, IEnumerable<Room> rooms, Player player)
        {
            Number = number;
            this.Terrains = terrains;
            this.rooms = rooms.ToArray();
            characters = new List<IDungeonCharacter>(){player};
            EnemyTable = new Dictionary<EnemyID, float>();
            items = new List<IItem>();
            EnemyPopProbability = 0;
            NowTurn = 0;
        }

        public IEnumerator NextTurn()
        {
            foreach (var character in characters)
            {
                yield return null;
            }
        }

        public IEnumerable<IDungeonCharacter> Throw(IItem item, Vector2Int basePosition, Vector2Int step,
            bool penetrable)
        {
            yield break;
        }

        public IDungeonCharacter Throw(IDungeonCharacter character, Vector2Int basePosition, Vector2Int destination)
        {
            return null;
        }

        public bool Put(IItem item, Vector2Int position)
        {
            return false;
        }

        public bool Summon(IDungeonCharacter character, Vector2Int position)
        {
            return false;
        }

        public IItem Pick(Vector2Int position)
        {
            IItem tmpItem = null;
            foreach (var item in items)
                if (item.Position == position)
                {
                    items.Remove(item);
                    tmpItem = item;
                }

            return tmpItem;
        }

        public void Kill()
        {
            
        }

        public bool InRange(Vector2Int position) => 
            position.x >= 0 && 
            position.y >= 0 && 
            position.x < Terrains.GetLength(0) && 
            position.y < Terrains.GetLength(1);
    }
}
