using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStats : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Animator animator;

    public SpriteRenderer spriteRenderer;
    private float colorAdjuster = 0f;
    public event Action<CharacterStats> OnDeath;
    public event Action<int, int> OnHealthChanged;

    private Color currentAdjustingColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = this.health > 0 ? this.health : 1;
        this.health = this.maxHealth;
        this.enabled = false;
    }

    // Update is called once per frame

    void Update()
    {

        this.colorAdjuster -= Time.deltaTime;

        this.spriteRenderer.color = Color.Lerp(Color.white, this.currentAdjustingColor, this.colorAdjuster);
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
        this.currentAdjustingColor = Color.red;

        this.OnHealthChanged?.Invoke(this.health, this.maxHealth);

        if (this.health <= 0)
        {
            this.animator.SetBool("IsAlive", false);
            this.OnDeath?.Invoke(this);
        }
    }

    public void Heal(int amount)
    {
        if (!IsAlive())
        {
            return;
        }
        this.health += amount;
        if(this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        this.colorAdjuster = 0.5f;
        this.spriteRenderer.color = Color.green;
        this.currentAdjustingColor = Color.green;
        this.enabled = true;
        this.OnHealthChanged?.Invoke(this.health, this.maxHealth);
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }
}