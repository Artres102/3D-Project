using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : AStateBehaviour
{
    private EnemyFoV fov;

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("CHASING");
        fov = GetComponent<EnemyFoV>();
    }

    public override void OnStateUpdate()
    {
            
        // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
    }

    public override void OnStateEnd()
    {
        return;
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
