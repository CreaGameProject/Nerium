using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logManager : SingletonMonoBehaviour<logManager>
{
    [SerializeField] GameObject logPanel;
    [SerializeField] Text logText;

    List<string> logList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logOpen()
    {
        logText.text = "";
        logPanel.GetComponent<CanvasGroup>().alpha = 1;

        for (int i =0; (i<logList.Count && i<30) ;i++)
        {
            logText.text += logList[i] + "\n";
        }
    }

    public void addLog(string newLog)
    {
        logList.Add(newLog);

        if (logList.Count > 30)
        {
            logList.RemoveAt(0);
        }
    }
}
