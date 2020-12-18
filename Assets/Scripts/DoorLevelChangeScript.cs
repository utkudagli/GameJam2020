using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorLevelChangeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ontriggerenter");
        GameObject obj = collision.gameObject;
        PlayerController pc = obj.GetComponent<PlayerController>();
        if(pc)
        {
            obj.transform.position = new Vector2(0, 0);
            SceneManager.LoadScene("RandomRoomScene");
        }
    }
}
