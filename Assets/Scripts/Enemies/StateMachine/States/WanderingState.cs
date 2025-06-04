using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderingState : AStateBehaviour
{
    public WPManager wpManager;
    public GameObject currentWaypoint;
    public float moveSpeed;

    private List<Node> path;
    private int pathIndex = 0;
    private Rigidbody rb;
    private float switchCooldown = 1f;

    private EnemyCollision collision;
    private EnemyFoV fov;

    private bool isCheckingStuck = false;
    private float stuckCheckDelay = 0.3f;
    private Vector3 lastPosition;

    public override bool InitializeState() => true;

    public override void OnStateStart()
    {
        Debug.Log("WANDERING");
        if (!fov) fov = GetComponent<EnemyFoV>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!collision) collision = GetComponent<EnemyCollision>();

        PickRandomDestination();
    }

    public override void OnStateUpdate() { }

    public override void OnStateFixedUpdate()
    {
        if (path == null || pathIndex >= path.Count) return;

        GameObject target = path[pathIndex].GetID();
        if (Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            pathIndex++;
            if (pathIndex >= path.Count)
            {
                Invoke(nameof(PickRandomDestination), switchCooldown);
            }
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        Vector3 velocity = dir * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        if (gameObject.name == "otter 4") Debug.Log(velocity);
        
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    public override void OnStateEnd()
    {
        rb.velocity = Vector3.zero;
    }

    public override int StateTransitionCondition()
    {
        int findPlayerTarget = fov.FindPlayerTarget();
        if (findPlayerTarget != (int)EnemyState.Invalid)
            return findPlayerTarget;

        if (collision.attacking)
            return (int)EnemyState.Attacking;

        return (int)EnemyState.Invalid;
    }

    void PickRandomDestination()
    {
        if (wpManager == null || wpManager.graph == null || wpManager.waypoints.Length == 0)
            return;

        GameObject destination;
        do
        {
            destination = wpManager.waypoints[Random.Range(0, wpManager.waypoints.Length)];
        } while (destination == currentWaypoint);

        if (wpManager.graph.AStar(currentWaypoint, destination))
        {
            path = wpManager.graph.pathList;
            pathIndex = 0;
        }

        currentWaypoint = destination;
    }

    // AVOID GETTING STUCK 
    
    private void OnCollisionEnter(Collision other)
    {
        if (!enabled || path == null || pathIndex >= path.Count || isCheckingStuck) return;

        if (other.gameObject.CompareTag("Player")) return;
        
        isCheckingStuck = true;
        lastPosition = transform.position;
        Invoke(nameof(CheckIfStuckAndRepath), stuckCheckDelay);
    }

    private void CheckIfStuckAndRepath()
    {
        float moved = Vector3.Distance(transform.position, lastPosition);

        if (moved < 1f) // barely moving
        {
            PickRandomDestination();
        }

        isCheckingStuck = false;
    }
}
