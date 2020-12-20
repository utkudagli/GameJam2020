using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject wantToQuitMenu;
    [SerializeField] private bool isPaused;
    private bool isQuitMenuOpened;

    private Button exit;
    private Button exitYes;
    private Button exitNo;

    private void Awake()
    {
        exit = transform.Find("PauseMenu").Find("Exit").GetComponent<Button>();
        exitYes = transform.Find("WantToQuitBG").Find("Yes").GetComponent<Button>();
        exitNo = transform.Find("WantToQuitBG").Find("No").GetComponent<Button>();
        exit.onClick.AddListener(WantToQuit);
    }

    private void WantToQuit()
    {
        isQuitMenuOpened = true;
        wantToQuitMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        exitYes.onClick.AddListener(Exit);
        exitNo.onClick.AddListener(OnClickNo);
    }
    private void Exit()
    {
        Application.LoadLevel("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isQuitMenuOpened)
            {
                isPaused = !isPaused;
            }
        }
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
    private void OnClickNo()
    {
        wantToQuitMenu.SetActive(false);
        isQuitMenuOpened = false;
    }

}
