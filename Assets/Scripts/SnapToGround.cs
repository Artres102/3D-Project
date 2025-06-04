using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    public float raycastDistance = 1000f;
    public LayerMask groundLayer;
    public float embedOffset = 0.5f; // how much the object sinks into the ground

    void Start()
    {
        Snap();
    }

    void Snap()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * raycastDistance;

        if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance * 2, groundLayer))
        {
            Vector3 newPos = hit.point + Vector3.up * embedOffset;
            transform.position = newPos;
        }
    }
}
