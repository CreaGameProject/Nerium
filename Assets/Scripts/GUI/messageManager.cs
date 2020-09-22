using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class messageManager : SingletonMonoBehaviour<messageManager>
{
    [SerializeField] GameObject messagePanel;
    [SerializeField] Text messageText;
    string gotText;
    logManager logManager;
    bool textFlag = false;

    //Coroutine coroutine;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        logManager = logManager.Instance;
        messageOpen("aaaaaaaaaaaaaaaaaasssssssssssssssd");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && textFlag ==true)
        {
            textFlag = false;
            StopCoroutine("messageWrite");

            messageText.text = gotText;      
        }
    }

    void messageOpen(string text)
    {
        messagePanel.GetComponent<CanvasGroup>().alpha = 1;
        messageText.text = "";

        gotText = text;

        StartCoroutine("messageWrite");
    }

    IEnumerator messageWrite()
    {
        logManager.addLog(gotText);
        textFlag = true;

        for (int i=0;i<gotText.Length;i++)
        {
            messageText.text += gotText.Substring(i, 1);

            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(0.1f);
        textFlag = false;
        messagePanel.GetComponent<CanvasGroup>().alpha = 0;
    }
}
