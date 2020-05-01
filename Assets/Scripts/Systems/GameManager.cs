using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static void SetDungeon(Dungeon.Dungeon dungeon, Player player, int jumpTo = 1)
        {
            CurrentDungeon = dungeon;
            GetPlayer = player;
            JumpFloor(jumpTo);
        }

        public static void NextFloor()
        {
            if (CurrentFloor.Number == CurrentDungeon.MaxFloorNum)
            {
                DungeonClear();
            }
            JumpFloor(CurrentFloor.Number + 1);
        }

        public static void JumpFloor(int floorNum)
        {
            if (floorNum <= 0 || floorNum > CurrentDungeon.MaxFloorNum)
            {
                Debug.LogError("階数が不正です。入力された階数:" + floorNum);
            }
            CurrentFloor = CurrentDungeon.MakeFloor(floorNum, GetPlayer);
            TilemapManager.GenerateFloor(CurrentFloor.Terrains);
            Instance.StartFloor();
        }

        private void StartFloor()
        {
            StartCoroutine(ManageTurn());
        }

        private IEnumerator ManageTurn()
        {
            for (int turn = 1; turn <= CurrentFloor.MaxTurn; turn++)
            {
                Debug.Log("Turn : " + CurrentFloor.Turn);
                yield return CurrentFloor.NextTurn();
            }
        }

        private static void DungeonClear()
        {

        }
    }
}
