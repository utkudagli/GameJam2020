using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : ScriptableObject
{
    static private GameData instance;

    public GameObject playerCharacter;
    public GameObject playerController;

    public EDirection nextSpawnPointDirection = EDirection.DOWN;

    bool bIsGameInitialized = false;

    public void InitializeGame(LevelScript script)
    {
        if(this.bIsGameInitialized)
        {
            return;
        }
        if (playerCharacter == null)
        {
            playerCharacter = Instantiate(script.playerCharacterPrefab, new Vector2(0, 0), Quaternion.identity);
            DontDestroyOnLoad(playerCharacter);
        }
        bIsGameInitialized = true;
    }

    public void NotifyNewSceneLoaded(LevelScript script)
    {
        foreach (GameObject obj in script.playerSpawnPoints)
        {
            PlayerSpawnPointScript pss = obj.GetComponent<PlayerSpawnPointScript>();
            if (pss.location == this.nextSpawnPointDirection)
            {
                playerCharacter.transform.position = obj.transform.position;
            }

        }
        playerCharacter.SetActive(true);
    }
    public static GameData Get()
    {
        if (instance == null)
        {
            instance = ScriptableObject.CreateInstance<GameData>();
        }
        return instance;
    }

    public void LoadNextRoom(EDirection comingFromDirection)
    {
        switch(comingFromDirection)
        {
            case EDirection.UP: nextSpawnPointDirection = EDirection.DOWN; break;
            case EDirection.DOWN: nextSpawnPointDirection = EDirection.UP; break;
            case EDirection.RIGHT: nextSpawnPointDirection = EDirection.LEFT; break;
            case EDirection.LEFT: nextSpawnPointDirection = EDirection.RIGHT; break;
            default: nextSpawnPointDirection = EDirection.DOWN; break;
        }
        playerCharacter.SetActive(false);
        SceneManager.LoadScene("RandomRoomScene");
    }
}
