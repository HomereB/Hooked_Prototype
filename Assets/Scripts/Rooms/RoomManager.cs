using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private CameraAnchor cameraAnchor;
    private bool isCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterRoom()
    {
        if(isCleared)
        {
            InitializeRoom();
        }
    }

    private void InitializeRoom()
    {
        
    }
}
