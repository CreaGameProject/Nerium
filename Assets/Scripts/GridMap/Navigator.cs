using System;
using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace GridMap
{
    public static class Navigator
    {
        /// <summary>
        /// ゲーム上の近傍の方向を列挙する
        /// </summary>
        public static IEnumerable<Vector2Int> Neighborhood =>
            new[]
            {
                Vector2Int.right,
                Utility.RightUp,
                Vector2Int.up,
                Utility.LeftUp,
                Vector2Int.left,
                Utility.LeftDown,
                Vector2Int.down,
                Utility.RightDown
            };

        /// <summary>
        /// DistanceMap専用の構造体
        /// </summary>
        private struct SearchAgent
        {
            public Vector2Int Position;
            public int Distance;
        }

        /// <summary>
        /// 最大ステップ数の実装がまだ
        /// </summary>
        /// <param name="size"></param>
        /// <param name="basePosition"></param>
        /// <param name="neighbor"></param>
        /// <param name="maxStep"></param>
        /// <returns></returns>
        public static GridMap<int> DistanceMap(Vector2Int size, Vector2Int basePosition, 
            Func<Vector2Int, IEnumerable<Vector2Int>> neighbor, int maxStep = -1)
        {
            var map = new GridMap<int>(size, v => -1);
            var agents = new Queue<SearchAgent> ();
            agents.Enqueue(new SearchAgent(){Position = basePosition, Distance = 0});
            map[basePosition] = 0;
            while (0 < agents.Count)
            {
                var cur = agents.Dequeue();
                foreach (var vec in neighbor(cur.Position))
                {
                    if (!map.InRange(vec) || map[vec] != -1) continue;
                    var item = new SearchAgent {Position = vec, Distance = map[vec] = cur.Distance + 1};
                    agents.Enqueue(item);
                }

                if (cur.Distance == maxStep)
                    break;
            }
            return map;
        }
    }
}
