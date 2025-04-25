using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject leafPrefab;
    public GameObject stickPrefab;
    public GameObject logPrefab;
    public GameObject rockPrefab;

    private GameObject player;

    private float xPosition;
    private float yPosition;
    private float zPosition;
    private int amount = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        for (int i = 0; i < amount; i++)
        {
            SpawnLeaf(new Vector3(Random.Range(-250f, 250f), 50f, Random.Range(-250f, 250f)));
            SpawnStick(new Vector3(Random.Range(-250f, 250f), 50f, Random.Range(-250f, 250f)));
            SpawnLog(new Vector3(Random.Range(-250f, 250f), 50f, Random.Range(-250f, 250f)));
            SpawnRock(new Vector3(Random.Range(-250f, 250f), 50f, Random.Range(-250f, 250f)));
        }
    }
    
    private void SpawnLeaf(Vector3 spawnPosition)
    {
        Instantiate(leafPrefab, spawnPosition, Quaternion.identity);
    }
    private void SpawnStick(Vector3 spawnPosition)
    {
        Instantiate(stickPrefab, spawnPosition, Quaternion.identity);
    }
    private void SpawnLog(Vector3 spawnPosition)
    {
        Instantiate(logPrefab, spawnPosition, Quaternion.identity);
    }
    private void SpawnRock(Vector3 spawnPosition)
    {
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }

    void Cheat()
    {
        Vector3 offset = player.transform.up * 2f;
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Instantiate(leafPrefab, player.transform.position + offset, Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(stickPrefab, player.transform.position + offset, Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(logPrefab, player.transform.position + offset, Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(rockPrefab, player.transform.position + offset, Quaternion.identity);
        }
    }

    private void Update()
    {
        Cheat();
    }
}
