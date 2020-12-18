using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public Vector2 inputVec;
    public float MovementSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        this.transform.Translate(this.inputVec * this.MovementSpeed * Time.fixedDeltaTime);
    }

    public virtual void ApplyMovementInput(Vector2 vec)
    {
        this.inputVec = vec;
    }
}
