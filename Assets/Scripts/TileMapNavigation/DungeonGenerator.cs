using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    [SerializeField] private List<TerrainTile> _terrainTiles = new List<TerrainTile>();

    [System.Serializable]
    private class TerrainTile
    {
        [SerializeField] private TerrainType _type;
        [SerializeField] private TileBase _tile;
    }
    // Start is called before the first frame update
    void Start()
    {
        var range = new Vector2Int(20, 10);
        var tileMap = _tileMapObject.GetComponent<Tilemap>();
        var a = new UnConvertibleArray<TileBase>(range.x*range.y);
        var b = new UnConvertibleMap<Vector3Int>(range, (x,y) => new Vector3Int(x,y,0));
        var c = new UnConvertibleArray<Vector3Int>(b);
        tileMap.SetTiles(c.Array, a.Array);

    }

    public void GenerateFloor(Map<TerrainType> dungeonData)
    {
        
    }
}
