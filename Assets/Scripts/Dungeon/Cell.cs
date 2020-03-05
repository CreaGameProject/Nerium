using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dungeon;
using Assets.Scripts.Items;
using UnityEngine;
using UnityEngine.UIElements;

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