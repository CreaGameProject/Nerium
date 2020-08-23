using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters;
using Assets.Scripts.Items;
using Assets.Scripts.Systems;
using GridMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using UnityEngine.XR.WSA.Persistence;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Dungeon
{
    /// <summary>
    /// Dungeonオブジェクトによって生成される
    /// Floor上のオブジェクトに対して空間的な支配権を持つ
    /// </summary>
    public class Floor
    {
        private readonly Room[] rooms; // 各部屋
        private readonly List<IDungeonCharacter> characters; // キャラクターレイヤーのオブジェクト
        private readonly List<IItem> items; // アイテムレイヤーのオブジェクト
        public Dictionary<EnemyID, float> EnemyTable { get; } // 敵の出現テーブル
        public float EnemyPopProbability { get; set; } // 敵の出現確率

        public int Number { get; } // 階層番号
        
        public int MaxTurn { get; } // ターン制限

        public TerrainType[,] Terrains { get; set; } // 地形の2次元配列

        public IEnumerable<Room> Rooms => rooms; // 部屋のデータを取得するためのgetプロパティ

        public IEnumerable<IDungeonCharacter> Characters => characters; // キャラクターレイヤーのオブジェクトのデータを取得するためのgetプロパティ

        public IEnumerable<BattleCharacter> Enemies => characters.Skip(1).OfType<BattleCharacter>().ToList(); // 敵キャラクターを取得するためのgetプロパティ

        public Player Player => characters.First() as Player; // 主人公を取得するためのgetプロパティ

        public IEnumerable<IItem> Items => items; // アイテムレイヤーのオブジェクトのデータを取得するためのgetプロパティ

        public Cell this[int x, int y] => this[new Vector2Int(x,y)]; // マスのデータを取得

        public Cell this[Vector2Int v] => new Cell(this, v); // マスのデータを取得

        public Floor(int number, int maxTurn, TerrainType[,] terrains, IEnumerable<Room> rooms, Player player)
        {
            Number = number;
            MaxTurn = maxTurn;
            this.Terrains = terrains;
            this.rooms = rooms.ToArray();
            characters = new List<IDungeonCharacter>(){player};
            EnemyTable = new Dictionary<EnemyID, float>();
            items = new List<IItem>();
            EnemyPopProbability = 0;
        }

        public IEnumerable<IDungeonCharacter> Throw(IItem item, Vector2Int basePosition, Vector2Int step,
            bool penetrable)
        {
            yield break;
        }

        public IDungeonCharacter Throw(IDungeonCharacter character, Vector2Int basePosition, Vector2Int destination)
        {
            return null;
        }

        public bool Put(IItem item, Vector2Int position)
        {
            return false;
        }

        public bool Summon(IDungeonCharacter character, Vector2Int position)
        {
            // ↓インスタンシエイトどうするの？
            // if (InRange(position))
            // {
            //     character.Floor = this;
            //     characters.Add(character);
            //     character.Position = position;
            // }
            return false;
        }

        public IItem Pick(Vector2Int position)
        {
            IItem tmpItem = null;
            foreach (var item in items)
                if (item.Position == position)
                {
                    items.Remove(item);
                    tmpItem = item;
                }

            return tmpItem;
        }

        public void Kill(IDungeonCharacter character)
        {
            
        }

        public bool InRange(Vector2Int position)
        {
            return position.x >= 0 &&
                   position.y >= 0 &&
                   position.x < Terrains.GetLength(0) &&
                   position.y < Terrains.GetLength(1);
        }

        public void PopEnemies()
        {
            if (rooms.Length < 1) return;
            if (EnemyPopProbability < Random.value) return;

            // 召喚する部屋を求める
            Room popRoom;
            if (rooms.Length == 1)
            {
                // 主にフロアモンスターハウス・大部屋使用時用
                // アルファ版で実装の予定はない
                return;
            }
            else
            {
                var candRooms = rooms.Where(x => !x.InRange(GameManager.GetPlayer.Position));
                var rand = Random.Range(0, candRooms.Count());
                popRoom = candRooms.ElementAt(rand);
            }
            
            // 召喚するキャラクターを決める
            var prob = Random.Range(0, EnemyTable.Values.Sum());
            foreach (var enemy in EnemyTable)
            {
                prob -= enemy.Value;
                if (prob <= 0)
                {
                    popRoom.RandomPopEnemy(IdTranslator.GetCharacter(enemy.Key));
                }
            }
        }
    }
}
