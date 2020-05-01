using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Assets.Scripts.Systems
{
    /// <summary>
    /// 入力を管理するクラス。キーに対応するメソッドをEventsリストに登録することで動作する。
    /// </summary>
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        /// <summary>
        /// キーに対応させるメソッドをAddする。
        /// </summary>
        public static List<KeyEvent> Events => Instance.events;
    
        private readonly List<KeyEvent> events = new List<KeyEvent>();


        /// <summary>
        /// 特定のグループのイベントの有効と無効を切り替える。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="isActive"></param>
        public static void SetGroupActive(string group, bool isActive)
        {
            Events.Where(x=>x.Group.Equals(group)).Select(x => x.IsActive = isActive);
        }

        private bool InputSelector(InputType inputType, KeyCode key)
        {
            switch (inputType)
            {
                case InputType.Up:
                    return Input.GetKeyUp(key);

                case InputType.Down:
                    return Input.GetKeyDown(key);

                case InputType.Constant:
                    return Input.GetKey(key);

                default:
                    return false;
            }
        }

        private void Update()
        {
            foreach (var keyEvent in events)
            {
                if (keyEvent.IsActive)
                {

                    var runAction = true;
                    foreach (var key in keyEvent.Keys)
                    {
                        runAction = runAction && InputSelector(keyEvent.InputInputType, key);
                    }
                    if (runAction)
                    {
                        keyEvent.Action();
                    }
                }
            }
        }
    }

    public class KeyEvent
    {
        public KeyEvent(InputType inputType, Action action, string group = "", params KeyCode[] keys)
        {
            Keys = keys;
            InputInputType = inputType;
            Action = action;
            Group = group;
            IsActive = true;
        }

        public KeyCode[] Keys { get; set; }
        public string Group { get; set; }
        public InputType InputInputType { get; set; }
        public Action Action { get; set; }
        public bool IsActive { get; set; }
    }

    public enum InputType
    {
        Down, Up, Constant
    }
}