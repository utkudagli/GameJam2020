using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHudScript : MonoBehaviour
{
    // Start is called before the first frame update

    public HealthBar healthBarScript;
    void Start()
    {
        
    }
    public void Initialize()
    {
        GameData data = GameData.Get();
        GameObject playerObj = data.playerCharacter;

        CharacterStats playerStats = playerObj.GetComponent<CharacterStats>();
        playerStats.OnHealthChanged += OnHealthChanged;
    }
    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        this.healthBarScript.SetSize((float)currentHealth / (float)maxHealth);
    }
}
