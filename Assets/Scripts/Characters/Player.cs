using System.Collections;
using System.Collections.Generic;
using Systems;
using Assets.Scripts.States;
using Items;
using UnityEngine;

namespace Characters
{
    public class Player : BattleCharacter
    {
        public override string Name { get; } = "ネリウム";
        public override Force Force { get; } = Force.Player;
        
        public int Money { get; set; }
        public List<Item> Items { get; set; }

        public override bool Attacked(AttackParam attack)
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

        private IEnumerator ActionBuffer { get; set; }

        /// <summary>
        /// プレイヤーのコマンド待機
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Command(ref ActCategory category)
        {
            bool commandAccept = false;
            if (commandAccept = MoveCommandCheck())
            {
                category = ActCategory.Move;
            }
            else if (commandAccept = AttackCommandCheck())
            {
                category = ActCategory.Action;
            }

            return commandAccept;
        }

        /// <summary>
        /// 使用不可
        /// </summary>
        /// <returns>必ずActionが返ってくる</returns>
        public override ActCategory RequestActCategory()
        {
            return ActCategory.Action;
        }
        
        public override IEnumerator Action()
        {
            yield return ActionBuffer;
        }

        public override void Move()
        {
            StartCoroutine(ActionBuffer);
        }

        // 
        private bool MoveCommandCheck()
        {
            var shift = Input.GetKey(KeyCode.LeftShift);
            var ctrl = Input.GetKey(KeyCode.LeftControl);
            var q = Input.GetKey(KeyCode.Q);
            var input = new Vector2Int();
            input += Input.GetKey(KeyCode.UpArrow) ? Vector2Int.up : Vector2Int.zero;
            input += Input.GetKey(KeyCode.DownArrow) ? Vector2Int.down : Vector2Int.zero;
            input += Input.GetKey(KeyCode.RightArrow) ? Vector2Int.right : Vector2Int.zero;
            input += Input.GetKey(KeyCode.LeftArrow) ? Vector2Int.left : Vector2Int.zero;
            
            if (ctrl)
            { // 方向切り替え
                Direction = input;
                return false;
            }
            
            if (shift && (input.x == 0 || input.y == 0)) 
                return false;
            
            if (input != Vector2Int.zero)
            {
                Direction = input;
                if (CanMoveTo(input))
                {
                    if (q)
                        DynamicParameter.StepTime = StaticSetting.RunTime;
                    else
                        DynamicParameter.StepTime = StaticSetting.WalkTime;
                
                    ActionBuffer = Move(DynamicParameter.StepTime, input);
                    return true;
                }
            }

            return false;
        }

        private bool AttackCommandCheck()
        {
            var z = Input.GetKeyDown(KeyCode.Z);
            var x = Input.GetKeyDown(KeyCode.X);
            var c = Input.GetKeyDown(KeyCode.C);
            if (z)
            {
                
                Debug.Log(Floor);
                if (Floor[Position + Direction].character != null)
                {
                    var target = Floor[Position + Direction].character;
                    target.Attacked(new AttackParam(this, Attack.Value, AttackAttribute.Blow));
                }

                IEnumerator ww()
                {
                    yield return new WaitForSeconds(1);
                }

                ActionBuffer = ww();
                
                return true;
            }

            return false;
        }

        protected override IEnumerator StepOn(IItem item)
        {
            GameManager.TurnManager.Enable = false;
            yield return item.SteppedBy(this);
            GameManager.TurnManager.Enable = true;
        }

        private void Start()
        {
            
        }
    }
}
