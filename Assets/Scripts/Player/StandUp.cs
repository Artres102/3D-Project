using UnityEngine;
using System.Collections;

public class StandUp : MonoBehaviour
{
    public bool Standingup = false;
    private PlayerMovement playerMovement;
    private Quaternion rotation;

    public bool IsStandingUp()
    {
        playerMovement.enabled = false;
        return true;
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
            Standingup = IsStandingUp();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Standingup)
        {
            StartCoroutine(RestoreMovement());
        }
    }

    private IEnumerator RestoreMovement()
    {
        yield return new WaitForSeconds(1f);
        Standingup = false;
        playerMovement.enabled = true;
        transform.rotation = rotation;
    }
}
