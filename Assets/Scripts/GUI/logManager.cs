using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logManager : MonoBehaviour
{
    [SerializeField] GameObject logPanel;
    [SerializeField] Text logText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void logOpen()
    {
        logPanel.GetComponent<CanvasGroup>().alpha = 1;


    }

    void addLog(string newLog)
    {

    }
}
