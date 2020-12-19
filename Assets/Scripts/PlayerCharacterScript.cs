using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerCharacterScript : MonoBehaviour
{
    public event Action<GameObject> OnTryInteract;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddInteractContext(MonoBehaviour other)
    {
        
    }

    public void TriggerInteract()
    {
        this.OnTryInteract?.Invoke(this.gameObject);
    }
}
