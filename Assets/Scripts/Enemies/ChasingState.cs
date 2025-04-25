using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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


    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("CHASING");
        fov = GetComponent<EnemyFoV>();
        agent = GetComponent<NavMeshAgent>();
        StandUp = GameObject.FindWithTag("Player").GetComponent<StandUp>();
        collision = GetComponent<EnemyCollision>();
    }

    public override void OnStateUpdate()
    {

        // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
    }

    public override void OnStateFixedUpdate()
    {
        agent.SetDestination(Player.position);
    }

    public override void OnStateEnd()
    {
        if (StateTransitionCondition() == (int)EnemyState.Attacking)
        {
            Debug.Log("I skipped everything lmao");
            return;
        }

        GameObject destination = FindClosestWaypoint();
        Debug.Log("closest = " + destination.transform.position);
        agent.SetDestination(destination.transform.position);
        StartCoroutine(CheckDestination(destination));
        return;
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
