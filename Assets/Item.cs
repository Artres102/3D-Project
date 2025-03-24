using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable
{
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interact");
        GameObject interactText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        interactText.SetActive(false);
        return true;
    }
}
