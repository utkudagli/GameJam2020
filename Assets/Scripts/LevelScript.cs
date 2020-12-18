using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerCharacterPrefab;
    public List<GameObject> doorSpawnPoints;
    public List<GameObject> doorPrefabs;

    private GameData gameData;
    void Start()
    {
        this.gameData = GameData.Get();
        this.gameData.InitializeGame(this);

        foreach(GameObject doorSpawnPoint in this.doorSpawnPoints)
        {
            int randomindex = Random.Range(0, doorPrefabs.Count);
            GameObject doorPrefab = doorPrefabs[randomindex];
            Instantiate(doorPrefab, doorSpawnPoint.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
