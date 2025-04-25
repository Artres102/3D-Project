using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamManager : MonoBehaviour
{
    // private List<Item> itemsInDam = new List<Item>();

    [SerializeField] private int[] itemsCounter = new int[4];
    [SerializeField] private int[] currentUpgrade;
    private int damLevel = 0;
    private Text damUpgradeUI;
    
    private InventoryScript inventoryScript;

    private List<Item> playerItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<InventoryScript>();
        playerItems = inventoryScript.items;

        damUpgradeUI = GameObject.FindWithTag("DamReq").GetComponent<Text>();
        currentUpgrade = GenerateUpgrade();
        
        DisplayCurrentUpgrade();
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

        foreach (Item item in playerItems.ToList())
        {
            Debug.Log("Depositing: " + item.itemName + " (ID: " + item.itemId + ")");
            inventoryScript.currentWeight -= item.itemWeight;
            if (inventoryScript.currentWeight <= 0)
            {
                inventoryScript.currentWeight = 0;
            }
            itemsCounter[item.itemId]++;
            inventoryScript.items.Remove(item);
        }
        inventoryScript.ShowInventory();
        CheckUpgrade();
        
        DisplayCurrentUpgrade();
    }

    void CheckUpgrade()
    {
        if (CheckDoneItems())
        {
            Debug.Log("I UPGRADED");
            for (int i = 0; i < itemsCounter.Length; i++)
            {
                itemsCounter[i] -= currentUpgrade[i];
            }

            damLevel++;

            currentUpgrade = GenerateUpgrade();
        }
        
    }
    
    bool CheckDoneItems()
    {
        for (int i = 0; i < itemsCounter.Length; i++)
        {
            if (itemsCounter[i] < currentUpgrade[i])
            {
                return false;
            }
        }

        return true;
    }

    int[] GenerateUpgrade()
    {
        int[] requiredItems = new int[4];
        if (damLevel < 1)
        {
            requiredItems[(int)ItemsEnum.Leaf] = 3;
            requiredItems[(int)ItemsEnum.Stick] = 2;
            requiredItems[(int)ItemsEnum.Log] = 2;
            requiredItems[(int)ItemsEnum.Rock] = 1;
            
            return requiredItems;
        }
        int sticks = Random.Range(1, 5);
        int logs = Random.Range(1, 5);
        int stones = Random.Range(1, 5);
        int leaves = Random.Range(1, 5);

        requiredItems[(int)ItemsEnum.Stick] = sticks;
        requiredItems[(int)ItemsEnum.Log] = logs;
        requiredItems[(int)ItemsEnum.Rock] = stones;
        requiredItems[(int)ItemsEnum.Leaf] = leaves;
        
        return requiredItems;
    }

    void DisplayCurrentUpgrade()
    {
        damUpgradeUI.text = $"Current Dam Level: {damLevel}\n" +
                            $"Items Required:\n" +
                            $"Leaves - {itemsCounter[(int)ItemsEnum.Leaf]}/{currentUpgrade[(int)ItemsEnum.Leaf]}\n" +
                            $"Sticks - {itemsCounter[(int)ItemsEnum.Stick]}/{currentUpgrade[(int)ItemsEnum.Stick]}\n" +
                            $"Logs - {itemsCounter[(int)ItemsEnum.Log]}/{currentUpgrade[(int)ItemsEnum.Log]}\n" +
                            $"Rocks - {itemsCounter[(int)ItemsEnum.Rock]}/{currentUpgrade[(int)ItemsEnum.Rock]}";;
    }
}