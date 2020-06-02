using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Characters;
using UnityEngine;

public enum EnemyID
{
    TestEnemy
}

public abstract class Enemy : BattleCharacter
{
    public EnemyID Id { get; }
    
    public override Force Force => Force.Enemy;
}
