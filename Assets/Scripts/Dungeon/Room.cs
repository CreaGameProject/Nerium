using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dungeon
{
    public class RoomEffect
    {

    }

    public class Room
    {
        public Floor Floor { get; }
        public Vector2Int RectStart { get; }
        public Vector2Int RectEnd { get; }

        public RoomEffect Effect { get; }

        public Room(Floor floor, Vector2Int rectStart, Vector2Int rectEnd)
        {
            Floor = floor;
            RectStart = rectStart;
            RectEnd = rectEnd;
        }

        public Cell this[Vector2Int position]
        {
            get => Floor[RectStart + position];
            set => Floor[RectStart + position] = value;
        }
    }
}
