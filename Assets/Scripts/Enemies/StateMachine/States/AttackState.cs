using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : AStateBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager gameManager;
    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        gameManager = GameManager.Instance;
        
        Debug.Log("ATACKED");
        playerMovement = gameManager.player.GetComponent<PlayerMovement>();
        StartCoroutine(WaitAndLoadScene(1f));
    }

    public override void OnStateUpdate() { }
    public override void OnStateFixedUpdate() { }
    public override void OnStateEnd() { }

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
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(2);
    }
}
