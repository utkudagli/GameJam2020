﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update

    public EDirection doorDirection = EDirection.UP;
    public List<Sprite> DoorSprites;
    public SpriteRenderer myRenderer;
    public void Start()
    {
        int index = Random.Range(0, DoorSprites.Count);
        this.myRenderer.sprite = DoorSprites[index];
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if(pcs)
        {
            pcs.OnTryInteract += Interact;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if (pcs)
        {
            pcs.OnTryInteract -= Interact;
        }
    }

    public void Interact(GameObject playerCharacter)
    {
        GameData.Get().LoadNextRoom(this.doorDirection);
    }
}
