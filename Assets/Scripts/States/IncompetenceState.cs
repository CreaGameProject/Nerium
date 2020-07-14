using System.Collections;
using System.Collections.Generic;
using Systems;
using Assets.Scripts.States;
using UnityEngine;

/// <summary>
/// 不能状態
/// </summary>
public class IncompetenceState : State
{
    public IncompetenceState(int remainTurn) : base(StateID.Incompetence , remainTurn)
    {
    }
}
