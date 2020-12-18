using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : ScriptableObject
{
    static private GameData instance;

    public GameObject playerCharacter;
    public GameObject playerController;

    bool bIsGameInitialized = false;

    public void InitializeGame(LevelScript script)
    {
        if(bIsGameInitialized)
        {
            return;
        }
        bIsGameInitialized = true;
        if (playerCharacter == null)
        {
            playerCharacter = Instantiate(script.playerCharacterPrefab, new Vector2(0, 0), Quaternion.identity);
            DontDestroyOnLoad(playerCharacter);
        }
    }
    public static GameData Get()
    {
        if (instance == null)
        {
            instance = ScriptableObject.CreateInstance<GameData>();
        }
        return instance;
    }

    public void LoadNextRoom()
    {
        playerCharacter.transform.position = new Vector2(0, 0);
        SceneManager.LoadScene("RandomRoomScene");
    }
}
