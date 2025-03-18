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
    [SerializeField] private float yPosition = 3;
    [SerializeField] private float zPosition = -5.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        cameraTransform = transform.GetChild(0).transform;

        if (target != null && cameraTransform != null)
        {
            Debug.Log(target.name);
            cameraTransform.LookAt(target.transform.position);

            cameraTransform.position = new Vector3(0, yPosition, zPosition);
        }
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.LookAt(target.transform.position);
        
        yRotation = Input.GetAxis("Mouse X");
        
        rotate = new Vector3(0,yRotation,0) * sensitivity * Time.deltaTime;
        
        transform.rotation *= Quaternion.Euler(rotate);
    }
}
