using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private CameraAnchor cameraAnchor;
    private bool isCleared = false;

    private CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.FindObjectOfType<CameraController>(); //TODO : No
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

    }

    private void InitializeRoom()
    {
        
    }
}
