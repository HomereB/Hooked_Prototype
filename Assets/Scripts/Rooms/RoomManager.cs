using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private CameraAnchor cameraAnchor;
    public bool isCleared = false;

    private CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.FindObjectOfType<CameraController>(); //TODO : No
        cameraAnchor = GetComponentInChildren<CameraAnchor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterRoom()
    {
        if(!isCleared)
        {
            InitializeRoom();
        }
        cameraController.CurrentAnchor = cameraAnchor;
        cameraAnchor.enabled = true;
    }

    public void CompleteRoom()
    {
        isCleared = true;
    }

    public void ExitRoom()
    {
        cameraAnchor.enabled = false;
    }

    private void InitializeRoom()
    {
        
    }
}
