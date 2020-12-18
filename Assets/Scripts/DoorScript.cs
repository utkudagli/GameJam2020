using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public EDirection doorDirection = EDirection.NONE;
    bool bCanBeUsed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if(pcs)
        {
            bCanBeUsed = true;
            GameData.Get().LoadNextRoom(this.doorDirection);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if (pcs)
        {
            bCanBeUsed = false;
        }
    }

    public void Interact(PlayerCharacterScript pcs)
    {
        GameData.Get().LoadNextRoom(this.doorDirection);
    }
}
