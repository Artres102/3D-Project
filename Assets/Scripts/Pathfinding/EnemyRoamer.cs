using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : MonoBehaviour
{
    public WPManager wpManager;
    public GameObject currentWaypoint;
    public float moveSpeed = 2f;
    private List<Node> path;
    private int pathIndex = 0;
    private Rigidbody rb;
    private float switchCooldown = 1f; // wait between reaching nodes and getting a new path

    void Start()
    {
        PickRandomDestination();
        
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (path == null || pathIndex >= path.Count) return;

        GameObject target = path[pathIndex].GetID();
        Vector3 dir = (target.transform.position - transform.position).normalized;
        
        Vector3 velocity = (dir * moveSpeed * Time.deltaTime);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        //transform.position += dir * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            pathIndex++;
            if (pathIndex >= path.Count)
            {
                Invoke(nameof(PickRandomDestination), switchCooldown); 
            }
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
        } while (destination == currentWaypoint); // avoid picking the same one

        currentWaypoint = destination;

        // find closest node 
        GameObject nearest = FindClosestWaypoint();
        if (wpManager.graph.AStar(nearest, destination))
        {
            path = wpManager.graph.pathList;
            pathIndex = 0;
        }
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
}
