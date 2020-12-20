using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public Animator animator;
    public GameData GameData;
    public DoorScript DoorScript;
    public float time = 1f;

    public void Awake()
    {
        DontDestroyOnLoad(animator.gameObject);
        GameData = FindObjectOfType<GameData>();
        DoorScript = FindObjectOfType<DoorScript>();
    }
    public void Fade()
    {
        StartCoroutine(WaitForIt());
    }

    IEnumerator WaitForIt()
    {
        animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1.5f);
    }
    
    // Update is called once per frame
}
