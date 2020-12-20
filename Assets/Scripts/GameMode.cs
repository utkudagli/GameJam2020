using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeScript : ScriptableObject
{
    // Start is called before the first frame update

    static private GameModeScript instance;
    public static GameModeScript Get()
    {
        if (instance == null)
        {
            instance = ScriptableObject.CreateInstance<GameModeScript>();
        }
        return instance;
    }

    public float gameTimer;
    public float pointTimer;
    public int points;

    public void Update()
    {
        this.gameTimer += Time.deltaTime;
        this.pointTimer -= Time.deltaTime;
        if(this.pointTimer < 0)
        {
            points++;
            this.pointTimer = 1f;
        }
    }

    public void NotifyNewRoomLoaded(LevelScript levelScript)
    {

    }
}
