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

        private IEnumerator ActionBuffer { get; set; }

        /// <summary>
        /// プレイヤーのコマンド待機
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool Command(ref ActCategory category)
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
                category = ActCategory.Move;
                ActionBuffer = Move(Settings.StepTime, input);
                return true;
            }

            return false;
        }

        // protected override IEnumerator Behave()
        // {
        //     while(true)
        //     {
        //         var shift = Input.GetKey(KeyCode.LeftShift);
        //         var ctrl = Input.GetKey(KeyCode.LeftControl);
        //         var input = new Vector2Int();
        //         input += Input.GetKey(KeyCode.UpArrow) ? Vector2Int.up : Vector2Int.zero;
        //         input += Input.GetKey(KeyCode.DownArrow) ? Vector2Int.down : Vector2Int.zero;
        //         input += Input.GetKey(KeyCode.RightArrow) ? Vector2Int.right : Vector2Int.zero;
        //         input += Input.GetKey(KeyCode.LeftArrow) ? Vector2Int.left : Vector2Int.zero;
        //
        //         if (ctrl)
        //         {
        //             Direction = input;
        //             continue;
        //         }
        //
        //         if (shift && input.sqrMagnitude == 1) continue;
        //
        //         if (input != Vector2Int.zero)
        //         {
        //             StartCoroutine(Move(1, input));
        //             break;
        //         }
        //
        //         yield return null;
        //     }
        //     
        //     
        //
        //     // inputIsActive = true;
        //     // InputManager.SetGroupActive("PlayerCommands", true);
        //     // yield return new WaitUntil(()=>!inputIsActive);
        //     //
        //     // InputManager.SetGroupActive("PlayerCommands", false);
        // }

        public override IEnumerator Turn(ActCategory cat)
        {
            switch (cat)
            {
                case ActCategory.Move:
                    StartCoroutine(ActionBuffer);
                    break;
                default:
                    yield return null;
                    break;
            }
        }

        private void PlayerMove(Vector2Int step)
        {
            IEnumerator Move()
            {
                yield return base.Move(1, step);
            }

            StartCoroutine(Move());
        }

        private void Start()
        {
            // var diagonals = new KeyEvent[4]
            // {
            //     new KeyEvent(InputType.Constant, () => PlayerMove(Vector2Int.up + Vector2Int.right), "PlayerCommands",
            //         KeyCode.UpArrow, KeyCode.RightArrow),
            //     new KeyEvent(InputType.Constant, () => PlayerMove(Vector2Int.up + Vector2Int.left), "PlayerCommands",
            //         KeyCode.UpArrow, KeyCode.LeftArrow),
            //     new KeyEvent(InputType.Constant, () => PlayerMove(Vector2Int.down + Vector2Int.right), "PlayerCommands",
            //         KeyCode.DownArrow, KeyCode.RightArrow),
            //     new KeyEvent(InputType.Constant, () => PlayerMove(Vector2Int.down + Vector2Int.right), "PlayerCommands",
            //         KeyCode.DownArrow, KeyCode.RightArrow)
            // };
            //
            // var commands = new List<KeyEvent>()
            // {
            //     new KeyEvent(InputType.Constant, ()=>PlayerMove(Vector2Int.up), "PlayerCommands", KeyCode.UpArrow),
            //     new KeyEvent(InputType.Constant, ()=>PlayerMove(Vector2Int.down), "PlayerCommands", KeyCode.DownArrow),
            //     new KeyEvent(InputType.Constant, ()=>PlayerMove(Vector2Int.left), "PlayerCommands", KeyCode.LeftArrow),
            //     new KeyEvent(InputType.Constant, ()=>PlayerMove(Vector2Int.right), "PlayerCommands", KeyCode.RightArrow),
            //     diagonals[0], diagonals[1], diagonals[2], diagonals[3],
            //     new KeyEvent(InputType.Down, ()=>diagonals.Select(e=>e.IsActive = false), "PlayerCommands", KeyCode.LeftShift),
            //     new KeyEvent(InputType.Up, ()=>diagonals.Select(e=>e.IsActive = true), "PlayerCommands", KeyCode.LeftShift),
            //     new KeyEvent(InputType.Down, ()=>Debug.Log("down"), "", KeyCode.D)
            // };
            //
            //
            // InputManager.SetGroupActive("PlayerCommands", false);
            // InputManager.Events.AddRange(commands);
        }
    }
}
