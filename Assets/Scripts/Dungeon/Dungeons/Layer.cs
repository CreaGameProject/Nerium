using System.Collections;
using System.Collections.Generic;
using Systems;
using Characters;
using Items;
using UnityEngine;

namespace Dungeon.Dungeons
{
    /// <summary>
    /// フロア内のレイヤー
    /// </summary>
    /// <typeparam name="T">座標情報を持つ型</typeparam>
    public abstract class Layer<T>: IEnumerable<T> where T: IPositional
    {
        private List<T> list;
        private T[,] matrix;
        private Floor floor;

        protected Layer(Floor floor, int sizeX, int sizeY)
        {
            matrix = new T[sizeX,sizeY];
            list = new List<T>();
            this.floor = floor;
        }

        public T this[int x, int y] => matrix[x, y];

        protected bool Add(T item, int x, int y)
        {
            if (list.Contains(item) || matrix[x,y] != null)
                return false;
            
            list.Add(item);
            matrix[x, y] = item;
            return true;
        }

        protected bool Remove(T item)
        {
            var x = item.Position.x;
            var y = item.Position.y;
            if (!list.Contains(item) || matrix[x, y] == null)
                return false;

            list.Remove(item);
            matrix[x,y] = default;
            return true;
        }

        protected bool Remove(int x, int y)
        {
            if (!list.Exists(i => i.Position.Equals(new Vector2Int(x, y))) ||
                matrix[x, y] == null)
                return false;

            var item = matrix[x, y];
            list.Remove(item);
            matrix[x, y] = default;
            return true;
        }

        public bool InRange(Vector2Int vec) => InRange(vec.x, vec.y);
        public bool InRange(int x, int y) => x >= 0 && y >= 0 && x < matrix.GetLength(0) && y < matrix.GetLength(1);

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>) list).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ItemLayer : Layer<IItem>
    {
        public ItemLayer(Floor floor, int sizeX, int sizeY) : base(floor, sizeX, sizeY)
        {
            
        }

        public bool Put(IItem item, int x, int y)
        {
            if (!InRange(x, y))
                return false;
            
            // Instanciate()
            // Add
            
            return true;
        }

        public IItem Pick(int x, int y)
        {
            if (!InRange(x, y))
                return null;
            
            // Remove()
            // Destroy()

            return null;
        }
    }

    public class CharacterLayer: Layer<IDungeonCharacter>
    {
        public CharacterLayer(Floor floor, int sizeX, int sizeY) 
            : base(floor, sizeX, sizeY)
        {
        }

        public bool Summon(GameObject original, int x, int y)
        {
            return false;
        }

        public bool Kill(IDungeonCharacter character)
        {
            return true;
        }
    }
}