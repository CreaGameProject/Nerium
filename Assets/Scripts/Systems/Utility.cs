using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public static class Utility
    {
        public static Vector2Int LeftUp => Vector2Int.left + Vector2Int.up;
        public static Vector2Int LeftDown => Vector2Int.left + Vector2Int.down;
        public static Vector2Int RightUp => Vector2Int.right + Vector2Int.up;
        public static Vector2Int RightDown => Vector2Int.right + Vector2Int.down;
        
        public static int GetDirectionIndex(Vector2Int v)
        {
            return
                v == Vector2Int.right ? 0 :
                v == RightUp ? 1 :
                v == Vector2Int.up ? 2 :
                v == LeftUp ? 3 :
                v == Vector2Int.left ? 4 :
                v == LeftDown ? 5 :
                v == Vector2Int.down ? 6 :
                v == RightDown ? 7 :
                -1;
        }
    }
}
