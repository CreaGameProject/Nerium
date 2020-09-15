using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class messageManager : SingletonMonoBehaviour<messageManager>
{
    [SerializeField] GameObject messagePanel;
    [SerializeField] Text messageText;
    string gotText;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        messageOpen("aaaaaaaaaaaaaaaaaasssssssssssssssd");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //addLog(gotText);

        for (int i=0;i<gotText.Length;i++)
        {
            messageText.text += gotText.Substring(i, 1);

            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(0.01f);
    }
}
