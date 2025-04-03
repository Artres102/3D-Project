using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<Item> items;

    public float maxWeight = 100f;
    
    // Might remove variable in the future, being replaced by a foreach loop in the items Interact function
    public float currentWeight = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
