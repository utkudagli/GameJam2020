using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public Animator myAnimator;
    public CharacterStats myStats;

    public GameObject healthPotionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        myStats.OnDeath += OnDeath;
    }

    // Update is called once per frame

    public void OnDeath(CharacterStats myStats)
    {
        myStats.OnDeath -= OnDeath;
        this.myAnimator.SetInteger("DestroyedLook", Random.Range(1,3));
        if(this.healthPotionPrefab)
        {
            Vector2 loc = Random.insideUnitCircle;
            loc = loc + new Vector2(this.transform.position.x, this.transform.position.y);
            Instantiate(this.healthPotionPrefab, loc, Quaternion.identity);
        }
    }
}
