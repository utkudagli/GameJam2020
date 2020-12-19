using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public Vector2 inputVec;
    public float MovementSpeed = 50;

    public bool bIsLookingRight;

    SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
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
                this.mySpriteRenderer.flipX = false;
            }
        }
        else
        {
            if (this.inputVec.x > 0)
            {
                this.bIsLookingRight = true;
                this.mySpriteRenderer.flipX = true;
            }
        }

    }
}
