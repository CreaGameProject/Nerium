using System.Collections;
using System.Collections.Generic;
using Systems;
using Characters;
using UnityEngine;

namespace Items
{
    public interface IItem: IPositional
    {
        // アイテム名
        string Name { get; }

        // ニックネームを設定する
        void SetNickName(string nickName);

        // アイテムの座標
        Vector2Int Position { get; set; }
        
        // アイテムクラス
        Item Derived { get; }

        // 上に乗られたとき
        IEnumerator SteppedBy(BattleCharacter character);

        // 投げられたアイテムがキャラクターにあたったとき
        IEnumerator Hit(BattleCharacter character);

        // メニュー画面で選択されたときの選択肢リスト
        IEnumerable<string> GetMenuItem();

        // メニュー画面で「使う」「拾う」等のコマンドを実行されたとき
        IEnumerator Use(string command);
    }
}
