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
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");     
    }

   /* private void Move()
    {
        float yaw = attachedCamera.eulerAngles.y;
        Vector3 camForward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 camRight = Quaternion.Euler(0, yaw, 0) * Vector3.right;
        
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        //moveDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    } */
   private void Move()
   {
       Vector3 camForward = attachedCamera.forward;
       camForward.y = 0;
       camForward.Normalize();

       Vector3 camRight = attachedCamera.right;
       camRight.y = 0;
       camRight.Normalize();
       
       Vector3 movementDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

       if (movementDirection.magnitude > 0.1f) 
       {
           rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.z * movementSpeed);
           
           if (verticalInput > 0)  // Only rotate if moving forward
           {
               Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            
               //Smooth rotation 
               //transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * 5f);
           }
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
}
