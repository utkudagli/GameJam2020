﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberstaffSphereScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    public float speed;
    public int damage;
    void Start()
    {
        this.damage = this.damage > 0 ? this.damage : 1;
        Object.Destroy(this.gameObject, 5);
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    public void FixedUpdate()
    {
        this.transform.Translate(this.direction * this.speed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "PlayerCharacter")
        {
            CharacterStats stats = obj.GetComponent<CharacterStats>();
            stats.ReceiveDamage(this.damage);
            Object.Destroy(this.gameObject);
        }
        else if(obj.tag == "Wall")
        {
            Object.Destroy(this.gameObject);
        }
    }
}
