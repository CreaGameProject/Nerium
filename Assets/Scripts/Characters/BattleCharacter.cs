using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Systems;
using Assets.Scripts.States;
using Assets.Scripts.Systems;
using Dungeon;
using Items;
using UnityEngine;
using UnityEngine.Tilemaps;
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
        private Vector2Int direction = Vector2Int.down;

        public abstract string Name { get; }
        public abstract Force Force { get; }

        public BattleCharacter Derived => this;

        public Vector2Int Position
        {
            get
            {
                if (gameObject != null) {
                    return TilemapManager.GetFloorPosition(transform.position);
                }
                return new Vector2Int(0, 0);
            }
            set
            {
                if (gameObject != null)
                {
                    transform.position = TilemapManager.GetScenePosition(value);
                }
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


        public virtual bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null)
        {
            if (Random.value > 0.9)
                return false;
            status.Hp = Mathf.Max(0, status.Hp - (power - status.Defense));
            if(status.Hp == 0)
                Floor.Kill(this);
            return true;
        }
        public abstract bool Healed(int power, BattleCharacter character = null, IItem item = null);
        public abstract bool AddState(State state);

        public abstract bool HealStates(params StateID[] states);

        protected Status status;
        public Status Status => status;

        public IEnumerable<State> GetStates => States;

        protected List<State> States = new List<State>();

        public virtual MovingAbility GetMovingAbility(TerrainType terrain)
        {
            switch (terrain)
            {
                case TerrainType.Floor:
                    return MovingAbility.Walkable;
                case TerrainType.WaterWay:
                    return MovingAbility.Corner;
                default:
                    return MovingAbility.UnWalkable;
            }
        }

        public abstract ActCategory RequestActCategory();

        public abstract void PlayMove();

        public abstract IEnumerator PlayAction();

        /// <summary>
        /// キャラクターをしていした座標にワープさせる
        /// 移動不可の場合false
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool Warp(Vector2Int destination)
        {
            var cell = GameManager.CurrentFloor[destination];
            if (GetMovingAbility(cell.TerrainType) == MovingAbility.Walkable && cell.Character == null)
            {
                Position = destination;
                return true;
            }

            return false;
        }


        /// <summary>
        /// 一マス隣の座標に移動可能であるかを判定する
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool CanMoveTo(Vector2Int to) => CanMoveTo(to, Position);


        /// <summary>
        /// 一マス隣の座標に移動可能であるかを判定する
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public bool CanMoveTo(Vector2Int to, Vector2Int from)
        {
            // directionが2マス以上の移動の場合false
            if (Mathf.Abs(to.x) > 1 || Mathf.Abs(to.y) > 1)
            {
                return false;
            }
            // 判定マスがフロア範囲外の場合false
            if (!GameManager.CurrentFloor.InRange(Position + to))
            {
                return false;
            }
            var nextCell = GameManager.CurrentFloor[Position + to];
            // 判定マスが壁であるor判定マスにキャラが存在する場合false
        
            if (GetMovingAbility(nextCell.TerrainType) != MovingAbility.Walkable || nextCell.Character != null)
            {
                return false;
            }
            // 斜め移動かつコーナーが移動不可マスの場合false
            if (to.magnitude > 1)
            {
                var corner = to;
                corner.x = 0;
                if (GetMovingAbility(GameManager.CurrentFloor[Position + corner].TerrainType) == MovingAbility.UnWalkable)
                {
                    return false;
                }

                corner = to;
                corner.y = 0;
                if (GetMovingAbility(GameManager.CurrentFloor[Position + corner].TerrainType) == MovingAbility.UnWalkable)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 指定したパスで移動する。移動距離にかかわらず移動時間は一定。
        /// </summary>
        /// <param name="moveTime"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        protected IEnumerator Move(float moveTime, params Vector2Int[] paths)
        {
            var stepTime = moveTime / paths.Length;
            foreach (var step in paths)
            {
                Direction = step;
                var beforeStep = TilemapManager.GetScenePosition(Position);
                var afterStep = TilemapManager.GetScenePosition(Position + step);
                for (float t = 0; t < stepTime; t += Time.deltaTime)
                {
                    transform.position = Vector3.Lerp(beforeStep, afterStep, t / stepTime);
                    yield return null;
                }

                var item = Floor[Position].Item;
                if(item != null)
                    StartCoroutine(StepOn(item));
                Position = Position;
            }
        }

        protected virtual IEnumerator StepOn(IItem item)
        {
            yield break;
        }

        private void Start()
        {
            Floor = GameManager.CurrentFloor;
        }
    }
}