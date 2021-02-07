using System.Collections;
using Systems;
using Assets.Scripts.States;
using Items;
using UnityEngine;

namespace Characters.Enemies
{
    public class TestEnemy : Enemy
    {
        public override string Name => "TestEnemy";

        public override ActCategory RequestActCategory()
        {
            return ActCategory.Move;
        }

        public override void Move()
        {
            if(CanMoveTo(Vector2Int.right))
                StartCoroutine(Move(DynamicParameter.StepTime, Vector2Int.right));
            else
                StartCoroutine(Move(DynamicParameter.StepTime, Vector2Int.left));
        }

        public override IEnumerator Action()
        {
            yield return null;
        }

        public override bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool Healed(int power, BattleCharacter character = null, IItem item = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool AddCondition(State state)
        {
            throw new System.NotImplementedException();
        }

        public override bool HealCondition(params State[] states)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            status = new Status();
        }
    }
}