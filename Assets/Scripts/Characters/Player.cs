using System.Collections;
using Assets.Scripts.Items;
using Assets.Scripts.States;

namespace Assets.Scripts.Characters
{
    public class Player : BattleCharacter, IDungeonCharacter
    {
        public override string Name { get; }
        public override Force Force { get; }
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

        protected override IEnumerator Behave()
        {
            throw new System.NotImplementedException();
        }
    }
}
