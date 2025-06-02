using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;
    public float itemWeight;
    public int itemId;

    private GameObject interactText;
    private GameObject inventoryFullText;
    private InventoryScript inventoryManager;
    
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        
        GameObject interactionCanvas = gameManager.interactionCanvas;
        interactText = interactionCanvas.transform.GetChild(0).gameObject;
        inventoryFullText = interactionCanvas.transform.GetChild(1).gameObject; 
        inventoryManager = gameManager.player.GetComponent<InventoryScript>();
    }
    public bool Interact(Interactor interactor)
    { 
        if (!interactor) return false;
        
        Debug.Log(itemWeight + " " + itemName);
        
        bool canAdd = AddWeight(inventoryManager);

        interactText.SetActive(false);
        if (canAdd)
        {
            //add partcticle
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
        inventoryManager.UpdateInventoryText();
        
        return true;
    }

    private IEnumerator FullInventoryText(GameObject inventoryFullText)
    {
        inventoryFullText.SetActive(true);
        yield return new WaitForSeconds(1f);
        inventoryFullText.SetActive(false);
    }
}
