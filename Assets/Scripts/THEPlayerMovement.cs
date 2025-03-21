using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THEPlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody rb;
    
    [SerializeField] private Transform atachcamera;

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
        float yaw = atachcamera.eulerAngles.y;
        Vector3 camForward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 camRight = Quaternion.Euler(0, yaw, 0) * Vector3.right;

        moveDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        Move();
    }
}
