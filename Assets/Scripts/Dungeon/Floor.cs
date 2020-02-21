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
using Vector2 = UnityEngine.Vector2;

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

        public void ThrowItem(Item item)
        {

        }

        public void PutItem(Item item, Vector2Int position)
        {

        }

        public bool InRange(Vector2Int position)
        {
            if (position.x < -1 || position.x > Range.x) return false;
            if (position.y < -1 || position.y > Range.y) return false;
            return true;
        }

        private Tilemap tileMap;
        private List<Room> rooms;

        
    
    }
}
