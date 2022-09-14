using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthManager : MonoBehaviour
{
    private float currentHealth;
    private CharacterHealthData characterHealthData;
    [SerializeField]

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float HealthPercentage{ get => currentHealth / characterHealthData.maxHealth; }

    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, characterHealthData.maxHealth);
    }

    public void SetupHealthManager(PlayerHealthData healthData)
    {
        characterHealthData = healthData;
        currentHealth = healthData.maxHealth;
    }
}
