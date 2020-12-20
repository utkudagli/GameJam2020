using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStats : MonoBehaviour
{
    public int health;

    public Animator animator;

    public event Action<CharacterStats> OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        this.health = this.health > 0 ? this.health : 1;
    }

    // Update is called once per frame

    public void ReceiveDamage(int amount)
    {
        if(!IsAlive())
        {
            return;
        }
        this.health -= amount;
        if (this.health <= 0)
        {
            Debug.Log($"Character {this.gameObject.name} is dead");
            this.animator.SetBool("IsAlive", false);
            this.OnDeath?.Invoke(this);
        }
    }

    bool IsAlive()
    {
        return this.health > 0;
    }
}