using System;
using System.Collections.Generic;
using System.Linq;
using Systems;
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
        // アイテム・キャラクター・地形を返す
        public (IItem item, IDungeonCharacter character, TerrainType terrain) this[int x, int y] =>
            (itemLayer[x,y], characterLayer[x,y], Terrains[x,y]);
        public (IItem item, IDungeonCharacter character, TerrainType terrain) this[Vector2Int v] =>
            (itemLayer[v.x,v.y], characterLayer[v.x,v.y], Terrains[v.x,v.y]);

        /// <summary> 階層番号 </summary>
        public int Number { get; } 
        
        /// <summary> 最大ターン数 </summary>
        public int MaxTurn { get; } 

        /// <summary> 敵の出現テーブル </summary>
        public Dictionary<EnemyID, float> EnemyTable { get; }
        
        /// <summary> 敵の出現確率 </summary>
        public float EnemyPopProbability { get; set; }

        /// <summary> 地形データ </summary>
        public TerrainType[,] Terrains { get; }

        /// <summary> 部屋のリスト </summary>
        public List<Room> Rooms { get; }
        

        private readonly ItemLayer itemLayer;

        private readonly CharacterLayer characterLayer;
        
        public Floor(int sizeX, int sizeY, int number, int maxTurn, GameObject player, Vector2Int playerPos)
        {
            Number = number;
            MaxTurn = maxTurn;
            characterLayer = new CharacterLayer(this, sizeX, sizeY);
            itemLayer = new ItemLayer(this, sizeX, sizeY);
            Terrains = new TerrainType[sizeX, sizeY];
            Rooms = new List<Room>();
            Summon(player, playerPos.x, playerPos.y);
            
            EnemyTable = new Dictionary<EnemyID, float>();
            EnemyPopProbability = 0;
        }

        #region フロア自体の構造に対するアクション

        // フロアに部屋を追加する
        public void AddRoom(int startX, int endX, int startY, int endY)
        {
            if(startX > endX) {
                var tmp = startX; startX = endX; endX = tmp;
            }

            if (startY > endY) {
                var tmp = startY; startY = endY; endY = startY;
            }

            var room = new Room(this, new Vector2Int(startX, startY), new Vector2Int(endX, endY));
            Rooms.Add(room);
            SetTerrain(startX, endX, startY, endY, TerrainType.Floor);
        }
        
        // フロア内の地形を変更する
        public void SetTerrain(int startX, int endX, int startY, int endY, TerrainType terrainType)
        {
            for (int i = startX; i <= endX; i++)
            for (int j = startY; j <= endY; j++)
                Terrains[i, j] = terrainType;
        }
        
        #endregion
        
        
        
        #region フロアのオブジェクトに対するアクション

        public List<IItem> Items => itemLayer.ToList();

        public List<IDungeonCharacter> Characters => characterLayer.ToList();

        public List<Enemy> Enemies => characterLayer.OfType<Enemy>().ToList();
        
        public Player Player => characterLayer.First() as Player;
        
        public List<IDungeonCharacter> Throw(IItem item, Vector2Int basePosition, Vector2Int step,
            bool penetrable)
        {
            return null;
        }

        public IDungeonCharacter Throw(IDungeonCharacter character, Vector2Int basePosition, Vector2Int destination)
        {
            return null;
        }

        public bool Put(IItem item, int x, int y) => itemLayer.Put(item, x, y);
        public IItem Pick(int x, int y) => itemLayer.Pick(x, y);
        public bool Summon(GameObject original, int x, int y) => characterLayer.Summon(original, x, y);
        public bool Kill(IDungeonCharacter character) => characterLayer.Kill(character);

        #endregion
        


        public bool InRange(Vector2Int vec)
        {
            return vec.x >= 0 && vec.y >= 0 &&
                   vec.x < Terrains.GetLength(0) && vec.y < Terrains.GetLength(1);
        }

        public void PopEnemies()
        {
            if (Rooms.Count < 1) return;
            if (EnemyPopProbability < Random.value) return;

            // 召喚する部屋を求める
            Room popRoom;
            if (Rooms.Count == 1)
            {
                // 主にフロアモンスターハウス・大部屋使用時用
                // アルファ版で実装の予定はない
                return;
            }
            else
            {
                var candRooms = Rooms.Where(x => !x.InRange(GameManager.GetPlayer.Position));
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
                    // popRoom.RandomPopEnemy(IdTranslator.GetCharacter(enemy.Key));
                }
            }
        }
    }
}
