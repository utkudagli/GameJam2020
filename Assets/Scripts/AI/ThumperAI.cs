using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ThumperAI : MonoBehaviour
{
    public float timer = 0f;
    public Vector2 moveDurationMinMax = new Vector2(1f, 2f);
    public Vector2 idleDurationMinMax = new Vector2(1f, 2f);
    public float preAttackDuration = 1f;
    public float postAttackDuration = 1f;
    public EAiState currentAiState = EAiState.IDLE;
    public Vector2 idleAttackRateMinMax = new Vector2(10, 90);
    public Vector2 moveAttackRateMinMax = new Vector2(10, 90);

    public AudioSource thumpSound;
    public AudioSource moveSound;
    public AudioSource deathSound;

    private Collider2D myCollider;
    public Animator animator;

    GameObject player;

    CharacterMovement2D movementComponent;

    public EDirection currentDirection = EDirection.UP;

    private event Action currentAiFunction = () => { };

    // Start is called before the first frame update
    void Start()
    {
        movementComponent = GetComponent<CharacterMovement2D>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        CharacterStats stats = GetComponent<CharacterStats>();
        stats.OnDeath += OnDeath;
    }

    void OnDeath(CharacterStats mystats)
    {
        this.currentAiFunction = () => { };
        this.currentAiState = EAiState.DEAD;
        mystats.OnDeath -= OnDeath;
        //this.GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentAiState == EAiState.DEAD)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            bool foundNewTask = ChangeAiState();
            if (!foundNewTask)
            {
                ResetAiOnNextTick();
            }
        }
        this.currentAiFunction();
    }

    bool ChangeAiState()
    {

        if (this.currentAiState == EAiState.IDLE)
        {
            float rng = Random.Range(0, 100);
            if (rng < Random.Range(idleAttackRateMinMax.x, idleAttackRateMinMax.y))
            {
                return Attack();
            }
            rng = Random.Range(0, 100);
            if (rng < 90)
            {
                return BeginMove();
            }
            return BeginIdle();
        }
        if (this.currentAiState == EAiState.MOVE)
        {
            float rng = Random.Range(0, 100);
            if (rng < Random.Range(moveAttackRateMinMax.x, moveAttackRateMinMax.y))
            {
                return Attack();
            }
            rng = Random.Range(0, 100);
            if (rng < 90)
            {
                return BeginIdle();
            }
            return BeginMove();
        }
        if (this.currentAiState == EAiState.PREATTACK)
        {
            return PostAttack();
        }
        if (this.currentAiState == EAiState.POSTATTACK)
        {
            float rng = Random.Range(0, 100);
            if (rng < 90)
            {
                return BeginMove();
            }
            return BeginIdle();
        }
        return false;
    }

    bool Attack()
    {
        this.currentAiState = EAiState.PREATTACK;
        this.currentAiFunction = () => { };
        this.timer = this.preAttackDuration;
        animator.SetInteger("AiState", (int)this.currentAiState);
        return true;
    }

    bool PostAttack()
    {
        this.Thump();
        this.currentAiState = EAiState.POSTATTACK;
        this.currentAiFunction = () => { };
        this.timer = this.postAttackDuration;
        return true;
    }

    bool BeginIdle()
    {
        this.currentAiState = EAiState.IDLE;
        this.currentAiFunction = () => { };
        this.timer = Random.Range(this.idleDurationMinMax.x, this.idleDurationMinMax.y);
        animator.SetInteger("AiState", (int)this.currentAiState);
        return true;
    }


    bool BeginMove()
    {
        this.currentAiState = EAiState.MOVE;
        this.currentAiFunction = Move;
        animator.SetInteger("AiState", (int)this.currentAiState);
        this.timer = Random.Range(this.moveDurationMinMax.x, this.moveDurationMinMax.y);
        Vector2 playerPos = GameData.Get().playerCharacter.transform.position;
        Vector2 myPos = this.transform.position;

        Vector2 difference = myPos - playerPos;
        if (Math.Abs(difference.x) > Math.Abs(difference.y))
        {
            //i'm more far away on the x axis, move horizontally

            this.currentDirection = difference.x < 0 ? EDirection.RIGHT : EDirection.LEFT;
        }
        else
        {
            //i'm more far away on the y axis, move vertically
            this.currentDirection = difference.y < 0 ? EDirection.UP : EDirection.DOWN;

        }

        return true;
    }

    void Move()
    {
        if (this.movementComponent)
        {
            Vector2 direction = Direction.GetDirectionVector(this.currentDirection);
            this.movementComponent.ApplyMovementInput(direction);
        }
    }

    void ResetAiOnNextTick()
    {
        this.currentAiState = EAiState.IDLE;
        this.currentAiFunction = () => { };
        this.timer = 0;
        animator.SetInteger("AiState", (int)this.currentAiState);
    }

    void Thump()
    {
        GameObject player = GameData.Get().playerCharacter;
        if (!player)
        {
            return;
        }
    }

    public void OnFootstepAnimNotify()
    {

    }

    public void OnDeathAnimNotify()
    {
        deathSound.Play();

    }

    void OnAttackAnimNotify()
    {
        thumpSound.Play();
        Debug.Log("Thumper attack anim notify");
        Vector2 mylocation = this.transform.position;
        float radius = this.myCollider.bounds.extents.x * 2;
        RaycastHit2D hit = Physics2D.CircleCast(mylocation, radius, Vector2.down, 0.01f, 1 << 9);
        if (hit.collider)
        {
            Debug.Log("Hit something");
            CharacterStats playerStats = hit.collider.gameObject.GetComponent<CharacterStats>();
            if (playerStats && playerStats.IsAlive())
            {
                Vector2 lookat = (hit.transform.position - new Vector3(mylocation.x, mylocation.y, 0)).normalized;
                //rb.AddForce(-lookat * rb.mass * 500);
                hit.collider.attachedRigidbody.AddForce(lookat * this.myCollider.attachedRigidbody.mass * 250);
                playerStats.ReceiveDamage(2);
            }
        }
    }
}
