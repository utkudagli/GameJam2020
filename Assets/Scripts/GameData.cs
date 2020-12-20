﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : ScriptableObject
{
    static private GameData instance = null;

    public float gameTimer;
    public float enemyPointGainTimer;
    public int enemyPoints;

    public float cratePointGainTimer;
    public int cratePoints;

    public GameObject playerCharacter;
    public GameObject playerController;
    public GameObject hud;

    public EDirection nextSpawnPointDirection = EDirection.DOWN;

    bool bIsGameInitialized = false;
    public TransitionScript TransitionScript;



    public void InitializeGame(LevelScript script)
    {
        if(this.bIsGameInitialized)
        {
            return;
        }
        if(this.hud == null)
        {
            hud = Instantiate(script.hudPrefab, new Vector2(0, 0), Quaternion.identity);
            DontDestroyOnLoad(hud);
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
            //spawn player
        foreach (GameObject obj in script.playerSpawnPoints)
        {
            PlayerSpawnPointScript pss = obj.GetComponent<PlayerSpawnPointScript>();
            if (pss.location == this.nextSpawnPointDirection)
            {
                playerCharacter.transform.position = obj.transform.position;
            }
        }
        
        //spend points to spawn enemies

        while (enemyPoints > 1)
        {
            int alreadyExisting = script.spawnedEnemies.Count;
            if ( alreadyExisting > 4)
            {
                if(Random.value < 0.1f - (alreadyExisting - 4) * 0.01f)
                {
                    break;
                }
            }
            Vector2 spawnLocation = FindRandomSpawnPoint(script);
            if(Vector2.Distance(spawnLocation, playerCharacter.transform.position) < 3)
            {
                spawnLocation = -spawnLocation;
            }
            if (enemyPoints == 2)
            {
                GameObject cyberMage = Instantiate(script.CyberMagePrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(cyberMage);
                enemyPoints -= 2;
                break; ;
            }
            //select 2 or 3
            int spend = Random.Range(2, 4);
            if (spend == 2)
            {
                GameObject cyberMage = Instantiate(script.CyberMagePrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(cyberMage);
                enemyPoints -= 2;
            }
            else if (spend == 3)
            {
                GameObject thumper = Instantiate(script.ThumperPrefab, spawnLocation, Quaternion.identity);
                script.spawnedEnemies.Add(thumper);
                enemyPoints -= 3;
            }
        }

        //TODO : spend points to spawn health crates

        while(this.cratePoints > 0)
        {
            if (Random.value < 0.2f)
            {
                break;
            }
            Vector2 spawnLocation = FindRandomSpawnPoint(script);
            GameObject crate = Instantiate(script.cratePrefab, spawnLocation, Quaternion.identity);
            script.spawnedCrates.Add(crate);
            cratePoints -= 1;
        }
        cratePoints = 0;

       // playerCharacter.SetActive(true);
    }
    public static GameData Get()
    {
        if (instance == null)
        {
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
       // playerCharacter.SetActive(false);
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
        this.enemyPointGainTimer -= Time.deltaTime;
        if (this.enemyPointGainTimer < 0)
        {
            //cap points at 50 for now
            enemyPoints = enemyPoints < 50 ? enemyPoints + 1 : enemyPoints;
            this.enemyPointGainTimer = Mathf.Sqrt(enemyPoints);
        }
        this.cratePointGainTimer -= Time.deltaTime;
        if (this.cratePointGainTimer < 0)
        {
            cratePoints = cratePoints < 2 ? cratePoints + 1 : cratePoints;
            this.cratePointGainTimer = Mathf.Sqrt(cratePoints);
        }
    }

    public void ShutDown()
    {
        instance = null;
        Object.Destroy(hud);
        hud = null;
    }
    public void Awake()
    {
        TransitionScript = FindObjectOfType<TransitionScript>();
    }
}
