using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public HealthBar healthBar;
    public float health;
    // Start is called before the first frame update
    private void Start()
    {
        float health = 1f;
    }

}
