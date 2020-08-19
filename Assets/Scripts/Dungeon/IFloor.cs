using System.Collections.Generic;
using Assets.Scripts.Characters;

namespace Assets.Scripts.Dungeon
{
    /// <summary>
    /// 一階層分の情報を保持するインターフェース。これ自体が行動を起こすことはないが、様々なクラスからアクセスされるためのインターフェースを提供する。
    /// </summary>
    public interface IFloor
    {
        Room[] Rooms { get; set; }
        
        // Character
        List<IDungeonCharacter> Characters { get; }
        
        
    }
}