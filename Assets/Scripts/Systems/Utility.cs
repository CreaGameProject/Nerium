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
    }
}
