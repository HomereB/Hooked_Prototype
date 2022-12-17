using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Camera/CameraData", order = 1)]
public class CameraData : ScriptableObject
{
    public float standardCameraMovementSpeed;
    public float standardCameraRotationSpeed;
    public float verticalRotationThresholdUp;
    public float verticalRotationThresholdDown;
    public float minZoomThreshold;
    public float maxZoomThreshold;
}
