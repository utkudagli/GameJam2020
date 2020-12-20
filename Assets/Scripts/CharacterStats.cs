using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStats : MonoBehaviour
{
    public int health;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    private float colorAdjuster = 0f;
    public event Action<CharacterStats> OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        this.health = this.health > 0 ? this.health : 1;
        this.enabled = false;
    }

    // Update is called once per frame

    void Update()
    {

        this.colorAdjuster -= Time.deltaTime;

        this.spriteRenderer.color = Color.Lerp(Color.white, Color.red, this.colorAdjuster);
        if(this.colorAdjuster < 0)
        {
            this.spriteRenderer.color = Color.white;
            this.enabled = false;
        }
    }
    public void ReceiveDamage(int amount)
    {
        if(!IsAlive())
        {
            return;
        }
        this.health -= amount;

        this.colorAdjuster = 0.5f;
        this.spriteRenderer.color = Color.red;
        this.enabled = true;
        if (this.health <= 0)
        {
            this.animator.SetBool("IsAlive", false);
            this.OnDeath?.Invoke(this);
        }
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }
}