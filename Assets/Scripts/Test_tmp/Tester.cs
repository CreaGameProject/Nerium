using System.Runtime.InteropServices;
using Characters;
using Systems;
using Dungeon;
using Dungeon.Dungeons;
using GridMap;
using UnityEngine;

namespace Test_tmp
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemy;

        // Start is called before the first frame update
        void Start()
        {
            // var gor =  new UnConvertibleMap<TerrainType>(new Vector2Int(20,10), (i, i1) => TerrainType.Floor);
            //
            // TilemapManager.GenerateFloor(gor);
            var dungeon = new TestDungeon();
            
            GameManager.SetDungeon(dungeon, player);
            GameManager.CurrentFloor.Summon(enemy, 3, 3);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
