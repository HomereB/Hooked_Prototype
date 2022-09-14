using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Character/Player/Health", order = 6)]

public class PlayerHealthData : CharacterHealthData
{
    [SerializeField]
    public float timeBeforeCompleteLoss;
    [SerializeField]
    public float damagePercentageRegen;
}
