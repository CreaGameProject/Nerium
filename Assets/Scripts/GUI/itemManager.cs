using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class itemManager : MonoBehaviour
{
    GameObject itemPanel;
    private bool panelFlag = false;

    GameObject selectArrow;
    private GameObject[] itemTexts;
    private List<string> itemName = new List<string>();
    [SerializeField] Text pageText;

    manuManager manuManager;
    private int selectedNum = 0;
    private int pageNum = 0;

    private int textWidth = 150;
    private int textYinterval = 40;

    //private List<item> itemList
    

    // Start is called before the first frame update
    void Start()
    {

        itemPanel = GameObject.Find("itemPanel");


        itemTexts = new GameObject[10];

        for (int i = 0;i<20;i++)
        {
            itemName.Add("退魔の種"+i);
        }

        for (int i =0;i<10;i++)
        {
            itemTexts[i] = GameObject.Find("itemText"+i);
            itemTexts[i].GetComponent<Text>().text = itemName[i];
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
       
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (pageNum < 1)
            {
                pageNum++;
                changePage(pageNum);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (pageNum > 0)
            {
                pageNum--;
                changePage(pageNum);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (selectedNum >0) selectedNum--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (selectedNum < 9) selectedNum++;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.Debug.Log(itemName[(pageNum)*10+selectedNum]);
            closePanel();
        }


        if (Input.anyKeyDown)
        {
            selectArrow.transform.position = new Vector3(itemTexts[selectedNum].transform.position.x - (textWidth / 2 + 5) - 20,
                                                    itemTexts[selectedNum].transform.position.y,
                                                    0);
        }
    }

    void changePage(int pageNum)
    {
        for (int i = 0; i < 10; i++)
        {
            itemTexts[i].GetComponent<Text>().text = itemName[i+(10*pageNum)];
        }

        pageText.text = "page"+(pageNum+1);
    }

    public void itemPanelOpen(){

        itemPanel.GetComponent<CanvasGroup>().alpha = 1;
        panelFlag = true;
        selectedNum = 0;
        pageNum = 0;
        pageText.text = "page1";

        if (manuManager == null)
        {
            manuManager = manuManager.Instance;

            for (int i = 0; i < itemTexts.Length; i++)
            {
                
                itemTexts[i].transform.localPosition = new Vector3(35,200-textYinterval*i,0);
            }
        }

        pageText.transform.localPosition = new Vector3(35, 200 - textYinterval * 10,0);

        selectArrow = manuManager.sendArrow();
        StartCoroutine("setArrow");
    }

    IEnumerator setArrow()
    {
        yield return new WaitForSeconds(0.1f);
        selectArrow.transform.position = new Vector3(itemTexts[0].transform.position.x - (textWidth / 2 + 5) - 20,
                                                    itemTexts[0].transform.position.y,
                                                    0);
    }

    private void closePanel()
    {
        itemPanel.GetComponent<CanvasGroup>().alpha = 0;
        panelFlag = false;

        

        manuManager.unRockFirstPanel();
    }
}
