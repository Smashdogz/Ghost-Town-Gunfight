using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1000f;

    public  Transform playerBody;

    float xRotation = 0f;

    // This disables the mouse cursor from appearing on the screen during gameplay
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked; 
    }

    // This enables the mouse to control the cameras view while also locking the cameras view of the x axis between two points
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    
        xRotation -= mouseY;
        xRotation  = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
