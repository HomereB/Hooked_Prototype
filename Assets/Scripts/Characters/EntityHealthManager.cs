using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealthManager : MonoBehaviour, IDamageable
{
    protected float currentHealth;
    protected CharacterHealthData characterHealthData;
    [SerializeField]

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float HealthPercentage{ get => currentHealth / characterHealthData.maxHealth; }

    public virtual void AddToCurrentHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, characterHealthData.maxHealth);
    }

    public virtual void SetupHealthManager(CharacterHealthData healthData)
    {
        characterHealthData = healthData;
        currentHealth = healthData.maxHealth;
    }

    public virtual void Damage(int value)
    {
        currentHealth -= value;
    }
}
