using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AttackAttribution
{
    Shot
}

public class TurnAction
{
    public IEnumerable<Vector2Int> Moves { get; set; } = new Vector2Int[0];
    public IEnumerator Behave { get; set; }
}

public interface IBattleCharacter
{
    int GetHp { get; }
    int GetAttack { get; }
    int GetDefense { get; }
    int Damage(int damage, params AttackAttribution[] attributions);
    TurnAction GetTurnAction { get; }
}
