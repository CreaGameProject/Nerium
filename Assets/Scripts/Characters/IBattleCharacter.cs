using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public enum AttackAttribution
    {
        Melee, Shot
    }

    public interface IBattleCharacter
    {
        // int Hp { get; }
        // int Attack { get; }
        // int Defense { get; }
        // Vector2Int Position { get; }
        int Damage(int attack, AttackAttribution attribution);

        IEnumerator Turn();

        Func<IEnumerator> AbnormalBehave { get; set; }
    }
}