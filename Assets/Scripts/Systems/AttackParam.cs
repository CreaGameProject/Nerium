using System.Collections;
using System.Collections.Generic;
using Characters;
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
        public int Power;
        public AttackAttribute Attribute;

        public AttackParam(BattleCharacter attacker, int power, AttackAttribute attribute)
        {
            Attacker = attacker;
            power = power;
            Attribute = attribute;
        }
    }
}