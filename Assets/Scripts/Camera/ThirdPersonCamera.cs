using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Object related variables
    private Transform cameraTransform;
    private GameObject target;

    // Rotation related variables
    [SerializeField] private float horizontalSensitivity;
    [SerializeField] private float verticalSensitivity;
    [SerializeField] private float verticalFramingOffset = 1.5f;
    private float yRotation;
    private float xRotation;
    private Vector3 rotate;

    // Position related variables
    [SerializeField] private Transform cameraPositionTransform;

    // Collision related variables
    public LayerMask collideAgainst = 1; // Layers to collide with
    public float minimumDistanceFromTarget = 0.1f;
    public float cameraRadius = 0.1f;

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
        horizontalSensitivity = PlayerPrefs.GetFloat("horizontalSensitivity", 100f);
        verticalSensitivity = PlayerPrefs.GetFloat("verticalSensitivity", 50f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = target.transform.position;

        yRotation += Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        xRotation += Input.GetAxis("Mouse Y") * -1 * verticalSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

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
        Vector3 lookTarget = target.transform.position + Vector3.up * verticalFramingOffset;
        cameraTransform.LookAt(lookTarget);
 
    }
}