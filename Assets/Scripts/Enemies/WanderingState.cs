using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderingState : AStateBehaviour
{
    [SerializeField] private Transform[] waypoints;

    private EnemyFoV fov;

    public override bool InitializeState()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return true;
        }

        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("WANDERING");
        fov = GetComponent<EnemyFoV>();
        SetNextWaypoint();
    }

    public override void OnStateUpdate()
    {
            SetNextWaypoint();
            
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
    
    private void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
        }
    }
    
    // private float lowerSuspicion(float suspicion)
    // {
    //     if (suspicion > 0f)
    //     {
    //         return suspicion - 15 * Time.deltaTime;
    //     }
    //     return suspicion;
    // }
}
