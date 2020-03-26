using System.Collections;
using System.Collections.Generic;
using GridMap;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gor =  new UnConvertibleMap<TerrainType>(new Vector2Int(20,10), (i, i1) => TerrainType.Floor);
        TilemapManager.GenerateFloor(gor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
