using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DamManager : MonoBehaviour
{
    private List<Item> itemsInDam = new List<Item>();

    private InventoryScript inventoryScript;

    private List<Item> playerItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(itemsInDam.Count + " Items in Dam");
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
            inventoryScript.currentWeight -= item.itemWeight;
            itemsInDam.Add(item);
            inventoryScript.items.Remove(item);
        }
    }
}
