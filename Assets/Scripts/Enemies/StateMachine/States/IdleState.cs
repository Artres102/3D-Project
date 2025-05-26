using System;
using UnityEngine;

public class IdleState : AStateBehaviour
{
    private EnemyFoV fov;

    [SerializeField] private float idleDuration = 2f;
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
    }

    public override void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            AssociatedStateMachine.SetState((int)EnemyState.Wandering); // Transition back to Wandering
        }
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        if (fov.FindPlayerTarget() != (int)EnemyState.Invalid)
        {
            return fov.FindPlayerTarget(); 
        }
        return (int)EnemyState.Invalid;
    }
}