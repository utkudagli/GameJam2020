using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    NONE,
    DOWN,
    UP,
    LEFT,
    RIGHT
}
public class DoorSpawnPointScript : MonoBehaviour
{
    // Start is called before the first frame update

    public EDirection direction = EDirection.NONE;

    void Start()
    {

    }

}
