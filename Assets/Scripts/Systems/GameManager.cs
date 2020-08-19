using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Systems;
using Assets.Scripts.Characters;
using Assets.Scripts.Dungeon;
using GridMap;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Systems
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static Dungeon.Dungeon CurrentDungeon { get; private set; }
        public static Floor CurrentFloor { get; private set; }
        public static Player GetPlayer { get; private set; }

        private TurnManager turnManager;
        
        // 潜るダンジョンを指定
        public static void SetDungeon(Dungeon.Dungeon dungeon, Player player, int jumpTo = 1)
        {
            CurrentDungeon = dungeon;
            GetPlayer = player;
            SetFloor(jumpTo);
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

        // 階数を指定してフロア呼び出し&開始
        public static void SetFloor(int floorNum)
        {
            if (floorNum <= 0 || floorNum > CurrentDungeon.MaxFloorNum)
            {
                Debug.LogError("階数が不正です。入力された階数:" + floorNum);
            }
            CurrentFloor = CurrentDungeon.MakeFloor(floorNum, GetPlayer);
            TilemapManager.GenerateFloor(CurrentFloor.Terrains);
            Instance.turnManager = new TurnManager(CurrentFloor);
            Instance.turnManager.SetTurnLoop();
        }

        private static void DungeonClear()
        {

        }
    }
}
