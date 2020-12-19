﻿using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
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
        if (FindFreeDirection())
        {
            this.currentAiState = EAiState.MOVE;
            this.currentAiFunction = Move;
            animator.SetInteger("AiState", (int)this.currentAiState);
            this.timer = Random.Range(this.moveDurationMinMax.x, this.moveDurationMinMax.y);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Move()
    {
        if (this.movementComponent)
        {
            Vector2 position = this.gameObject.transform.position;
            Vector2 direction = Direction.GetDirectionVector(this.currentDirection);

            RaycastHit2D hit = Physics2D.Raycast(position, direction, 1f, 1 << 8);
            if (!hit.collider)
            {
                Debug.DrawLine(position, position + direction * 1, Color.yellow);
                this.movementComponent.ApplyMovementInput(direction);
            }
            else
            {
                Debug.DrawLine(position, position + direction * 1, Color.red);
                ResetAiOnNextTick();
            }
        }
    }

    bool FindFreeDirection()
    {
        //try to select a random direction to move
        EDirection rngDirection = this.currentDirection = (EDirection)Random.Range(0, 3);
        Vector2 direction = Vector2.up;
        switch (rngDirection)
        {
            case EDirection.UP:
                direction = Vector2.up; break;
            case EDirection.DOWN:
                direction = Vector2.down; break;
            case EDirection.LEFT:
                direction = Vector2.left; break;
            case EDirection.RIGHT:
                direction = Vector2.right; break;
        }
        //check if we can actually go that way
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, 1f, 1 << 8);
        if (hit.collider)
        {
            //no wall in the way, go that way
            this.currentDirection = rngDirection;
            return true;
        }

        // a wall is in the way, just check directions clockwise
        EDirection blockedDirection = rngDirection;
        rngDirection += 1 % 4;
        while (rngDirection != blockedDirection)
        {
            direction = Direction.GetDirectionVector(rngDirection);
            hit = Physics2D.Raycast(this.gameObject.transform.position, direction, 1f, 1 << 8);
            if (!hit.collider)
            {
                //no wall in the way, go that way
                this.currentDirection = rngDirection;
                return true;
            }
            rngDirection += 1 % 4;
        }
        //couldnt find any direction, reset ai
        return false;
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
}