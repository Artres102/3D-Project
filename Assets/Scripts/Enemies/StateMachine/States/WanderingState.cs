using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class WanderingState : AStateBehaviour
{
    
    public WPManager wpManager;
    public GameObject currentWaypoint;
    public float moveSpeed = 2f;
    private List<Node> path;
    private int pathIndex = 0;
    private Rigidbody rb;
    private float switchCooldown = 1f; // wait between reaching nodes and getting a new path
    private EnemyCollision collision;

    private EnemyFoV fov;

    public override bool InitializeState() => true;
    
    private float maxWanderTime = 18f;  
    private float wanderTimer = 0f;   // keep track of the time

    public override void OnStateStart()
    {
        //Debug.Log("WANDERING");
        fov = GetComponent<EnemyFoV>();
        
        PickRandomDestination();
        rb = gameObject.GetComponent<Rigidbody>();
        collision = GetComponent<EnemyCollision>();
    }

    public override void OnStateUpdate()
    {
        return;
    }

    public override void OnStateFixedUpdate()
    {
        if (path == null || pathIndex >= path.Count) return;
        
        wanderTimer += Time.fixedDeltaTime;
        if (wanderTimer >= maxWanderTime)
        {
            //Debug.Log("Wander timeout reached, picking a new destination");
            PickRandomDestination();
            return;
        }

        GameObject target = path[pathIndex].GetID();
        //Debug.Log("Distance = " + Vector3.Distance(transform.position, target.transform.position));
        if (Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            pathIndex++;
            if (pathIndex >= path.Count)
            {
                Invoke(nameof(PickRandomDestination), switchCooldown); 
            }
        }
        Vector3 dir = (target.transform.position - transform.position).normalized;
        
        Vector3 velocity = (dir * moveSpeed * Time.deltaTime);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        //transform.position += dir * moveSpeed * Time.deltaTime;
        
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        
    }

    public override void OnStateEnd()
    {
        return;
    }
    
    public override int StateTransitionCondition()
    {
        int findPlayerTarget = fov.FindPlayerTarget();
        if (findPlayerTarget != (int)EnemyState.Invalid)
        {
            return findPlayerTarget;
        }
        if (collision.attacking)
        {
            return (int)EnemyState.Attacking;
        }
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
            //Debug.Log("dest = " + destination.transform.position);
        } while (destination == currentWaypoint); // avoid picking the same one

        if (wpManager.graph.AStar(currentWaypoint, destination))
        {
            //Debug.Log("A STAR");
            path = wpManager.graph.pathList;
            pathIndex = 0;
            wanderTimer = 0f; 
        }
        currentWaypoint = destination;
    }
}
