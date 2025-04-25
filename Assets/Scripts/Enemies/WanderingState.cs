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
        
        PickRandomDestination();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public override void OnStateUpdate()
    {
            SetNextWaypoint();
            
    }

    public override void OnStateFixedUpdate()
    {
        if (path == null || pathIndex >= path.Count) return;

        GameObject target = path[pathIndex].GetID();
        Debug.Log("Distance = " + Vector3.Distance(transform.position, target.transform.position));
        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
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
    
    
    void PickRandomDestination()
    {
        if (wpManager == null || wpManager.graph == null || wpManager.waypoints.Length == 0)
            return;

        GameObject destination;
        do
        {
            destination = wpManager.waypoints[Random.Range(0, wpManager.waypoints.Length)];
            Debug.Log("dest = " + destination.transform.position);
        } while (destination == currentWaypoint); // avoid picking the same one

        //currentWaypoint = destination;

        // find closest node 
        GameObject nearest = FindClosestWaypoint();
        if (wpManager.graph.AStar(currentWaypoint, destination))
        {
            Debug.Log("A STAR");
            path = wpManager.graph.pathList;
            Debug.Log("path = " + path.Count);
            pathIndex = 0;
        }
        currentWaypoint = destination;
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
    
    
    // private float lowerSuspicion(float suspicion)
    // {
    //     if (suspicion > 0f)
    //     {
    //         return suspicion - 15 * Time.deltaTime;
    //     }
    //     return suspicion;
    // }
}
