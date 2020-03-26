using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dungeon;

namespace Assets.Scripts.Systems
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public Floor NowFloor { get; }

        public void SetDungeon(Dungeon.Dungeon dungeon)
        {

        }

        public void NextFloor()
        {

        }

        public void JumpFloor()
        {

        }

        private IEnumerator ManageTurn()
        {
            return null;
        }
    }
}
