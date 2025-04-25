using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : AStateBehaviour
{
    public WPManager wpManager;
    private EnemyFoV fov;
    public Transform Player;
    private NavMeshAgent agent;
    private StandUp StandUp;
    private EnemyCollision collision;

    [SerializeField] private float chaseSpeed = 10f; //speed of the animal while chasing

    public override bool InitializeState() => true;

    public override void OnStateStart()
    {
        Debug.Log("CHASING");

        fov = GetComponent<EnemyFoV>();
        agent = GetComponent<NavMeshAgent>();
        StandUp = GameObject.FindWithTag("Player").GetComponent<StandUp>();
        collision = GetComponent<EnemyCollision>();

        if (agent != null)
        {
            agent.speed = chaseSpeed;
        }
    }

    public override void OnStateUpdate()
    {
        // Nothing here right now
    }

    public override void OnStateFixedUpdate()
    {
        if (Player != null)
        {
            agent.SetDestination(Player.position);
        }
    }

    public override void OnStateEnd()
    {
        if (StateTransitionCondition() == (int)EnemyState.Attacking)
        {
            return;
        }

        GameObject destination = FindClosestWaypoint();
        Debug.Log("closest = " + destination.transform.position);
        agent.SetDestination(destination.transform.position);
        StartCoroutine(CheckDestination(destination));
    }

    public override int StateTransitionCondition()
    {
        if (collision.attacking)
        {
            return (int)EnemyState.Attacking;
        }

        if (fov.FindPlayerTarget() != (int)EnemyState.Invalid)
        {
            return fov.FindPlayerTarget();
        }

        if (StandUp.Standingup)
        {
            return (int)EnemyState.Running;
        }

        return (int)EnemyState.Invalid;
    }

    private GameObject FindClosestWaypoint()
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
