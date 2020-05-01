using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Dungeon;
using Assets.Scripts.Items;
using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts.Characters;
using IDungeonCharacter = Assets.Scripts.Characters.IDungeonCharacter;

public class Cell
{
    public Vector2Int position { get; }

    private Floor floor;

    public Room Room { get; }

    public TerrainType TerrainType { get; set; }

    public IDungeonCharacter Character { get; set; }

    public IItem Item { get; set; }

    public Cell(Floor floor, Vector2Int position)
    {
        this.TerrainType = floor.Terrains[position.x, position.y];
        var candCharas = floor.Characters.Where(c=>c.Position.Equals(position));
        if (candCharas.Count() == 1)
            Character = candCharas.First();
        var candItems = floor.Items.Where(i => i.Position.Equals(position));
        if (candItems.Count() == 1)
            Item = candItems.First();
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