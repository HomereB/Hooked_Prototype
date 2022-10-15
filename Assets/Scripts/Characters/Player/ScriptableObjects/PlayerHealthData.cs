using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Character/Player/Health", order = 6)]

public class PlayerHealthData : CharacterHealthData
{
    [SerializeField]
    public float regainableHealthLossRate;
    [SerializeField]
    public float regainableHealthTimeLossBegin;
    [SerializeField]
    public float damagePercentageRegen;
}
