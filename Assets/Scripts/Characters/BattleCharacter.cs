using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters;
using Assets.Scripts.Items;
using Assets.Scripts.States;
using UnityEngine;

public abstract class BattleCharacter : IDungeonCharacter
{
    public abstract string Name { get; }
    public abstract Force Force { get; }
    public Vector2Int Position { get; }

    public IEnumerator Turn()
    {
        yield return Behave();
        foreach (var state in GetStates)
        {
            
        }
    }

    public abstract bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null);
    public abstract bool Healed(int power, BattleCharacter character = null, IItem item = null);
    public abstract bool AddState(State state);
    public abstract bool HealStates(params StateID[] states);

    public int MaxHp { get; }
    public int Hp { get; }
    public int Attack { get; }
    public int Dexterity { get; }
    public int Defense { get; }
    public int Resist { get; }
    public IEnumerable<State> GetStates => States;

    protected List<State> States = new List<State>();

    // Turnから呼び出し
    protected abstract IEnumerator Behave();

    protected IEnumerator Move(Vector2Int destination)
    {
        return null;
    }
}
