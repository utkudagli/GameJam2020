using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerCharacterPrefab;

    private GameData gameData;
    void Start()
    {
        this.gameData = GameData.Get();
        this.gameData.InitializeGame(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
