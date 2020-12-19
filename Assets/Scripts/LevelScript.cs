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
    public List<Vector2> thingSpawnPoints;
    public Vector2 numThingSpawnPoints;
    private GameData gameData;

    public List<GameObject> spawnedEnemies;

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

        GameObject floorArea = GameObject.FindGameObjectWithTag("FloorArea");
        if(floorArea)
        {
            Bounds bounds = floorArea.GetComponent<SpriteRenderer>().bounds;
            Vector2 center = bounds.center;
            Vector2 extends = bounds.extents;
            for (int x = 1; x < numThingSpawnPoints.x; x++)
            {
                for (int y = 1; y < numThingSpawnPoints.y; y++)
                {
                    float offsetX = x * bounds.size.x / (numThingSpawnPoints.x);
                    float offsetY = y * bounds.size.y / (numThingSpawnPoints.y);
                    Vector2 point = new Vector2(center.x + offsetX - extends.x + Random.Range(-0.7f, 0.7f), center.y + offsetY - extends.y + Random.Range(-0.7f, 0.7f));

                    this.thingSpawnPoints.Add(point);
                }
            }
        }
        /*
        foreach(GameObject obj in thingSpawnPoints)
        {
            obj.transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1));
        }
        */

        this.gameData.NotifyNewSceneLoaded(this);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Vector2 vec in this.thingSpawnPoints)
        {

            Debug.DrawLine(vec, vec + Vector2.up * 0.2f, Color.green);
        }
        
    }

    void SpawnNPCs()
    {

    }
}
