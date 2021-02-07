using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Items
{
    public class Trap: IItem
    {
        public string Name { get; }
        public void SetNickName(string nickName)
        {
            throw new System.NotImplementedException();
        }

        public Vector2Int Position { get; set; }
        public Item Derived { get; }
        public IEnumerator SteppedBy(BattleCharacter character)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator Hit(BattleCharacter character)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetMenuItem()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator Use(string command)
        {
            throw new System.NotImplementedException();
        }
    }
}