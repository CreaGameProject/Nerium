using System.Collections.Generic;
using System.Linq;
using Systems;
using Assets.Scripts.Systems;
using Characters;
using Dungeon.Dungeons;
using GridMap;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeon
{
    /// <summary>
    /// Dungeonオブジェクトによって生成される
    /// Floor上のオブジェクトに対して空間的な支配権を持つ
    /// </summary>
    public class Floor
    {
        
#region Variables & Properties

        /// <summary>
        /// 階層番号
        /// </summary>
        public int Number { get; } 
        
        /// <summary>
        /// 最大ターン数
        /// </summary>
        public int MaxTurn { get; } 

        /// <summary>
        /// 敵の出現テーブル
        /// </summary>
        public Dictionary<EnemyID, float> EnemyTable { get; }
        
        /// <summary>
        /// 敵の出現確率
        /// </summary>
        public float EnemyPopProbability { get; set; }

        /// <summary>
        /// 地形データ
        /// </summary>
        public GridMap<TerrainType> Terrains { get; set; }

        /// <summary>
        /// 部屋のリスト
        /// </summary>
        private readonly List<Room> rooms;

        /// <summary>
        /// 部屋を取得するプロパティ
        /// </summary>
        public IEnumerable<Room> Rooms => rooms;
        
        /// <summary>
        /// キャラクターレイヤーのオブジェクト
        /// </summary>
        private readonly List<IDungeonCharacter> characters; // 

        /// <summary>
        /// キャラクターレイヤーのオブジェクトを取得するためのgetプロパティ
        /// </summary>
        public IEnumerable<IDungeonCharacter> Characters => characters;
        
        /// <summary>
        /// 敵キャラクターを取得するためのgetプロパティ
        /// </summary>
        public IEnumerable<BattleCharacter> Enemies => characters.Skip(1).OfType<BattleCharacter>().ToList(); 
        
        /// <summary>
        /// 主人公を取得するためのgetプロパティ
        /// </summary>
        public Player Player => characters.First() as Player; 
        
        /// <summary>
        /// アイテムレイヤーのオブジェクト
        /// </summary>
        private readonly List<IItem> items; // アイテムレイヤーのオブジェクト
        
        /// <summary>
        /// アイテムレイヤーのオブジェクトを取得するためのgetプロパティ
        /// </summary>
        public IEnumerable<IItem> Items => items;
        
#endregion
        
        /// <summary>
        /// インデクサ by int
        /// </summary>
        public Cell this[int x, int y] => this[new Vector2Int(x,y)]; // マスのデータを取得
        
        /// <summary>
        /// インデクサ by vector
        /// </summary>
        public Cell this[Vector2Int v] => new Cell(this, v); // マスのデータを取得

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="number">階数</param>
        /// <param name="maxTurn">最大ターン数</param>
        /// <param name="player">プレイヤーオブジェクト</param>
        /// <param name="size">フロアサイズ</param>
        public Floor(int number, int maxTurn, Player player, Vector2Int size)
        {
            Number = number;
            MaxTurn = maxTurn;
            characters = new List<IDungeonCharacter>(){player};
            items = new List<IItem>();
            rooms = new List<Room>();
            Terrains = new GridMap<TerrainType>(size, v => TerrainType.Wall);
            
            EnemyTable = new Dictionary<EnemyID, float>();
            EnemyPopProbability = 0;
        }

        /// <summary>
        /// フロアに部屋を追加する
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="endX"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        public void AddRoom(int startX, int endX, int startY, int endY)
        {
            if(startX > endX) {
                var tmp = startX; startX = endX; endX = tmp;
            }

            if (startY > endY) {
                var tmp = startY; startY = endY; endY = startY;
            }

            var room = new Room(this, new Vector2Int(startX, startY), new Vector2Int(endX, endY));
            rooms.Add(room);
            room.SetRoomTerrain();
        }

        public List<IDungeonCharacter> Throw(IItem item, Vector2Int basePosition, Vector2Int step,
            bool penetrable)
        {
            return null;
        }

        public IDungeonCharacter Throw(IDungeonCharacter character, Vector2Int basePosition, Vector2Int destination)
        {
            return null;
        }

        public bool Put(IItem item, Vector2Int position)
        {
            return false;
        }

        public bool Summon(GameObject prefab, Vector2Int position)
        {
            // ↓インスタンシエイトどうするの？
            // if (InRange(position))
            // {
            //     character.Floor = this;
            //     characters.Add(character);
            //     character.Position = position;
            // }
            if (InRange(position))
            {
                var obj = GameObject.Instantiate(prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
                var cmp = obj.GetComponent<IDungeonCharacter>();
                cmp.Floor = this;
                characters.Add(cmp);
            }
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
            return Terrains.InRange(position);
        }

        public void PopEnemies()
        {
            if (rooms.Count < 1) return;
            if (EnemyPopProbability < Random.value) return;

            // 召喚する部屋を求める
            Room popRoom;
            if (rooms.Count == 1)
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
