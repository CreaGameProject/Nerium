using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Systems;
using Assets.Scripts.States;
using Dungeon;
using Items;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Systems.AttackAttribute;
using Random = UnityEngine.Random;

namespace Characters
{
    public enum MovingAbility
    {
        Walkable, Corner, UnWalkable
    }

    /// <summary>
    /// シーンのダンジョン内で動く、戦えるキャラクターのクラス。
    /// </summary>
    public abstract class BattleCharacter : MonoBehaviour, IDungeonCharacter
    {
        #region シリアライズ

        [SerializeField] private Status status;

        #endregion

        private Vector2Int direction = Vector2Int.down;
        private Movement movement;
        private ConditionEffector conditionEffector;
        private Brain brain;

        public abstract string Name { get; }
        public abstract Force Force { get; }

        public Vector2Int Position
        {
            get => gameObject != null 
                ? TilemapManager.GetFloorPosition(transform.position) 
                : new Vector2Int(0, 0);
            set
            {
                if (gameObject != null) 
                    transform.position = TilemapManager.GetScenePosition(value);
            }
        }

        public Vector2Int Direction
        {
            get => direction;
            set
            {
                var x = value.x == 0 ? 0 : value.x / Mathf.Abs(value.x);
                var y = value.y == 0 ? 0 : value.y / Mathf.Abs(value.y);
                direction = new Vector2Int(x, y);
                if (gameObject != null)
                {
                    var ac = GetComponent<Animator>();
                    ac.SetFloat("x", x);
                    ac.SetFloat("y", y);
                }
            }
        }

        public Floor Floor { get; set; }

        #region ステータス
        public int Hp
        {
            get => hp <= MaxHp.Value ? hp : MaxHp.Value;
            set
            {                
                hp = Mathf.Clamp(value, 0, MaxHp.Value);
                // 0の場合死亡処理
                Floor.Kill(this);
            }
        }
        
        public StatusParameter MaxHp { get; private set; }

        public Status Status
        {
            get => default;
            set
            {
            }
        }

        public ConditionEffector ConditionEffector
        {
            get => default;
            set
            {
            }
        }

        public Movement Movement
        {
            get => default;
            set
            {
            }
        }

        public Brain Brain
        {
            get => default;
            set
            {
            }
        }

        #endregion

        #region 受動

        public virtual bool Attacked(AttackParam attack)
        {
            switch (attack.Attribute)
            {
                case Blow:
                    Hp -= attack.Power - Defense.Value;
                    break;
                case Shot:
                    Hp -= attack.Power - Resist.Value;
                    break;
                case Other:
                    Hp -= attack.Power;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
        
        public abstract bool Healed(int power, BattleCharacter character = null, IItem item = null);
        public abstract bool AddCondition(State state);

        public abstract bool HealCondition(params ConditionID condition);
        
        #endregion


        public abstract ActCategory RequestActCategory();

        public abstract void Move();

        public abstract IEnumerator Action();

        /// <summary>
        /// キャラクターをしていした座標にワープさせる
        /// 移動不可の場合false
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool SetPosition(Vector2Int destination)
        {
            var cell = GameManager.CurrentFloor[destination];
            if (GetMovingAbility(cell.terrain) == MovingAbility.Walkable && cell.character == null)
            {
                Position = destination;
                return true;
            }

            return false;
        }




        // /// <summary>
        // /// 指定したパスで移動する。移動距離にかかわらず移動時間は一定。
        // /// </summary>
        // /// <param name="moveTime"></param>
        // /// <param name="paths"></param>
        // /// <returns></returns>
        // protected IEnumerator Move(float moveTime, params Vector2Int[] paths)
        // {
        //     var stepTime = moveTime / paths.Length;
        //     foreach (var step in paths)
        //     {
        //         Direction = step;
        //         var beforeStep = TilemapManager.GetScenePosition(Position);
        //         var afterStep = TilemapManager.GetScenePosition(Position + step);
        //         for (float t = 0; t < stepTime; t += Time.deltaTime)
        //         {
        //             transform.position = Vector3.Lerp(beforeStep, afterStep, t / stepTime);
        //             yield return null;
        //         }
        //
        //         var item = Floor[Position].item;
        //         if(item != null)
        //             StartCoroutine(StepOn(item));
        //         Position = Position;
        //     }
        // }


        private void Start()
        {
            Floor = GameManager.CurrentFloor;
            MaxHp = new StatusParameter(status.hp);
            Attack = new StatusParameter(status.attack);
            Defense = new StatusParameter(status.defense);
            Dexterity = new StatusParameter(status.dexterity);
            Resist = new StatusParameter(status.resist);
            hp = MaxHp.Value;
        }
    }
}