using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Dungeon;
using Assets.Scripts.Systems;
using UnityEngine;
using UnityEngine.Tilemaps;
using GridMap;
using GenericArray;
using UnityEngine.Assertions.Comparers;

public enum TerrainType
{
    Floor, WaterWay, Wall
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
    public static Vector3 GetScenePosition(Vector2Int localPosition) => Instance.transform.position + new Vector3(localPosition.x, 0, localPosition.y) * CellSize;

    // タイルマップを描画する
    public static void GenerateFloor(UnConvertibleMap<TerrainType> dungeonData)
    {
        var range = dungeonData.Range;

        var positionArray = new UnConvertibleArray<Vector3Int>(
            new UnConvertibleMap<Vector3Int>(range, (c, r) => new Vector3Int(c, r, 0)));

        var tileArray = new UnConvertibleArray<TileBase>(
            new UnConvertibleMap<TileBase>(range, (c, r) => tileDictionary[dungeonData[c, r]]));

        tilemap.SetTiles(positionArray.Array, tileArray.Array);
    }

    // Start is called before the first frame update
    void Start()
    {
        tileDictionary = tileData.ToDictionary((x) => x.type, (y) => y._tile);
        tilemap = GetComponent<Tilemap>();
    }
}
