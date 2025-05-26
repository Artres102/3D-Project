using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoV : MonoBehaviour
{
    [SerializeField] private float fov = 30f; // Field of view angle
    [SerializeField] private float viewDistance = 10f; // Maximum distance to detect the player
    [SerializeField] private float aggressionFactor = 1f; // Factor to increase aggression
    [SerializeField] private float aggressionRange = 25f; // How much aggression enemy needs to have before chasing
    [SerializeField] private float aggressionLimit = 50f; // How much aggression level can increase
    [SerializeField] private float aggressionFall = 10f; // Rate at which aggression decreases

    private GameObject player; 
    public float aggressionLevel = 0f;
    private StateMachine stateMachine;
    private ChasingState chasingState;
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        
        if (!stateMachine) stateMachine = GetComponent<StateMachine>();
        if (!chasingState) chasingState = gameObject.GetComponent<ChasingState>();
        if (!player) player = gameManager.player;
    }

    private void Update()
    {
        // Decrease aggression level over time
        if (aggressionLevel > 0)
        {
            aggressionLevel -= aggressionFall * Time.deltaTime;
        }
        else if (aggressionLevel < 0)
        {
            aggressionLevel = 0;
        }

        FindPlayerTarget();
    }

    public int FindPlayerTarget()
    {
        if (aggressionLevel < aggressionRange && stateMachine.GetCurrentState() == chasingState)
        {
            return (int)EnemyState.Wandering;
        }
        // Check if the player is within view distance
        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance)
        {
            return (int)EnemyState.Invalid;;
        }

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);
        float fovInRadians = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);

        if (dotProduct < fovInRadians)
        {
            return (int)EnemyState.Invalid;;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // If the player is directly seen, raise aggression level
                aggressionLevel = RaiseAggression(aggressionLevel);
                Debug.Log($"This is the player and aggression level is {aggressionLevel}");

                // Check aggression levels to change state
                if (aggressionLevel > aggressionLimit)
                {
                    aggressionLevel = aggressionLimit;
                } else if (aggressionLevel >= aggressionRange)
                {
                    return (int)EnemyState.Chasing; // Start chasing
                }
            }
        }

        return (int)EnemyState.Invalid;;
    }

    private float RaiseAggression(float aggression)
    {
        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance / 2)
        {
            return aggression + 15 * aggressionFactor * Time.deltaTime;
        }
        else
        {
            return aggression + 25 * aggressionFactor * Time.deltaTime;
        } 
    }

    private void DrawLines()
    {
        
    }
}