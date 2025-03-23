using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
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
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");     
    }

    private void Move()
    {
        float yaw = attachedCamera.eulerAngles.y;
        Vector3 camForward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 camRight = Quaternion.Euler(0, yaw, 0) * Vector3.right;

        moveDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    }

    void FixedUpdate()
    {
        Move();
    }
}
