using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controlledObject;
    CharacterMovement2D movement;
    PlayerCharacterScript playerCharacterScript;
    void Start()
    {
        if (!this.controlledObject)
        {
            return;
        }
        this.movement = this.controlledObject.GetComponent<CharacterMovement2D>();
        this.playerCharacterScript = this.controlledObject.GetComponent<PlayerCharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement)
        {
            return;
        }
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.movement.ApplyMovementInput(input);
        if(Input.GetButtonDown("Jump"))
        {
            if(this.playerCharacterScript)
            {
                this.playerCharacterScript.TriggerInteract();
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            if(this.playerCharacterScript)
            {
                this.playerCharacterScript.Attack();
            }
        }
    }
}