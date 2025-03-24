using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{ 
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            GameObject interactText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            interactText.SetActive(false);
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
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name + "Still here");
        var interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
                Debug.Log("I WORK LMAO");
            }
        }
    }
}
