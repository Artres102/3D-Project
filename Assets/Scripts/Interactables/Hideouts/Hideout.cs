using UnityEngine;

public class InteractableHideout : MonoBehaviour, IInteractable
{
    private bool isInsideHideout = false;
    private int originalLayer = -1;
    private GameObject player;
    
    private GameManager gameManager;

    void Start()
    {
        if (!player) player = gameManager.player;
    }

    public bool Interact(Interactor interactor)
    { 
        // Original layer change
        if (originalLayer == -1)
        {
            originalLayer = player.layer;
        }

        // Get movement and renderer components
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        MeshRenderer playerRenderer = player.GetComponentInChildren<MeshRenderer>();

        if (playerMovement == null || playerRenderer == null)
        {
            Debug.LogError("Missing PlayerMovement or MeshRenderer.");
            return false;
        }

        // Toggle 
        isInsideHideout = !isInsideHideout;

        // Enable/disable movement and visibility
        playerMovement.enabled = !isInsideHideout;
        playerRenderer.enabled = !isInsideHideout;

        // Change layer to ignore raycast while hidden
        if (isInsideHideout)
        {
            player.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            player.layer = originalLayer;
        }

        // Hide UI
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null && canvas.transform.childCount > 0)
        {
            GameObject interactText = canvas.transform.GetChild(0).gameObject;
            interactText.SetActive(false);
        }

        return true;
    }
}