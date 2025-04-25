using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : AStateBehaviour
{
    public WPManager wpManager;
    private EnemyFoV fov;
    public Transform Player;
    private NavMeshAgent agent;
    public float runAwayDistance = 5f;
    

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Running");
        fov = GetComponent<EnemyFoV>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateUpdate()
    {
            
        // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
    }

    public override void OnStateFixedUpdate()
    {
        Vector3 directionAwayFromPlayer = (agent.transform.position - Player.position).normalized;
        Vector3 destination = agent.transform.position + directionAwayFromPlayer * runAwayDistance;
        agent.SetDestination(destination);
    }


    public override void OnStateEnd()
    {
        GameObject destination = FindClosestWaypoint();
        Debug.Log("closest = " + destination.transform.position);
        agent.SetDestination(destination.transform.position);
        StartCoroutine(CheckDestination(destination));
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
    GameObject FindClosestWaypoint()
    {
        float minDist = Mathf.Infinity;
        GameObject closest = wpManager.waypoints[0];

        foreach (var wp in wpManager.waypoints)
        {
            float dist = Vector3.Distance(transform.position, wp.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = wp;
            }
        }

        return closest;
    }

    private IEnumerator CheckDestination(GameObject destination)
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, destination.transform.position) < 0.2f);
        agent.ResetPath();
    }
}
