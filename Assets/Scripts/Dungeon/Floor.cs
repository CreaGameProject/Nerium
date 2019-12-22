using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters;
using GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Dungeon
{
    public class Cell
    {
        private TerrainType _terrainType;
        public Cell(TerrainType terrainType) => _terrainType = terrainType;
        public Character Character;
        public Item Item;
    }
    
    public class Floor : UnConvertibleMap<Cell>
    {
        private Tilemap tileMap;
        private List<Room> rooms;

        public Floor(UnConvertibleMap<TerrainType> terrainTypes, params Room[] rooms) : base(terrainTypes.Range,
            (x, y) => new Cell(terrainTypes[x, y])) => this.rooms = rooms.ToList();
    }
}
