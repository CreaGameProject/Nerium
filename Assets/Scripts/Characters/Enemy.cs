using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Characters;
using UnityEngine;

public enum EnemyID
{

}

public abstract class Enemy : BattleCharacter
{
    public override Force Force => Force.Enemy;
}
