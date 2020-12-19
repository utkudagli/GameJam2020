using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public static int countValue = 0;
    Text counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = transform.Find("Canvas").Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    public void Increase(int value)
    {
        counter.text = "" + value;
    }
}
