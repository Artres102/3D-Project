using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject camera;
    public GameObject damUI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        damUI = GameObject.FindGameObjectWithTag("DamUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
