using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            PlayerCharacterScript pss = obj.GetComponent<PlayerCharacterScript>();
            pss.OnTryInteract += OnPlayerInteract;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            PlayerCharacterScript pss = obj.GetComponent<PlayerCharacterScript>();
            pss.OnTryInteract -= OnPlayerInteract;
        }
    }

    public void OnPlayerInteract(GameObject obj)
    {
        Debug.Log("Potion interact");
        PlayerCharacterScript pss = obj.GetComponent<PlayerCharacterScript>();
        pss.OnTryInteract -= OnPlayerInteract;

        Object.Destroy(this.gameObject);
    }
}
