using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<Item> items;
    public float maxWeight = 10f;
    
    public float currentWeight = 0f;
    
    private Text inventoryUI;
    
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        
        inventoryUI = gameManager.UI.transform.GetChild(1).gameObject.GetComponent<Text>();
        UpdateInventoryText();
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
