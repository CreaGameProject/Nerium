using System.Linq;
using Items;
using UnityEngine;
using IDungeonCharacter = Characters.IDungeonCharacter;

namespace Dungeon
{
    public class Cell
    {
        public Vector2Int Position { get; }

        private Floor floor;

        public Room Room { get; }

        public TerrainType TerrainType
        {
            get => floor.Terrains[Position.x, Position.y];
            set => floor.Terrains[Position.x, Position.y] = value;
        }

        public IDungeonCharacter Character => floor.Characters.FirstOrDefault(i => i.Position == Position);

        public IItem Item => floor.Items.FirstOrDefault(i => i.Position == Position);

        public Cell(Floor floor, Vector2Int position)
        {
            this.floor = floor;
            this.Position = position;
        }

        public IItem Pick()
        {
            return null;
        }

        public IDungeonCharacter DeleteCharacter()
        {
            return null;
        }
    }
}