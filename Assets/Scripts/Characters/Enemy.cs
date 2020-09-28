using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Characters
{
    public enum EnemyID
    {
        TestEnemy
    }

    public abstract class Enemy : BattleCharacter
    {
        [SerializeField] private int maxHp;
        [SerializeField] private int attack;
        [SerializeField] private int dexterity;
        [SerializeField] private int defense;
        [SerializeField] private int resist;
        public EnemyID Id { get; }
    
        public override Force Force => Force.Enemy;

        public Enemy()
        {

        }
    }
}