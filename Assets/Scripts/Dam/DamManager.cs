using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamManager : MonoBehaviour, IInteractable
{
    [SerializeField] private int[] itemsCounter = new int[4];
    [SerializeField] private int[] currentUpgrade;
    private int damLevel = 0;
    private Text damUpgradeUI;
    private GameObject interactionText;
    private GameObject depositedText;
    private GameObject damGrownUI;
    private Text damGrownText;
    
    private InventoryScript inventoryScript;
    private GameManager gameManager;
    private List<Item> playerItems;

    void Start()
    {
        gameManager = GameManager.Instance;
        inventoryScript = gameManager.player.GetComponent<InventoryScript>();
        playerItems = inventoryScript.items;

        interactionText = GameManager.Instance.interactionCanvas.transform.GetChild(0).gameObject;
        depositedText = GameManager.Instance.interactionCanvas.transform.GetChild(2).gameObject;
        damGrownUI = GameManager.Instance.interactionCanvas.transform.GetChild(3).gameObject;
        
        damGrownText = damGrownUI.GetComponent<Text>();
        
        damUpgradeUI = gameManager.damUI.GetComponent<Text>();
        currentUpgrade = GenerateUpgrade();
        DisplayCurrentUpgrade();
    }

    public bool Interact(Interactor interactor)
    {
        if (!interactor) return false;
        
        DepositItems();
        inventoryScript.UpdateInventoryText();

        bool upgraded = CheckUpgrade();
        if (!upgraded) ChangeUI();
        
        DisplayCurrentUpgrade();
        return true;
    }

    void DepositItems()
    {
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
    }

    void ChangeUI()
    {
        interactionText.SetActive(false);
        StartCoroutine(UITimer(depositedText));
    }

    bool CheckUpgrade()
    {
        if (!CheckDoneItems()) return false;
        
        for (int i = 0; i < itemsCounter.Length; i++)
        {
            itemsCounter[i] -= currentUpgrade[i];
        }

        damLevel++;

        float damYScale = transform.localScale.y * (damLevel + 1);
        Vector3 newScale = new Vector3(transform.localScale.x, damYScale, transform.localScale.z);
        transform.localScale = newScale;

        currentUpgrade = GenerateUpgrade();
        
        damGrownText.text = $"Your Dam has grown to level {damLevel}";
        StartCoroutine(UITimer(damGrownUI));
        return true;
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

        requiredItems[(int)ItemsEnum.Stick] = Random.Range(1, 5);
        requiredItems[(int)ItemsEnum.Log] = Random.Range(1, 5);
        requiredItems[(int)ItemsEnum.Rock] = Random.Range(1, 5);
        requiredItems[(int)ItemsEnum.Leaf] = Random.Range(1, 5);

        return requiredItems;
    }

    void DisplayCurrentUpgrade()
    {
        damUpgradeUI.text = "Goals\n\n" +
                            $"{itemsCounter[(int)ItemsEnum.Leaf]}/{currentUpgrade[(int)ItemsEnum.Leaf]}\n\n" +
                            $"{itemsCounter[(int)ItemsEnum.Stick]}/{currentUpgrade[(int)ItemsEnum.Stick]}\n\n" +
                            $"{itemsCounter[(int)ItemsEnum.Log]}/{currentUpgrade[(int)ItemsEnum.Log]}\n\n" +
                            $"{itemsCounter[(int)ItemsEnum.Rock]}/{currentUpgrade[(int)ItemsEnum.Rock]}";
    }

    private IEnumerator UITimer(GameObject UI)
    {
        UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        UI.SetActive(false);
    }
}
