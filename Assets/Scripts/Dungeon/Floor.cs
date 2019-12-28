using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters;
using GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

namespace Assets.Scripts.Dungeon
{
    public class Cell
    {
        private TerrainType _terrainType;
        public Cell(TerrainType terrainType) => _terrainType = terrainType;
        public Character Character { get; set; }
        public Item Item { get; set; }
    }
    
    public class Floor : UnConvertibleMap<Cell>
    {
        public Floor(UnConvertibleMap<TerrainType> terrainTypes, params Room[] rooms) : 
            base(terrainTypes.Range, (x, y) => new Cell(terrainTypes[x, y]))
        {
            this.rooms = rooms.ToList();
        }

        public void PutItem(Item item, Vector2Int position)
        {

        }

        public IEnumerable<IDungeonObject> ThrowItem(Item item, Vector2Int from, Vector2Int direction)
        {
            yield break;
        }

        private Tilemap tileMap;
        private List<Room> rooms;
    
    }
}
