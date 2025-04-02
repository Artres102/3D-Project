using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Object related variables
    private Transform cameraTransform;
    private GameObject target;

    // Rotation related variables
    [SerializeField] private float sensitivity;
    private float yRotation;
    private Vector3 rotate;

    // Position related variables
    [SerializeField] private Transform cameraPositionTransform;

    // Collision related variables
    public LayerMask collideAgainst = 1; // Layers to collide with
    public float minimumDistanceFromTarget = 0.1f; // Minimum distance from the target
    public float cameraRadius = 0.1f; // Radius of the camera for collision detection

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        cameraPositionTransform = transform.GetChild(0);

        if (target != null && cameraTransform != null)
        {
            MoveCamera();
            Debug.Log(target.name);
        }
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = target.transform.position;
        yRotation = Input.GetAxis("Mouse X");
        rotate = new Vector3(0, yRotation, 0) * sensitivity * Time.deltaTime;

        transform.rotation *= Quaternion.Euler(rotate);

        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 cameraPosition = cameraPositionTransform.position;

        Vector3 directionToTarget = (targetPosition - cameraPosition).normalized;
        float distanceToTarget = Vector3.Distance(cameraPosition, targetPosition);

        // Check if the camera is too close to the target
        if (distanceToTarget < minimumDistanceFromTarget)
        {
            // Pull the camera back to maintain minimum distance
            cameraPosition = targetPosition - directionToTarget * minimumDistanceFromTarget;
        }
        else
        {
            // Perform a raycast to check for obstacles
            RaycastHit hit;
            if (Physics.SphereCast(targetPosition, cameraRadius, -directionToTarget, out hit, distanceToTarget, collideAgainst))
            {
                // If an obstacle is hit, pull the camera forward
                Vector3 newCameraPosition = hit.point + directionToTarget * cameraRadius; // Adjust position to be in front of the obstacle
            
                // Maintain or increase the Y position
                newCameraPosition.y = Mathf.Max(newCameraPosition.y, cameraPosition.y);
                cameraPosition = newCameraPosition;
            }
        }

        // Update the camera's position and rotation
        cameraTransform.position = cameraPosition;
        cameraTransform.LookAt(targetPosition); // Always look at the target
    }
}