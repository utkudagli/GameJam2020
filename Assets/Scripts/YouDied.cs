using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied : MonoBehaviour
{
    private Button backToMM;

    // Start is called before the first frame update
    private void Awake()
    {
        backToMM = transform.Find("BacktoMainMenu").GetComponent<Button>();
        backToMM.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
