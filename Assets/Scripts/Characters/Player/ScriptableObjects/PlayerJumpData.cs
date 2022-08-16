using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = "ScriptableObjects/Character/Player/Jump", order = 1)]
public class PlayerJumpData : ScriptableObject
{
    [SerializeField]
    public Vector2 initialJumpVelocity;
    [SerializeField]
    public float minJumpVelocity;
    [SerializeField]
    public Vector2 speedDecreaseRate;
    [SerializeField]
    public int maxJumpAmount;
    [SerializeField]
    public float maxJumpTime;
}
