using System.Collections;
using System.Collections.Generic;
using Systems;
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

    public interface IDungeonCharacter: IPositional
    {
        string Name { get; }
        Force Force { get; }
        Vector2Int Direction { get; set; }
        IEnumerable<Condition> Conditions { get; set; }
        Status Status { get; set; }
        Status OriginStatus { get; set; }

        ActCategory RequestActCategory();

        void Move();
        void Action();

        bool Attacked(AttackParam attack);

        bool Healed(int power, BattleCharacter character = null, IItem item = null);

        bool AddCondition(Condition condition);

        bool HealCondition(params ConditionID[] condition);
    }
}
