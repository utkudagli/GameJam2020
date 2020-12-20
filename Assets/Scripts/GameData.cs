using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : ScriptableObject
{
    static private GameData instance = null;

    public float gameTimer;
    public float pointTimer;
    public int points;

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
        //spend points to spawn enemies

        while(points > 1)
        {
            Vector2 spawnLocation = FindRandomSpawnPoint(script);
            if(points == 2)
            {
                GameObject cyberMage = Instantiate(script.CyberMagePrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(cyberMage);
                points -= 2;
                break; ;
            }
            //select 2 or 3
            int spend = Random.Range(2, 4);
            if(spend == 2)
            {
                GameObject cyberMage = Instantiate(script.CyberMagePrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(cyberMage);
                points -= 2;
            }
            else if(spend == 3)
            {
                GameObject thumper = Instantiate(script.ThumperPrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(thumper);
                points -= 3;
            }
        }
        //spawn player
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
            Debug.Log("Creating new gamedata instance");
            instance = ScriptableObject.CreateInstance<GameData>();
            DontDestroyOnLoad(instance);
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

    Vector2 FindRandomSpawnPoint(LevelScript script)
    {
        if(script.thingSpawnPoints.Count > 0)
        {
            return script.thingSpawnPoints[Random.Range(0, script.thingSpawnPoints.Count)];
        }
        return Vector2.zero;
    }
    public void Update()
    {
        this.gameTimer += Time.deltaTime;
        this.pointTimer -= Time.deltaTime;
        if (this.pointTimer < 0)
        {
            //cap points at 50 for now
            points = points < 50 ? points + 1 : points;
            this.pointTimer = Mathf.Sqrt(points);
        }
    }

    public void ShutDown()
    {
        Debug.Log("Shutting down gamedata");
        instance = null;
    }
}
