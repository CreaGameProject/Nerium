using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    GameObject itemPanel;
    private bool panelFlag = false;

    GameObject selectArrow;
    private GameObject[] itemTexts;
    manuManager manuManager;
    private int selectedNum = 0;
    private int pageNum = 0;

    private int textWidth = 100;
    private int textYinterval = 45;

    // Start is called before the first frame update
    void Start()
    {
        itemPanel = GameObject.Find("itemPanel");


        itemTexts = new GameObject[7];

        for (int i =0;i<7;i++)
        {
            itemTexts[i] = GameObject.Find("itemText"+i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (panelFlag ==true)
        {
            itemCommand();
        }
    }

    private void itemCommand()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pageNum <7)
            {
                pageNum++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (pageNum >0)
            {
                pageNum--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (selectedNum >0) selectedNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (selectedNum < 6) selectedNum++;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            closePanel();
        }


        if (Input.anyKeyDown)
        {
            selectArrow.transform.position = new Vector3(itemTexts[selectedNum].transform.position.x - (textWidth / 2 + 5) - 10,
                                                    itemTexts[selectedNum].transform.position.y,
                                                    0);
        }
        
    }

    public void itemPanelOpen(){

        itemPanel.GetComponent<CanvasGroup>().alpha = 1;
        panelFlag = true;
        selectedNum = 0;

        if (manuManager == null)
        {
            manuManager = manuManager.Instance;

            for (int i = 0; i < itemTexts.Length; i++)
            {
                
                itemTexts[i].transform.localPosition = new Vector3(0,125-textYinterval*i,0);
            }
        }

        selectArrow = manuManager.sendArrow();

        UnityEngine.Debug.Log(selectArrow.transform.position);
        selectArrow.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void closePanel()
    {
        itemPanel.GetComponent<CanvasGroup>().alpha = 0;
        panelFlag = false;

        

        manuManager.unRockFirstPanel();
    }
}
