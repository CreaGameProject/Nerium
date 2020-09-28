using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace GridMap
{
    // 式木で使ってますがあんまり気にしないでください。
    using Binary = Func<ParameterExpression, ParameterExpression, BinaryExpression>;
    
    // おなじみ引数デリゲート
    public delegate void MOFunctionByVector(Vector2Int coordinate);
    public delegate void MOFunctionByInt(int x, int y);

    /// <summary>
    /// 一応下のGenericMapクラスがメインですが、IConvertibleを継承していないクラスで配列を作りたい場合こちらを使用してください。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnConvertibleMap<T> : IEnumerable<T>
    {
        /// <summary>
        /// 2次元配列のLength
        /// </summary>
        public Vector2Int Range { get; private set; }
        
        /// <summary>
        /// 2次元配列の本体
        /// </summary>
        public T[,] Matrix { get; }

        // コンストラクタ2種
        public UnConvertibleMap(Vector2Int range, Func<Vector2Int, T> function = null)
        {
            Range = range;
            Matrix = new T[range.x, range.y];
            if (function == null) return;
            MatrixOperate(v => Matrix[v.x, v.y] = function(v));
        }
        public UnConvertibleMap(T[,] matrix)
        {
            Range = new Vector2Int(matrix.GetLength(0), matrix.GetLength(1));
            Matrix = new T[Range.x, Range.y];
            Array.Copy(matrix, Matrix, matrix.Length);
        }
        
        // インデクサ2種
        public T this[int xIndex, int yIndex]
        {
            get => Matrix[xIndex, yIndex];
            set => Matrix[xIndex, yIndex] = value;
        }
        public T this[Vector2Int index]
        {
            get => Matrix[index.x, index.y];
            set => Matrix[index.x, index.y] = value;
        }
        
        // ただの2重ループ
        public void MatrixOperate(MOFunctionByInt function)
        {
            for(int x = 0; x < Range.x; x++)
                for(int y = 0; y < Range.y; y++)
                    function(x,y);
        }
        // Vector2Int使いたい人用
        public void MatrixOperate(MOFunctionByVector function) => MatrixOperate((x,y)=>function(new Vector2Int(x,y)));
        
        // 2つのマップのサイズがあってるか確認する
        protected bool RangeCheck(UnConvertibleMap<T> right)
        {
            if (this.Range == right.Range) return true;
            Debug.LogError("Augments have different ranges");
            return false;
        }

        // IEnumerableの制約で実装
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Range.x * Range.y; i++)
                yield return this[i % Range.x, i / Range.x];
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool InRange(Vector2Int vec)
        {
            return vec.x >= 0 && vec.y >= 0 && vec.x < Range.x && vec.y < Range.y;
        }
    }
    
    /// <summary>
    /// タイルマップに特化した2次元配列管理クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GridMap<T> : UnConvertibleMap<T> where T:IConvertible
    {
        // コンストラクタ2種
        public GridMap(Vector2Int range, Func<Vector2Int, T> function = null) : base(range, function){}
        public GridMap(T[,] matrix) : base(matrix){}

        // 演算子オーバーロード
        public static implicit operator GridMap<float>(GridMap<T> gridMap)=>new GridMap<float>(gridMap.Range, v=>(float)Convert.ToDouble(gridMap[v]));
        public static implicit operator GridMap<double>(GridMap<T> gridMap)=>new GridMap<double>(gridMap.Range, v=>Convert.ToDouble(gridMap[v]));
        public static implicit operator GridMap<decimal>(GridMap<T> gridMap)=>new GridMap<decimal>(gridMap.Range, v=>Convert.ToDecimal(gridMap[v]));
        public static explicit operator GridMap<int>(GridMap<T> gridMap)=>new GridMap<int>(gridMap.Range, v=>Convert.ToInt32(gridMap[v]));
        public static explicit operator GridMap<long>(GridMap<T> gridMap)=>new GridMap<long>(gridMap.Range, v=>Convert.ToInt64(gridMap[v]));
        public static GridMap<T> operator +(GridMap<T> l, GridMap<T> r) => l.RangeCheck(r) ? new GridMap<T>(l.Range, v => Operator(Expression.Add)(l[v], r[v])) : new GridMap<T>(l.Range);
        public static GridMap<T> operator -(GridMap<T> l, GridMap<T> r) => l.RangeCheck(r) ? new GridMap<T>(l.Range, v => Operator(Expression.Subtract)(l[v], r[v])) : new GridMap<T>(l.Range);
        public static GridMap<T> operator *(GridMap<T> l, GridMap<T> r) => l.RangeCheck(r) ? new GridMap<T>(l.Range, v => Operator(Expression.Multiply)(l[v], r[v])) : new GridMap<T>(l.Range);
        public static GridMap<T> operator /(GridMap<T> l, GridMap<T> r) => l.RangeCheck(r) ? new GridMap<T>(l.Range, v => Operator(Expression.Divide)(l[v], r[v])) : new GridMap<T>(l.Range);
        public static GridMap<T> operator %(GridMap<T> l, GridMap<T> r) => l.RangeCheck(r) ? new GridMap<T>(l.Range, v => Operator(Expression.Modulo)(l[v], r[v])) : new GridMap<T>(l.Range);
        public static GridMap<T> operator +(GridMap<T> l, T r) => new GridMap<T>(l.Range, v=>Operator(Expression.Add)(l[v], r));
        public static GridMap<T> operator -(GridMap<T> l, T r) => new GridMap<T>(l.Range, v=>Operator(Expression.Subtract)(l[v], r));
        public static GridMap<T> operator *(GridMap<T> l, T r) => new GridMap<T>(l.Range, v=>Operator(Expression.Multiply)(l[v], r));
        public static GridMap<T> operator /(GridMap<T> l, T r) => new GridMap<T>(l.Range, v=>Operator(Expression.Divide)(l[v], r));
        public static GridMap<T> operator %(GridMap<T> l, T r) => new GridMap<T>(l.Range, v=>Operator(Expression.Modulo)(l[v], r));
        
        // 一度使用された演算子のデリゲートが格納される
        private static readonly Dictionary<Binary, Func<T, T, T>> OpDictionary = new Dictionary<Binary, Func<T, T, T>>();

        // 演算子のデリゲート辞書から値取り出し 無ければ格納
        private static Func<T, T, T> Operator(Binary op)
        {
            if(!OpDictionary.ContainsKey(op))
                OpDictionary.Add(op, CreateOperator(op));
            return OpDictionary[op];
        }
        
        // 演算子オーバーロード用の式木
        private static Func<T, T, T> CreateOperator(Binary op)
        {
            var l = Expression.Parameter(typeof(T), "l");
            var r = Expression.Parameter(typeof(T), "r");
            var expression = Expression.Lambda<Func<T, T, T>>(op(l, r), l, r);
            return expression.Compile();
        }

        // 以降おまけ
        /// <summary>
        /// 内積計算
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static GridMap<T> Dot(GridMap<T> left, GridMap<T> right)
        {
            var result = new GridMap<T>(new Vector2Int(right.Range.x, left.Range.y));
            if (left.Range.x != right.Range.y) return result;
            result.MatrixOperate((x1, y1) =>
            {
                for (int i = 0; i < left.Range.x; i++)
                    result[x1, y1] = Operator(Expression.Add)(result[x1, y1], Operator(Expression.Multiply)(left[i, y1], right[x1, i]));
            });
            return result;
        }
    }
}
