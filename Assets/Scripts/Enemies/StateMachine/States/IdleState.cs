using System;
using UnityEngine;

public class IdleState : AStateBehaviour
{
    private EnemyFoV fov;
    private EnemyCollision collision;

    [SerializeField] private float idleDuration = 5f;
    private float idleTimer = 0f;

    // private EnemyFoV fov;


    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("IDLE");
        
        if (!fov) fov = GetComponent<EnemyFoV>();
        if (!collision) collision = GetComponent<EnemyCollision>();
        
    }

    public override void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            AssociatedStateMachine.SetState((int)EnemyState.Wandering); // Transition back to Wandering
        }
    }

    public override void OnStateFixedUpdate() { }

    public override void OnStateEnd() { }

    public override int StateTransitionCondition()
    {
        int findPlayerTarget = fov.FindPlayerTarget();
        
        if (findPlayerTarget != (int)EnemyState.Invalid) return findPlayerTarget;
        if (collision.attacking) return (int)EnemyState.Attacking;

        return (int)EnemyState.Invalid;
    }
}