using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody rigidbodyPlayer;
    private float mouseX;
    private float mouseY;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -35, 60);
        rigidbodyPlayer.transform.rotation  = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
