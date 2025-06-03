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
        player = GameManager.Instance.player;
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
