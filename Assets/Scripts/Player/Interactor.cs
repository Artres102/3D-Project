using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{ 
    public float distance;
    [SerializeField] private GameObject interactText;
    private IInteractable interactable;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        
        interactText = gameManager.interactionCanvas.transform.GetChild(0).gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            Debug.Log(interactText);
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + "Out");
        IInteractable otherInteractable = other.GetComponent<IInteractable>();

        if (otherInteractable == interactable)
        {
            interactText.SetActive(false);
            interactable = null;
        }
        
        Debug.Log(distance);
    }

    void Update()
    {
        //var interactable = other.GetComponent<IInteractable>();

        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            //if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
    }
}
