using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunData", menuName = "ScriptableObjects/Effect/Stun", order = 0)]
public class EffectData : ScriptableObject
{
    public string effectKey;
    public float maxEffectDuration;
    public float intensity;
}
