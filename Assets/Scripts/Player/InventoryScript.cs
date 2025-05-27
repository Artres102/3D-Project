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
        
        inventoryUI = gameManager.inventoryUI.GetComponent<Text>();
        UpdateInventoryText();
    }
    
    public void UpdateInventoryText()
    {
        int leafCount = 0;
        int stickCount = 0;
        int logCount = 0;
        int rockCount = 0;
        foreach (Item item in items)
        {
            switch (item.itemId)
            {
                case (int)ItemsEnum.Leaf: leafCount++; break;
                case (int)ItemsEnum.Stick: stickCount++; break;
                case (int)ItemsEnum.Log: logCount++; break;
                case (int)ItemsEnum.Rock: rockCount++; break;
            }
        }
        inventoryUI.text = $"{leafCount}\n\n{stickCount}\n\n{logCount}\n\n{rockCount}";
    }
}
