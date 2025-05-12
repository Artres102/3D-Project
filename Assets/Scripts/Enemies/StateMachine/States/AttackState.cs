using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : AStateBehaviour
{
    private PlayerMovement playerMovement;
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("ATACKED");
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(WaitAndLoadScene(1f));
    }

    public override void OnStateUpdate()
    {
        
    }
    public override void OnStateFixedUpdate()
    {
      
    }
    public override void OnStateEnd()
    {
        
    }

    public override int StateTransitionCondition()
    { 
        return (int)EnemyState.Invalid;
    }
    
    private IEnumerator WaitAndLoadScene(float delay)
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }
}
