using Assets.Scripts.Characters;
using Assets.Scripts.Systems;
using UnityEngine;

namespace Assets.Scripts.Test_tmp
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private Player player;

        // Start is called before the first frame update
        void Start()
        {
            // var gor =  new UnConvertibleMap<TerrainType>(new Vector2Int(20,10), (i, i1) => TerrainType.Floor);
            //
            // TilemapManager.GenerateFloor(gor);
            var dungeon = new TestDungeon();
            
            GameManager.SetDungeon(dungeon, player);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
