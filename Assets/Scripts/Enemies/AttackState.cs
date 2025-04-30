using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : AStateBehaviour
{

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("ATACKED");
        StartCoroutine(WaitAndLoadScene(1f));
    }

    public override void OnStateUpdate()
    {
        // Handle alarm behavior
    }
    public override void OnStateFixedUpdate()
    {
      
    }
    public override void OnStateEnd()
    {
        // Cleanup if necessary
    }

    public override int StateTransitionCondition()
    { 
        return (int)EnemyState.Invalid;// Default: no transition
    }
    
    private IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
		Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
}
