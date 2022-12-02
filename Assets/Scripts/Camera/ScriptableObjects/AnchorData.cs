using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Camera/AnchorData", order = 2)]

public class AnchorData : ScriptableObject
{
    public float initialSpeed;
    public float lerpIntensity;

    //Thresholds
    public Vector3 minPos;
    public Vector3 maxPos;
}
