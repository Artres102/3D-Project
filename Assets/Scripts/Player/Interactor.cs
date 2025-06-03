using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        if (interactable == null) return;

        switch (other.tag)
        {
            case "Item":
                ItemText();
                break;
            case "Hideout":
                HideoutText();
                break;
            case "Dam":
                DamText();
                break;
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

    void ItemText()
    {
        interactText.GetComponent<Text>().text = "[E] to Collect";
        interactText.SetActive(true);
    }

    void HideoutText()
    {
        interactText.GetComponent<Text>().text = "[E] to Hide";
        interactText.SetActive(true);
    }

    void DamText()
    {
        interactText.GetComponent<Text>().text = "[E] to Deposit Items";
        interactText.SetActive(true);
    }

    void Update()
    {
        //var interactable = other.GetComponent<IInteractable>();

        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            //if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
                interactText.SetActive(false);
            }
        }
    }
}
