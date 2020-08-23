using System;
using System.Collections;
using Assets.Scripts.Items;
using Assets.Scripts.States;
using UnityEditor;

namespace Assets.Scripts.Characters.Enemies
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

        public override bool AddState(State state)
        {
            throw new System.NotImplementedException();
        }

        public override bool HealStates(params StateID[] states)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            status = new Status();
        }
    }
}