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
    public List<GameObject> playerSpawnPoints;

    private GameData gameData;
    void Start()
    {
        this.gameData = GameData.Get();
        this.gameData.InitializeGame(this);
        //spawn doors

        foreach(GameObject doorSpawnPoint in this.doorSpawnPoints)
        {
            DoorSpawnPointScript dss = doorSpawnPoint.GetComponent<DoorSpawnPointScript>();
            if(dss.direction == this.gameData.nextSpawnPointDirection)
            {
                //we are coming from here, dont spawn a door
            }
            else
            {
                int randomindex = Random.Range(0, doorPrefabs.Count);
                GameObject doorPrefab = doorPrefabs[randomindex];
                GameObject door = Instantiate(doorPrefab, doorSpawnPoint.transform);
                door.GetComponent<DoorScript>().doorDirection = dss.direction;
            }
        }
        this.gameData.NotifyNewSceneLoaded(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
