using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelOpen : MonoBehaviour
{

    GameObject secondPanel;
    private bool panelFlag = false;
    manuManager manuManager;

    // Start is called before the first frame update
    void Start()
    {
        secondPanel = GameObject.Find("SecondPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (panelFlag)
        {
            panelCommand();
        }
    }

    public void secondOpen(string name)
    {
        secondPanel.GetComponent<CanvasGroup>().alpha = 1;
        panelFlag = true;

        switch (name)
        {
            case "field":

                //secondPanel.GetComponent<Image>().sprite = ;
                break;

            case "status":
                break;

            case "explain":
                break;
        }
    }

    private void panelCommand()
    {
        if (Input.anyKeyDown)
        {
            closePanel();
        }
    }

    

    private void closePanel()
    {
        secondPanel.GetComponent<CanvasGroup>().alpha = 0;
        panelFlag = false;

        if (manuManager ==null)
        {
            manuManager = manuManager.Instance;
        }

        manuManager.unRockFirstPanel();
    }

}
