using UnityEngine;
using System.Collections;

public class StandUp : MonoBehaviour
{
    public bool Standingup = false;
    private PlayerMovement playerMovement;

    public bool IsStandingUp()
    {
        playerMovement.enabled = false;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Standingup = IsStandingUp();
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
    }
}
