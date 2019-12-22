using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Dungeon;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    // フィールドを動くキャラ全般の基底クラス。
    // バトルキャラとは限らない
    public abstract class Character : MonoBehaviour
    {
        public Floor OnFloor;

        // return character name
        public override string ToString()
        {
            return base.ToString();
        }

        public Vector2Int Position
        {
            get => Vector2Int.RoundToInt(transform.position);
            set => transform.position = new Vector3(value.x, value.y, 0);
        }
    }
}
