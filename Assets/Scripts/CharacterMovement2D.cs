using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public Vector2 inputVec;
    public float MovementSpeed = 50;

    public bool bIsLookingRight;

    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        this.renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        this.transform.Translate(this.inputVec * this.MovementSpeed * Time.fixedDeltaTime);
        this.inputVec = Vector2.zero;
    }

    public virtual void ApplyMovementInput(Vector2 vec)
    {
        this.inputVec = vec;

        if (this.bIsLookingRight)
        {
            if (this.inputVec.x < 0)
            {
                this.bIsLookingRight = false;
                this.renderer.flipX = false;
            }
        }
        else
        {
            if (this.inputVec.x > 0)
            {
                this.bIsLookingRight = true;
                this.renderer.flipX = true;
            }
        }

    }
}
