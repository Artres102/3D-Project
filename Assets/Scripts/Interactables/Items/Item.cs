using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public float itemWeight;

    public int itemId;
    
    public bool Interact(Interactor interactor)
    { 
        GameObject interactText = GameObject.Find("Interaction Canvas").transform.GetChild(0).gameObject;
        GameObject inventoryFullText = GameObject.Find("Interaction Canvas").transform.GetChild(1).gameObject;
        InventoryScript inventoryManager = GameObject.FindWithTag("Player").GetComponent<InventoryScript>();
        
        Debug.Log(itemWeight + " " + itemName);
        
        bool canAdd = AddWeight(inventoryManager);

        interactText.SetActive(false);
        if (canAdd)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(FullInventoryText(inventoryFullText));
        }
        
        return true;
    }

    private bool AddWeight(InventoryScript inventoryManager)
    {
        if (inventoryManager == null)
        {
            return false;
        }
        
        if (inventoryManager.currentWeight + itemWeight > inventoryManager.maxWeight)
        {
            return false;
        }
        
        
        inventoryManager.currentWeight += itemWeight;
        inventoryManager.items.Add(this);
        inventoryManager.ShowInventory();
        
        return true;
    }

    private IEnumerator FullInventoryText(GameObject inventoryFullText)
    {
        inventoryFullText.SetActive(true);
        yield return new WaitForSeconds(1f);
        inventoryFullText.SetActive(false);
    }
}
