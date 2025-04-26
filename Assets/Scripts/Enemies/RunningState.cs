using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : AStateBehaviour
{
    public WPManager wpManager;
    private EnemyFoV fov;
    public Transform Player;
    private NavMeshAgent agent;
    public float runAwayDistance = 5f;

    private bool destinationReached = false;

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("Running");
        fov = GetComponent<EnemyFoV>();
        agent = GetComponent<NavMeshAgent>();
        destinationReached = false;

        // Define destination away from player
        Vector3 directionAwayFromPlayer = (agent.transform.position - Player.position).normalized;
        Vector3 destination = agent.transform.position + directionAwayFromPlayer * runAwayDistance;
        agent.SetDestination(destination);
    }

    public override void OnStateUpdate()
    {
        // Check if the agent has finished running away
        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            destinationReached = true;
        }

        // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateEnd()
    {
        GameObject destination = FindClosestWaypoint();
        Debug.Log("Closest waypoint = " + destination.transform.position);
        agent.SetDestination(destination.transform.position);
        StartCoroutine(CheckDestination(destination));
        return;
    }

    public override int StateTransitionCondition()
    {
        int playerTarget = fov.FindPlayerTarget();
        if (playerTarget != (int)EnemyState.Invalid)
        {
            return playerTarget;
        }

        // Return to wandering after reaching the runaway destination
        if (destinationReached)
        {
            return (int)EnemyState.Wandering;
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
        yield return new WaitUntil(() => Vector3.Distance(transform.position, destination.transform.position) < 1f);
        agent.ResetPath();
    }
}
