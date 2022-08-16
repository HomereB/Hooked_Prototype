using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashData", menuName = "ScriptableObjects/Character/Player/Dash", order = 3)]

public class PlayerDashData : ScriptableObject
{
    [SerializeField]
    public float maxDashTime;
    [SerializeField]
    public float minDashTime;
    [SerializeField]
    public float dashSpeed;
    [SerializeField]
    public float dashSpeedDecrease;
    [SerializeField]
    public float dashCooldown;
    [SerializeField]
    public int maxDashCharges;
}
