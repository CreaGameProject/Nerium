using Characters;
using Systems;
using Dungeon.Dungeons;
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
            GameManager.CurrentFloor.Summon(enemy, new Vector2Int(3,3));
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
