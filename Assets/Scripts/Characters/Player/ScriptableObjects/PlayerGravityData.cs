using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GravityData", menuName = "ScriptableObjects/Character/Player/Gravity", order = 2)]
public class PlayerGravityData : ScriptableObject
{
    public Vector2 baseVelocity;
    public Vector2 maximumVelocity;
    public Vector2 gravity;
    public float gravityScale;
}
