using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum EPlayerState
{
    IDLE,
    MOVING,
    ATTACKING,
    DEAD
}
public class PlayerCharacterScript : MonoBehaviour
{
    public event Action<GameObject> OnTryInteract;

    public Transform attackHitboxTransform;
    private CharacterMovement2D movementComponent;
    private Rigidbody2D rb;
    private CharacterStats myStats;
    public Animator myAnimator;
    public EPlayerState myPlayerState = EPlayerState.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        movementComponent = gameObject.GetComponent<CharacterMovement2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        myStats = GetComponent<CharacterStats>();
        myStats.OnDeath += OnDeath;
    }

    public void OnDeath(CharacterStats myStats)
    {
        myStats.OnDeath -= OnDeath;
        this.myPlayerState = EPlayerState.DEAD;
        this.myAnimator.SetBool("IsAlive", false);
        this.enabled = false;
    }

    // Update is called once per frame

    public void Update()
    {
        if(this.movementComponent.lastActiveInputVec != Vector2.zero)
        {
            this.myAnimator.SetInteger("PlayerState",(int)EPlayerState.MOVING);
        }
        else
        {
            this.myAnimator.SetInteger("PlayerState", (int)EPlayerState.IDLE);
        }
    }
    public void TriggerInteract()
    {
        this.OnTryInteract?.Invoke(this.gameObject);
    }

    private Vector2 lastAttackLocation;
    bool bDidAttackLastFrame = false;

    public void Attack()
    {
        Debug.Log("trying to play attacking animation");
        this.myPlayerState = EPlayerState.ATTACKING;
        this.myAnimator.Play("AN_PlayerAttack");
    }

    void OnAttackAnimNotify()
    {
        if (!this.attackHitboxTransform)
        {
            return;
        }
        Vector2 origin = this.transform.position;
        Vector2 offset = this.movementComponent.bIsLookingRight ? Vector2.right * 0.5f : Vector2.left * 0.5f;

        lastAttackLocation = origin + offset;

        bDidAttackLastFrame = true;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(lastAttackLocation, 1, offset.normalized, 0.5f, 1 << 10);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider)
            {
                if (hit.collider.tag == "Enemy")
                {
                    CharacterStats enemyStats = hit.collider.gameObject.GetComponent<CharacterStats>();
                    if (enemyStats && enemyStats.IsAlive())
                    {
                        Vector2 lookat = (hit.transform.position - new Vector3(origin.x, origin.y, 0)).normalized;
                        rb.AddForce(-lookat * rb.mass * 500);
                        hit.collider.attachedRigidbody.AddForce(lookat * rb.mass * 250);
                        enemyStats.ReceiveDamage(1);
                    }
                }

            }
        }
    }

    void OnPostAttackAnimNotify()
    {
        this.myPlayerState = EPlayerState.IDLE;
        //this.myAnimator.SetInteger("PlayerState", (int)EPlayerState.IDLE);
    }

    private void OnDrawGizmos()
    {
        if(!bDidAttackLastFrame)
        {
            return;
        }
        DrawEllipse(lastAttackLocation + Vector2.up * 0.3f, transform.forward, transform.up, 1, 1, 16, Color.white);
        bDidAttackLastFrame = false;
    }

    private static void DrawEllipse(Vector3 pos, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, Color color, float duration = 0)
    {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(forward, up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }
}
