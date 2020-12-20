using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public Vector2 inputVec;
    public Vector2 lastActiveInputVec;
    public float MovementSpeed = 50;

    public bool bIsLookingRight;
    public bool bIsLookingRightByDefault = false;

    public SpriteRenderer mySpriteRenderer;
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
        this.lastActiveInputVec = vec.normalized;
        if (this.bIsLookingRight)
        {
            if (this.inputVec.x < 0)
            {
                this.bIsLookingRight = false;
            }
        }
        else
        {
            if (this.inputVec.x > 0)
            {
                this.bIsLookingRight = true;
                
            }
        }
        if(this.mySpriteRenderer != null)
        {
            bool bShouldFlip = this.bIsLookingRightByDefault ^ this.bIsLookingRight;
            this.mySpriteRenderer.flipX = bShouldFlip;
        }
    }
}
