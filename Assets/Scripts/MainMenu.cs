using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Button newGame;
    private Button credits;
    private Button exit;

    private void Awake()
    {
        newGame = transform.Find("NewGame").GetComponent<Button>();
        credits = transform.Find("Credits").GetComponent<Button>();
        exit = transform.Find("Exit").GetComponent<Button>();

        newGame.onClick.AddListener(NewGame);
        credits.onClick.AddListener(Credits);
        exit.onClick.AddListener(Exit);
    }

    private void NewGame()
    {
        Application.LoadLevel("RandomRoomScene");
    }

    private void Credits()
    {
        Application.LoadLevel("Credits");
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
