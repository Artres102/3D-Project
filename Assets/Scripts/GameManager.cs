using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject camera;
    public GameObject damUI;
    public GameObject interactionCanvas;
    public GameObject inventoryUI;
    public GameObject damUpgradedUI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        damUI = GameObject.FindGameObjectWithTag("DamUI");
        interactionCanvas = GameObject.FindGameObjectWithTag("Interaction Canvas");
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        damUpgradedUI = GameObject.FindGameObjectWithTag("DamUpgradedUI");
    }
}