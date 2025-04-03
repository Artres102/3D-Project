using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideout : MonoBehaviour, IInteractable
{
    private bool isPlayerInside = false;
    private bool isInsideHideout = false; 
    private PlayerMovement playerMovement;
    
    private MeshRenderer playerRenderer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInside = true;
            Debug.Log("Player inside true");

            playerMovement = other.GetComponent<PlayerMovement>(); 
            playerRenderer = other.GetComponent<MeshRenderer>(); 

            if (playerRenderer == null) 
            {
                playerRenderer = other.GetComponentInChildren<MeshRenderer>(); //trocar se deixar de ser child
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E)) 
        {
            ToggleHideout();
        }
    }

    private void ToggleHideout()
    {
        if (playerMovement != null) 
        {
            isInsideHideout = !isInsideHideout; 

            if (isInsideHideout)
            {
                playerMovement.enabled = false; //np movement
                if (playerRenderer != null) playerRenderer.enabled = false; //player hides
            }
            else
            {
                playerMovement.enabled = true; //movement
                if (playerRenderer != null) playerRenderer.enabled = true; //player shwos up
            }
        }
    }

    public bool Interact(Interactor iInteractor)
    {
        
    }

}