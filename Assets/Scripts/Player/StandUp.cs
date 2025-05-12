using UnityEngine;
using System.Collections;
using UnityEditor;

public class StandUp : MonoBehaviour
{
    public bool standingUp = false;
    private PlayerMovement playerMovement;
    private Quaternion rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (standingUp)
        {
            StartCoroutine(RestoreMovement());
        }
        else
        {
            rotation = Quaternion.Euler(90f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Standingup");
            standingUp = true;
            DisableMovement();
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    public void DisableMovement()
    {
        playerMovement.enabled = false;
    }
    
    private IEnumerator RestoreMovement()
    {
        yield return new WaitForSeconds(1f);
        standingUp = false;
        playerMovement.enabled = true;
        transform.rotation = rotation;
    }
}
