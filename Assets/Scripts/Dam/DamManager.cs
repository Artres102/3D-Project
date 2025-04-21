using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamManager : MonoBehaviour
{
    // private List<Item> itemsInDam = new List<Item>();

    [SerializeField] private int[] itemsCounter = new int[4];
    [SerializeField] private int[] currentUpgrade;
    private bool[] itemsDone = new bool[4];
    private int damLevel = 0;
    
    private InventoryScript inventoryScript;

    private List<Item> playerItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<InventoryScript>();
        playerItems = inventoryScript.items;

        currentUpgrade = GenerateUpgrade();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log(itemsInDam.Count + " Items in Dam");
            for (int i = 0; i < itemsCounter.Length; i++)
            {
                Debug.Log(itemsCounter[i] + " " + i);
            }
            Debug.Log(inventoryScript.items.Count + " Items player has");
            Debug.Log(inventoryScript.currentWeight + " Weight player has");
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!other.CompareTag("Player"))
        {
            return;
        }

        foreach (Item item in inventoryScript.items.ToList())
        {
            Debug.Log("Depositing: " + item.itemName + " (ID: " + item.itemId + ")");
            inventoryScript.currentWeight -= item.itemWeight;
            itemsCounter[item.itemId]++;
            inventoryScript.items.Remove(item);
        }
        
        CheckUpgrade();
        // foreach (Item item in inventoryScript.items.ToList())
        // {
        //     inventoryScript.currentWeight -= item.itemWeight;
        //     itemsInDam.Add(item);
        //     inventoryScript.items.Remove(item);
        // }
    }

    void CheckUpgrade()
    {
        for (int i = 0; i < itemsCounter.Length; i++)
        {
            if (itemsCounter[i] >= currentUpgrade[i] && !itemsDone[i])
            {
                itemsCounter[i] -= currentUpgrade[i];
                itemsDone[i] = true;
            }
        }
        
        if (CheckDoneItems())
        {
            Debug.Log("I UPGRADED");
            for (int i = 0; i < itemsDone.Length; i++)
            {
                itemsDone[i] = false;
            }

            damLevel++;

            GenerateUpgrade();
        }
    }
    
    bool CheckDoneItems()
    {
        for (int i = 0; i < itemsDone.Length; i++)
        {
            if (!itemsDone[i])
            {
                return false;
            }
        }
        return true;
    }


    int[] GenerateUpgrade()
    {
        int sticks = Random.Range(1, 5);
        int logs = Random.Range(1, 5);
        int stones = Random.Range(1, 5);
        int leaves = Random.Range(1, 5);
        
        int[] requiredItems = new int[4];

        requiredItems[(int)ItemsEnum.Stick] = sticks;
        requiredItems[(int)ItemsEnum.Log] = logs;
        requiredItems[(int)ItemsEnum.Rock] = stones;
        requiredItems[(int)ItemsEnum.Leaf] = leaves;
        
        return requiredItems;
    }
}