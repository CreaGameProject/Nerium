using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public enum AttackAttribute
    {
        Blow, Shot, Other
    }

    public class AttackParam
    {
        public BattleCharacter Attacker;
        public int Damage;
        public AttackAttribute Attribute;

        public AttackParam(BattleCharacter attacker, int damage, AttackAttribute attribute)
        {
            Attacker = attacker;
            Damage = damage;
            Attribute = attribute;
        }
    }
}