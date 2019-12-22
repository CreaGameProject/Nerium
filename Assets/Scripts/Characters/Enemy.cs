using UnityEngine;
using System.Collections;
using Assets.Scripts.Characters;

public class Enemy : Character, IBattleCharacter
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHp { get; }
    public int GetAttack { get; }
    public int GetDefense { get; }
    public int Damage(int damage, params AttackAttribution[] attributions)
    {
        throw new System.NotImplementedException();
    }

    public TurnAction GetTurnAction { get; }
}
