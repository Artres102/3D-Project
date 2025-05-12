using UnityEngine;
using System.Collections;

public class StandUp : MonoBehaviour
{
    public bool standingUp = false;
    private PlayerMovement playerMovement;
    private Quaternion rotation;

    public void DisableMovement()
    {
        playerMovement.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Standingup");
            standingUp = true;
            DisableMovement();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (standingUp)
        {
            StartCoroutine(RestoreMovement());
        }
    }

    private IEnumerator RestoreMovement()
    {
        yield return new WaitForSeconds(1f);
        standingUp = false;
        playerMovement.enabled = true;
        transform.rotation = rotation;
    }
}
