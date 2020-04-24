using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manuManager : MonoBehaviour
{
    /*
     * メニューの項目
     *  アイテム
     *  フィールド
     *  ステータス確認
     *  操作説明
     *  （攻撃も入るかも）
     *  調べる
     *  メッセージ履歴（完全版）
     *  リタイア
     *  
     */

    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject firstText;
    [SerializeField] GameObject selectArrow;

    // Start is called before the first frame update
    void Start()
    {
        openManu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void openManu() {

        firstPanel.GetComponent<CanvasGroup>().alpha = 1;
        selectArrow.GetComponent<CanvasGroup>().alpha = 1;
    }
}
