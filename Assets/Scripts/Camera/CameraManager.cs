﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Camera currentCamera;

    [SerializeField]
    CameraController currentCameraController;





    private void Awake()
    {
        //currentCamera = Camera.main;
        currentCameraController = currentCamera.gameObject.GetComponent<CameraController>();
    }

    private void Update()
    {

    }

    public void AddCameraFilter()
    {
        //TODO
    }

    public void SwitchCamera(Camera camera, bool keepCurrentEffects)
    {
        currentCamera.enabled = false;
        currentCameraController.enabled = false;
        currentCamera = camera;
        currentCameraController = camera.GetComponent<CameraController>();
        currentCamera.enabled = true;
        currentCameraController.enabled = true;
        //TODO : add effect transfer from one camera to another
    }
}
