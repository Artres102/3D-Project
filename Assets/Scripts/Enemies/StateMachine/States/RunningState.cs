using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : AStateBehaviour
{
    public WPManager wpManager;
    private EnemyFoV fov;
    public Transform player;
    private NavMeshAgent agent;
    public float runAwayDistance = 5f;

    private bool destinationReached = false;
    
    private GameManager gameManager;

    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        gameManager = GameManager.Instance;
        
        Debug.Log("Running");
        if (!fov) fov = GetComponent<EnemyFoV>();
        if (!player) player = gameManager.player.transform;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        destinationReached = false;

        // Define destination away from player
        Vector3 directionAwayFromPlayer = (agent.transform.position - player.position).normalized;
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
        int findPlayerTarget = fov.FindPlayerTarget();
        if (findPlayerTarget != (int)EnemyState.Invalid)
        {
            return findPlayerTarget;
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
