using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionalItemSpawner : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject leafPrefab;
    public GameObject stickPrefab;
    public GameObject logPrefab;
    public GameObject rockPrefab;

    [Header("Item Amounts")]
    [SerializeField] private int leafAmount;
    [SerializeField] private int stickAmount;
    [SerializeField] private int logAmount;
    [SerializeField] private int rockAmount;

    private GameObject player;

    private Collider collider;

    private float xPosition;
    private float yPosition;
    private float zPosition;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        
        SpawnLeaves();
        SpawnSticks();
        SpawnLogs();
        SpawnRocks();
    }

    private void SpawnLeaves()
    {
        for (int i = 0; i < leafAmount; i++)
        {
            xPosition = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            zPosition = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
            yPosition = transform.position.y;
            
            InstantiateLeaf(new Vector3(xPosition, yPosition, zPosition));
        }
    }

    private void SpawnSticks()
    {
        for (int i = 0; i < stickAmount; i++)
        {
            xPosition = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            zPosition = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
            
            InstantiateStick(new Vector3(xPosition, yPosition, zPosition));
        }
    }
    
    private void SpawnLogs()
    {
        for (int i = 0; i < logAmount; i++)
        {
            xPosition = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            zPosition = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
            
            InstantiateLog(new Vector3(xPosition, yPosition, zPosition));
        }
    }  
    
    private void SpawnRocks()
    {
        for (int i = 0; i < rockAmount; i++)
        {
            xPosition = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            zPosition = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
            
            InstantiateRock(new Vector3(xPosition, yPosition, zPosition));
        }
    }
    
    private void InstantiateLeaf(Vector3 spawnPosition)
    {
        Instantiate(leafPrefab, spawnPosition, leafPrefab.transform.rotation);
    }
    private void InstantiateStick(Vector3 spawnPosition)
    {
        Instantiate(stickPrefab, spawnPosition, stickPrefab.transform.rotation);
    }
    private void InstantiateLog(Vector3 spawnPosition)
    {
        Instantiate(logPrefab, spawnPosition, logPrefab.transform.rotation);
    }
    private void InstantiateRock(Vector3 spawnPosition)
    {
        Instantiate(rockPrefab, spawnPosition, rockPrefab.transform.rotation);
    }
}
