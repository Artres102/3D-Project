using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{ 
    public float distance;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            GameObject interactText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            Debug.Log(interactText);
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + "Out");
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            GameObject interactText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            interactText.SetActive(false);
        }
        
        Debug.Log(distance);
    }

    void OnTriggerStay(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
    }
}
