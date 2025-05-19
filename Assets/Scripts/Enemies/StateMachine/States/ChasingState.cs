using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : AStateBehaviour
{
    public WPManager wpManager;
    private EnemyFoV fov;
    public Transform player;
    private NavMeshAgent agent;
    private StandUp standUp;
    private EnemyCollision collision;

    [SerializeField] private float chaseSpeed = 10f; //speed of the animal while chasing

    public override bool InitializeState() => true;

    public override void OnStateStart()
    {
        Debug.Log("CHASING");
        
        AudioManager.Instance.Play(AudioManager.SoundType.Chasing);

        fov = GetComponent<EnemyFoV>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        standUp = player.GetComponent<StandUp>();
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
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public override void OnStateEnd()
    {
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
        int findPlayerTarget = fov.FindPlayerTarget();
        if (findPlayerTarget != (int)EnemyState.Invalid)
        {
            return findPlayerTarget;
        }

        if (standUp.standingUp)
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
        yield return new WaitUntil(() => Vector3.Distance(transform.position, destination.transform.position) < 1f);
        agent.ResetPath();
    }
}
