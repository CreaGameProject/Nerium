using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dungeon;
using Assets.Scripts.Items;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public enum Force
    {
        None, Player, Enemy
    }

    public enum ActCategory
    {
        Move, Action
    }

    public interface IDungeonCharacter
    {
        string Name { get; }
        Force Force { get; }
        Vector2Int Position { get; set; }
        Vector2Int Direction { get; set; }
        Floor Floor { get; set; }

        ActCategory RequestActCategory();

        void Move();

        IEnumerator Action();

        bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null);

        bool Healed(int power, BattleCharacter character = null, IItem item = null);

        bool AddState(State state);

        IEnumerable<State> GetStates { get; }

        bool HealStates(params StateID[] states);
    }
}
