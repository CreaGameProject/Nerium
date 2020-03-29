using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Items;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public enum Force
    {
        None, Player, Enemy
    }

    public interface IDungeonCharacter
    {
        string Name { get; }
        Force Force { get; }
        Vector2Int Position { get; }

        IEnumerator Turn();

        bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null);

        bool Healed(int power, BattleCharacter character = null, IItem item = null);

        bool AddState(State state);

        bool HealStates(params StateID[] states);
    }
}
