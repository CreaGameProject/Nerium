using Systems;

namespace Assets.Scripts.States
{
    public enum StateID
    {
        Incompetence
    }

    /// <summary>
    /// 状態異常
    /// </summary>
    public abstract class State
    {
        public StateID Id { get; private set; }
        private BattleCharacter character;
        private int remainTurn;

        protected State(StateID id, int remainTurn)
        {
            Id = id;
            this.remainTurn = remainTurn;
        }
        
        public virtual void Caught(BattleCharacter character)
        {
            this.character = character;
        }

        public virtual void Healed()
        {
            
        }
        
        // 戻り値要検討
        public virtual void Behave()
        {
            
        }

        public virtual AttackParam GetAttack(AttackParam attack) => attack;

        public virtual AttackParam Attack(AttackParam attack) => attack;

        public virtual int GetDamage(int damage) => damage;

        public virtual int Damage(int damage) => damage;

        public virtual void BehaveEnd()
        {
            remainTurn--;
            if (remainTurn <= 0){
                character.HealStates(Id);
            }
        }

        public virtual void TurnEnd()
        {
            
        }
    }
}
