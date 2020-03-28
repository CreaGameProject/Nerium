using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Characters;
using Assets.Scripts.Dungeon;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Systems
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public Dungeon.Dungeon CurrentDungeon { get; private set; }
        public Floor CurrentFloor { get; private set; }
        public Player GetPlayer { get; private set; }

        public void SetDungeon(Dungeon.Dungeon dungeon, Player player, int jumpTo = 1)
        {
            CurrentDungeon = dungeon;
            GetPlayer = player;
            JumpFloor(jumpTo);
        }

        public void NextFloor()
        {
            if (CurrentFloor.Number == CurrentDungeon.MaxFloorNum)
            {
                DungeonClear();
            }
            JumpFloor(CurrentFloor.Number + 1);
        }

        public void JumpFloor(int floorNum)
        {
            if (floorNum <= 0 || floorNum > CurrentDungeon.MaxFloorNum)
            {
                Debug.LogError("階数が不正です。入力された階数:" + floorNum);
            }
            CurrentFloor = CurrentDungeon.MakeFloor(floorNum);
        }

        private IEnumerator ManageTurn()
        {
            return null;
        }

        private void DungeonClear()
        {

        }
    }
}
