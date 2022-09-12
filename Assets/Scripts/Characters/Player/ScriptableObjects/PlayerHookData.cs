using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HookData", menuName = "ScriptableObjects/Character/Player/Hook", order = 4)]
public class PlayerHookData : ScriptableObject
{
    [SerializeField]
    public LayerMask hookLayerMask;
    [SerializeField]
    public float maxHookDistance;
    [SerializeField]
    public float cutoffDistance;
    [SerializeField]
    public float travelTime;
    [SerializeField]
    public float playerTravelSpeed;
    [SerializeField]
    public float hookCooldown;
    [SerializeField]
    public float hookCooldownRefundOnMiss;
}
