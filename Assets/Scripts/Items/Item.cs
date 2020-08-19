using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Items;
using UnityEngine;

public enum ItemID
{

}

public abstract class Item : IItem
{
    protected Dictionary<string, Func<IEnumerator>> choices = new Dictionary<string, Func<IEnumerator>>();
    
    // アイテム欄から選択したときの選択肢を与える
    public IEnumerable<string> GetChoices()
    {
        return choices.Keys;
    }

    // あるアイテムに対してコマンドを実行する(使う、投げるなど)
    public string Name { get; }
    public void SetNickName(string nickName)
    {
        throw new NotImplementedException();
    }

    public Vector2Int Position { get; set; }
    public IEnumerator SteppedBy(BattleCharacter character)
    {
        throw new NotImplementedException();
    }

    public IEnumerator Hit(BattleCharacter character)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetMenuItem()
    {
        throw new NotImplementedException();
    }

    public IEnumerator Use(string choice)
    {
        if(choices.ContainsKey(choice))
            yield return choices[choice];
        else
            Debug.LogError("存在しない選択肢が選ばれました。");
    }

}
