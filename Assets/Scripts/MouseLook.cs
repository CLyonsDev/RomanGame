﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    public float offset = 0.0f;

    public Transform CameraContainer;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        //Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        //transform.rotation = localRotation;
        Quaternion camRot = Quaternion.Euler(rotX, 0.0f, 0.0f);
        Quaternion bodyRot = Quaternion.Euler(0.0f, rotY + offset, 0.0f);

        CameraContainer.transform.localRotation = camRot;
        transform.rotation = bodyRot;
    }

    public void SetRot(float rot)
    {
        offset += rot;

        if (offset > 180)
            offset -= 360;
        else if (offset < -180)
            offset += 360;
    }
}