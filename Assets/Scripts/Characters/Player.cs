using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Assets.Scripts.Items;
using Assets.Scripts.States;
using Assets.Scripts.Systems;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Scripting;

namespace Assets.Scripts.Characters
{
    public class Player : BattleCharacter
    {
        public override string Name { get; } = "ネリウム";
        public override Force Force { get; } = Force.Player;
        
        public int Money { get; set; }
        public List<Item> Items { get; set; }

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
            
            if (input != Vector2Int.zero && CanMoveTo(input))
            {
                ActionBuffer = Move(Settings.StepTime, input);
                return true;
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
                if (Floor[Position + Direction].Character != null)
                {
                    var target = Floor[Position + Direction].Character;
                    target.Attacked(status.Attack, false, this);
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

        private void Start()
        {
            
        }
    }
}
