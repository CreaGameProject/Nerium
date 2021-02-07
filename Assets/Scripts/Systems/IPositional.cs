using UnityEngine;

namespace Systems
{
    public interface IPositional
    {
        Vector2Int Position { get; set; }
    }
}