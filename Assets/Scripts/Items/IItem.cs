using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Items
{
    interface IItem
    {
        // アイテム名
        string Name { get; }

        void SetNickName(string nickName);

        // アイテムの座標
        Vector2Int Position { get; set; }

        // 上に乗られたとき
        IEnumerator SteppedBy(IBattleCharacter character);

        // 投げられたアイテムがキャラクターにあたったとき
        IEnumerator Hit(IBattleCharacter character);

        // メニュー画面で選択されたときの選択肢リスト
        IEnumerable<string> GetMenuItem();

        // メニュー画面で「使う」「拾う」等のコマンドを実行されたとき
        IEnumerator Use(string command);
    }
}
