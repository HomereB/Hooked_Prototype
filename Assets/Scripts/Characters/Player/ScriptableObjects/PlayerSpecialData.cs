using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialData", menuName = "ScriptableObjects/Character/Player/Special", order = 5)]

public class PlayerSpecialData : ScriptableObject
{
    [SerializeField]
    public float initialSpecialValue;
    [SerializeField]
    public float MaxSpecialValue;
    [SerializeField]
    public float maxChargeAmount;
    [SerializeField]
    public float regenOverTime;
    [SerializeField]
    public float costMultiplicator;
    [SerializeField]
    public float regenPerHit;

    public float ChargeValue { get => MaxSpecialValue / maxChargeAmount; }
}
