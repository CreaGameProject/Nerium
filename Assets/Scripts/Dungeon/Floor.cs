using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Items;
using GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using UnityEngine.XR.WSA.Persistence;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Dungeon
{
    public class Floor
    {
        private Room[] rooms;
        private List<IDungeonCharacter> characters;
        private List<IItem> items;

        public Dungeon Dungeon { get; }

        public int Number { get; }

        public int NowTurn { get; }

        public TerrainType[,] terrains { get; set; }

        public IEnumerable<Room> Rooms => rooms;

        public IEnumerable<IDungeonCharacter> Characters => characters;

        public IEnumerable<IDungeonCharacter> Enemies => characters.Skip(1);

        public IDungeonCharacter Player => characters.First();

        public Cell this[int x, int y] => this[new Vector2Int(x,y)];

        public Cell this[Vector2Int v] => new Cell(this, v);


    }
}
