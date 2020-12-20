using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;
    private int i = -1;
    private Image messageBackground;
    private Text toContinue;

    private void Awake()
    {
        messageText = transform.Find("message").Find("Text").GetComponent<Text>();
        messageBackground = transform.Find("message").Find("Image").GetComponent<Image>();
        toContinue = transform.Find("message").Find("toContinue").GetComponent<Text>();
        Application.targetFrameRate = 100;

        
    }
    private void NextMsg(string[] messageArr)
    {
        i++;
        if (i <= messageArr.Length)
        {
            if(i == messageArr.Length)
            {
                messageText.gameObject.SetActive(false);
                messageBackground.gameObject.SetActive(false);
                toContinue.gameObject.SetActive(false);
            }
            else
            {
                string msg = messageArr[i];
                textWriter.AddWriter(messageText, msg, .025f);
            }
        }
    }
    private void setVisible(string[] messageArr) {
        messageText.gameObject.SetActive(true);
        messageBackground.gameObject.SetActive(true);
        toContinue.gameObject.SetActive(true);

        InvokeTextBox(messageArr);
    }
    private void InvokeTextBox(string[] messageArr)
    {
        Button getNextMsg = transform.Find("message").GetComponent<Button>();
        getNextMsg.onClick.AddListener(delegate { NextMsg(messageArr); });
    }
}
