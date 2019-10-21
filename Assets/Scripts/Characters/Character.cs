using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public abstract class Character : MonoBehaviour
    {
        public int Hp;

        public Vector2Int Position
        {
            get => Vector2Int.RoundToInt(transform.position);
            set => transform.position = new Vector3(value.x, value.y, 0);
        }

        public abstract IEnumerator NextTurn();
        
    }
}
