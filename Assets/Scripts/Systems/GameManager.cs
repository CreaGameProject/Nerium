using System.ComponentModel.Design.Serialization;
using Characters;
using Dungeon;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Systems
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static Dungeon.Dungeon CurrentDungeon { get; private set; }
        public static Floor CurrentFloor { get; private set; }
        public static Player GetPlayer { get; private set; }
        public static TurnManager TurnManager => Instance.turnManager;
        
        
        private TurnManager turnManager;
        
        // 潜るダンジョンを指定
        // プレイヤーを指定階に転送
        public static void SetDungeon(Dungeon.Dungeon dungeon, GameObject player, int jumpTo = 1)
        {
            CurrentDungeon = dungeon;
            GetPlayer = player.GetComponent<Player>();
            SetFloor(jumpTo);

            var camera = GameObject.FindWithTag("MainCamera").transform;
            var playerPosition = player.transform.position;
            camera.position = new Vector3(
                playerPosition.x,
                playerPosition.y,
                camera.position.z);
            camera.parent = GetPlayer.transform;
        }

        // 次のフロア呼び出し&開始
        public static void NextFloor()
        {
            if (CurrentFloor.Number == CurrentDungeon.MaxFloorNum)
            {
                DungeonClear();
            }
            SetFloor(CurrentFloor.Number + 1);
        }

        // 階数を指定してフロア呼び出し&ターンルーチン開始
        public static void SetFloor(int floorNum)
        {
            if (floorNum <= 0 || floorNum > CurrentDungeon.MaxFloorNum)
            {
                Debug.LogError("階数が不正です。入力された階数:" + floorNum);
            }
            CurrentFloor = CurrentDungeon.MakeFloor(floorNum, GetPlayer);
            TilemapManager.GenerateFloor(CurrentFloor.Terrains.Matrix);
            Instance.turnManager = new TurnManager(CurrentFloor);
            Instance.turnManager.SetTurnLoop();
        }

        private static void DungeonClear()
        {

        }
    }
}
