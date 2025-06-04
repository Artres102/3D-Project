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
    
    private GameManager gameManager;

    [SerializeField] private float chaseSpeed = 10f; //speed of the animal while chasing

    public override bool InitializeState() => true;

    public override void OnStateStart()
    {
        gameManager = GameManager.Instance;
        Debug.Log("CHASING");
        
        gameObject.GetComponent<AudioSource> ().Play ();

        if (!player) player = gameManager.player.transform;
        if (!fov) fov = GetComponent<EnemyFoV>();
        if (!agent) agent = GetComponent<NavMeshAgent>(); ;
        if (!standUp) standUp = player.GetComponent<StandUp>();
        if (!collision) collision = GetComponent<EnemyCollision>();

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
        gameObject.GetComponent<AudioSource> ().Stop ();
        GameObject destination = FindClosestWaypoint();
        Debug.Log("closest = " + destination.transform.position);
        agent.SetDestination(destination.transform.position);
        StartCoroutine(CheckDestination(destination));
    }

    public override int StateTransitionCondition()
    {
        if (collision.attacking) return (int)EnemyState.Attacking;
        
        int findPlayerTarget = fov.FindPlayerTarget();
        
        if (findPlayerTarget != (int)EnemyState.Invalid) return findPlayerTarget;
        if (standUp.standingUp) return (int)EnemyState.Running;

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
