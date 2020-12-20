using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour
{
    public int HealthRecovered = 3;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if (pcs)
        {
            pcs.OnTryInteract += OnPlayerInteract;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacterScript pcs = obj.GetComponent<PlayerCharacterScript>();
        if (pcs)
        {
            pcs.OnTryInteract -= OnPlayerInteract;
        }
    }

    public void OnPlayerInteract(GameObject obj)
    {
        CharacterStats stats = obj.GetComponent<CharacterStats>();
        stats.Heal(this.HealthRecovered);
        PlayerCharacterScript pss = obj.GetComponent<PlayerCharacterScript>();
        pss.OnTryInteract -= OnPlayerInteract;
        Object.Destroy(this.gameObject);
    }
}
