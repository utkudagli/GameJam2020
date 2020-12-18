using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject controlledObject;
    CharacterMovement2D movement;
    void Start()
    {
        if (!this.controlledObject)
        {
            return;
        }
        this.movement = this.controlledObject.GetComponent<CharacterMovement2D>();
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
    }
}