using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credis : MonoBehaviour
{
    private Button Back;
    private void Awake()
    {
        Back = transform.Find("Back").GetComponent<Button>();
        Back.onClick.AddListener(goBack);
    }
    private void goBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
