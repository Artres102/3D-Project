using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100f;
    private float yRotation;
    private Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        yRotation = Input.GetAxis("Mouse X");
        
        rotate = new Vector3(0,yRotation,0) * sensitivity * Time.deltaTime;
        
        transform.eulerAngles += rotate;
    }
}
