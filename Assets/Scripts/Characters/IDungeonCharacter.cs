using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.States;
using Dungeon;
using Items;
using UnityEngine;

namespace Characters
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

        BattleCharacter Derived { get; }

        ActCategory RequestActCategory();

        void PlayMove();

        IEnumerator PlayAction();

        bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null);

        bool Healed(int power, BattleCharacter character = null, IItem item = null);

        bool AddState(State state);

        IEnumerable<State> GetStates { get; }

        bool HealStates(params StateID[] states);
    }
}
