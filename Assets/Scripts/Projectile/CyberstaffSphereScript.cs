using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberstaffSphereScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    public float speed;
    void Start()
    {
        
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        this.transform.Translate(this.direction * this.speed * Time.fixedDeltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Collision");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Overlap");
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            //TODO : do damage to player
            Object.Destroy(this.gameObject);
        }
        else if(obj.tag == "Wall")
        {
            Object.Destroy(this.gameObject);
        }
    }
}
