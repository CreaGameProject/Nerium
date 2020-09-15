using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

public class manuManager : SingletonMonoBehaviour<manuManager>
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
    [SerializeField] GameObject[] texts;
    [SerializeField] GameObject selectArrow;

    private int selectedNum = 0;
    private int textsSize;

    private int textWidth = 100;
    private int textXinterval = 130;
    private int textYinterval = 50;

    private string[] selectName;
    private bool selectFlag = false;
    private int selectFlagAssist = 0;


    // Start is called before the first frame update
    void Start()
    {    
        textsSize = texts.Length;
        manuSet();
        openManu();
    }


    private void manuSet()
    {
        selectName = new string[textsSize];

        for (int i = 0; i < textsSize; i++)
        {
            selectName[i] = texts[i].name;
            texts[i].transform.localPosition = new Vector3(10 - textXinterval / 2 + textXinterval * (i % 2),
                                                            80 - (textYinterval * (i / 2)),
                                                            0);
        }
    }

    void openManu()
    {
        firstPanel.GetComponent<CanvasGroup>().alpha = 1;
        selectArrow.GetComponent<CanvasGroup>().alpha = 1;
        selectedNum = 0;
        selectAssist = 1;
        selectFlag = true;

        selectArrow.transform.position = new Vector3(texts[0].transform.position.x - (textWidth / 2 + 5) - 30,
                                                     texts[0].transform.position.y,
                                                     0);
    }

    void closeManu()
    {
        firstPanel.GetComponent<CanvasGroup>().alpha = 0;
        selectArrow.GetComponent<CanvasGroup>().alpha = 0;
        selectFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (selectFlag ==true && selectFlagAssist ==1)  mainManuCommand();

        if (selectFlag == true) selectFlagAssist = 1;
    }

    private int selectAssist = 0;

    void mainManuCommand()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedNum += -1 * (selectedNum % 2);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedNum += 1 * (1 - selectedNum % 2 - (selectedNum / (textsSize - 1)));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectAssist = selectedNum / 2;
            if (selectAssist != 0) selectedNum += -2;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedNum += 2 * (1 - (selectedNum+2)/textsSize);

        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            openAction(selectedNum);
        } else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            closeManu();
        }


        if (Input.anyKeyDown)
        {
            selectArrow.transform.position = new Vector3(texts[selectedNum].transform.position.x - (textWidth / 2 + 5) - 30,
                                                    texts[selectedNum].transform.position.y,
                                                    0);
        }
    }

    void openAction(int n)
    {
        rockFirstPanel();

        switch (selectName[n])
        {
            case "serchText":

                UnityEngine.Debug.Log("調べる開始");
                closeManu();
                //呼び出しserch
                break;

            case "itemText":

                firstPanel.GetComponent<itemManager>().itemPanelOpen();
                //呼び出しitem
                break;

            case "fieldText":

                firstPanel.GetComponent<panelOpen>().secondOpen("field");
                //呼び出しsecond(field)

                break;

            case "statusText":

                firstPanel.GetComponent<panelOpen>().secondOpen("status");
                //呼び出しsecond(field)
                break;

            case "explainText":

                firstPanel.GetComponent<panelOpen>().secondOpen("explain");
                //呼び出しsecond(field)
                break;

            case "logText":


                //呼び出しlog
                break;

            case "ritaiaText":

                
                //呼び出しritaia
                break;
        } 
    }


    void rockFirstPanel()
    {
        selectFlagAssist = 2;
        selectFlag = false;
        //panelの色変更
    }

    
    public void unRockFirstPanel()
    {
        selectedNum = 0;
        selectArrow.transform.position = new Vector3(texts[0].transform.position.x - (textWidth / 2 + 5) - 10,
                                                     texts[0].transform.position.y,
                                                     0);
        selectFlag = true;
    }

    public GameObject sendArrow()
    {
        return selectArrow;
    }
}
