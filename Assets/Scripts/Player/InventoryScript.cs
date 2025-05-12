using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<Item> items;

    public float maxWeight = 10f;
    
    // Might remove variable in the future, being replaced by a foreach loop in the items Interact function
    public float currentWeight = 0f;
    
    private Text inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = GameObject.Find("UI").transform.GetChild(1).gameObject.GetComponent<Text>();
        UpdateInventoryText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventoryText()
    {
        string test = null;
        foreach (Item item in items)
        {
            test += $"{item.itemName}\n";
        }
        inventoryUI.text = $"Current Inventory: {currentWeight}/{maxWeight}\n" + test;
    }
}
