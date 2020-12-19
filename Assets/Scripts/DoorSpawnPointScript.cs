using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDirection
{
    DOWN,
    UP,
    LEFT,
    RIGHT
}

public class Direction
{
    public static Vector2 GetDirectionVector(EDirection direction)
    {
        switch (direction)
        {
            case EDirection.UP:
                return Vector2.up;
            case EDirection.DOWN:
                return Vector2.down;
            case EDirection.LEFT:
                return Vector2.left;
            case EDirection.RIGHT:
                return Vector2.right;
            default:
                return Vector2.up;
        }
    }
}
public class DoorSpawnPointScript : MonoBehaviour
{
    // Start is called before the first frame update

    public EDirection direction = EDirection.UP;

    void Start()
    {

    }

}
