using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMap;
using GenericArray;
using UnityEngine.Assertions.Comparers;

public enum TerrainType
{
    Wall, Floor, WaterWay 
}

public class TilemapManager : SingletonMonoBehaviour<TilemapManager>
{
    [SerializeField] private GameObject _tileMapObject;
    [SerializeField] private List<TerrainTile> tileData;
    [SerializeField] private float cellSize;

    public static float CellSize => tilemap.cellSize.x;

    private static Dictionary<TerrainType, TileBase> tileDictionary;
    private static Tilemap tilemap;

    // シリアライズ用のクラス
    [System.Serializable]
    private class TerrainTile
    {
        public TerrainType type;
        public TileBase _tile;
    }

    // シーン上の座標を取得する
    public static Vector3 GetScenePosition(Vector2Int floorPosition) => Instance.transform.position + new Vector3(floorPosition.x, floorPosition.y, 0) * CellSize;
    
    // シーン上の座標からフロア上の座標を取得
    public static Vector2Int GetFloorPosition(Vector3 scenePosition) =>
        Vector2Int.RoundToInt(scenePosition - Instance.transform.position);

    // タイルマップを描画する
    public static void GenerateFloor(TerrainType[,] dungeonData)
    {
        var range = new Vector2Int(dungeonData.GetLength(0), dungeonData.GetLength(1));

        var positionArray = new UnConvertibleArray<Vector3Int>(
            new UnConvertibleMap<Vector3Int>(range, v => new Vector3Int(v.x, v.y, 0)));

        var tileArray = new UnConvertibleArray<TileBase>(
            new UnConvertibleMap<TileBase>(range, v => tileDictionary[dungeonData[v.x, v.y]]));

        tilemap.SetTiles(positionArray.Array, tileArray.Array);
    }

    // Start is called before the first frame update
    void Start()
    {
        tileDictionary = tileData.ToDictionary((x) => x.type, (y) => y._tile);
        tilemap = GetComponent<Tilemap>();
    }
}
