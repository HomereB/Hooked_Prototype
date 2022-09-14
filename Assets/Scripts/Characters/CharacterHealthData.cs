using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Character/Health", order = 2)]

public class CharacterHealthData : ScriptableObject
{
    [SerializeField]
    public float maxHealth;
}
