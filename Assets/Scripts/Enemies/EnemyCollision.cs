using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public bool attacking = false;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag + "Eu sei que sim");
        if (collision.gameObject.CompareTag("Player"))
        {
            attacking = true;
            Debug.Log(attacking);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
