using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
   //[SerializeField] private float rotationSpeed = 5f;
    private Rigidbody rb;
    
    [SerializeField] private Transform attachedCamera;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
        SpeedCheat();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");     
    }

    private void Move()
    {
        Vector3 camForward = attachedCamera.forward;
        camForward.y = 0; // Ignore vertical component
        camForward.Normalize();

        Vector3 camRight = attachedCamera.right;
        camRight.y = 0; // Ignore vertical component
        camRight.Normalize();

        // Calculate the movement direction based on input
        Vector3 movementDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        // Check if there is significant movement input
        if (movementDirection.magnitude > 0.1f) 
        {
            rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.z * movementSpeed);
        
            // Calculate the target rotation based on the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Preserve the current X rotation while updating the Y rotation
            Vector3 currentRotation = transform.rotation.eulerAngles;
            targetRotation = Quaternion.Euler(currentRotation.x, targetRotation.eulerAngles.y, currentRotation.z);

            // Smooth rotation of the player object
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void FixedUpdate()
    {
        Move();
        
    }

    private void SpeedCheat()
    {
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            movementSpeed += 1f;
        } else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            movementSpeed -= 1f;
        }
    }
}
