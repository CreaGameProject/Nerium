using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Dungeon;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMap;
using GenericArray;
using UnityEngine.Assertions.Comparers;

public enum TerrainType
{
    Floor, WaterWay, Wall
}

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tileMapObject;
    [SerializeField] private List<TerrainTile> tileData = new List<TerrainTile>();

    private Dictionary<TerrainType, TileBase> tileDictionary;

    [System.Serializable]
    private class TerrainTile
    {
        public TerrainType type;
        public TileBase _tile;
    }
    // Start is called before the first frame update
    void Start()
    {
        tileDictionary = tileData.ToDictionary((x) => x.type, (y)=>y._tile);

        var range = new Vector2Int(20, 10);
        var tileMap = _tileMapObject.GetComponent<Tilemap>();
        var a = new UnConvertibleArray<TileBase>(range.x*range.y, (x)=>tileDictionary[TerrainType.Floor]);
        var b = new UnConvertibleMap<Vector3Int>(range, (x,y) => new Vector3Int(x,y,0));
        var c = new UnConvertibleArray<Vector3Int>(b);
        tileMap.SetTiles(c.Array, a.Array);

    }

    public void GenerateFloor(Map<TerrainType> dungeonData)
    {
        
    }
}
