using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
   //[SerializeField] private float rotationSpeed = 5f;
    private Rigidbody rb;
    
    [SerializeField] private Transform attachedCamera;

    private Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleInvisibleCheat();
        }
        
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
        camForward.Normalize();

        Vector3 camRight = attachedCamera.right;
        camRight.Normalize();

        // Calculate the movement direction
        Vector3 movementDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        if (movementDirection.magnitude > 0.1f) 
        {
            animator.SetBool("Walking", true);
            rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.z * movementSpeed);
        
            // Calculate the target rotation and the current rotation to keep the player on its side
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            
            targetRotation = Quaternion.Euler(currentRotation.x, targetRotation.eulerAngles.y, currentRotation.z);

            // Smooth rotation of the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 15f);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("Walking", false);
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

    private void ToggleInvisibleCheat()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
    }
}
