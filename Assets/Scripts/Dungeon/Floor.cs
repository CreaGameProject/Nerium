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
    // Dungeon.MakeFloorによって生成
    public class Floor
    {
        private readonly Room[] rooms;
        private readonly List<IDungeonCharacter> characters;
        private readonly List<IItem> items;
        public Dictionary<EnemyID, float> EnemyTable { get; }
        public float EnemyPopProbability { get; set; }

        public int Number { get; }

        public int Turn { get; private set; }

        public int MaxTurn { get; }

        public TerrainType[,] Terrains { get; set; }

        public IEnumerable<Room> Rooms => rooms;

        public IEnumerable<IDungeonCharacter> Characters => characters;

        public IEnumerable<IDungeonCharacter> Enemies => characters.Skip(1);

        public Player Player => GameManager.GetPlayer;//characters.First() as Player;

        public IEnumerable<IItem> Items => items;

        public Cell this[int x, int y] => this[new Vector2Int(x,y)];

        public Cell this[Vector2Int v] => new Cell(this, v);

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
            Turn = 1;
        }

        public IEnumerator NextTurn()
        {
            // ファストムーブ、クイックムーブなど考慮すべき点が多々あり
            ActCategory playerActCat = ActCategory.Wait; // 仮代入
            yield return new WaitUntil(() => Player.Command(ref playerActCat));

            var actionActors = new List<IDungeonCharacter>();
            var moveActors = new List<IDungeonCharacter>();
            var waitActors = new List<IDungeonCharacter>();

            foreach (var character in Enemies)
            {
                var cat = character.RequestActCategory();
                switch (cat)
                {
                    case ActCategory.Action:
                        actionActors.Add(character);
                        break;
                    case ActCategory.Move:
                        moveActors.Add(character);
                        break;
                    case ActCategory.Wait:
                        waitActors.Add(character);
                        break;
                }
            }

            yield return Player.Turn(playerActCat);

            if (playerActCat == ActCategory.Move)
            { // プレイヤーが移動を行った場合
                foreach (var actor in moveActors)
                {
                    yield return actor.Turn(ActCategory.Move);
                }

                yield return new WaitForSeconds(Settings.StepTime);

                foreach (var actor in actionActors)
                {
                    yield return actor.Turn(ActCategory.Action);
                }
            }
            else
            { // プレイヤーが移動以外を行った場合
                foreach (var actor in actionActors)
                {
                    yield return actor.Turn(ActCategory.Action);
                }

                foreach (var actor in moveActors)
                {
                    yield return actor.Turn(ActCategory.Move);
                }

                yield return new WaitForSeconds(Settings.StepTime);
            }

            foreach (var actor in waitActors)
            {
                yield return actor.Turn(ActCategory.Wait);
            }

            // yield return null;

            PopEnemies();
            Turn++;
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

        public void Kill()
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
