using UnityEngine;

public class InteractableHideout : MonoBehaviour, IInteractable
{
    private bool isInsideHideout = false;
    private int originalLayer = -1;
    private GameObject player;
    private GameObject canvas;
    private GameObject interactText;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        
        if (!player) player = gameManager.player;
        if (!canvas) canvas = gameManager.interactionCanvas;
        interactText = canvas.transform.GetChild(0).gameObject;
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
        GameObject playerModel = player.transform.GetChild(1).gameObject;

        if (playerMovement == null || playerModel == null)
        {
            Debug.LogError("Missing PlayerMovement or MeshRenderer.");
            return false;
        }

        // Toggle 
        isInsideHideout = !isInsideHideout;

        // Enable/disable movement and visibility
        playerMovement.enabled = !isInsideHideout;
        playerModel.SetActive(!isInsideHideout);

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
        if (canvas != null && canvas.transform.childCount > 0)
        {
            interactText.SetActive(false);
        }

        return true;
    }
}