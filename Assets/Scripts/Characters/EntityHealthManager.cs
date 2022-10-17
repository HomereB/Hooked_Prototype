using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealthManager : MonoBehaviour, IDamageable
{
    protected float currentHealth;
    protected CharacterHealthData characterHealthData;
    private bool isVulnerable;
    private Coroutine invulerabilityCoroutine;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float HealthPercentage{ get => currentHealth / characterHealthData.maxHealth; }
    protected bool IsVulnerable { get => isVulnerable; set => isVulnerable = value; }

    public virtual void AddToCurrentHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, characterHealthData.maxHealth);
    }

    public virtual void SetupHealthManager(CharacterHealthData healthData)
    {
        characterHealthData = healthData;
        currentHealth = healthData.maxHealth;
        isVulnerable = true;
    }

    public virtual void Damage(int value)
    {
        if(isVulnerable)
            currentHealth -= value;
    }

    public virtual void StartInvulnerability()
    {
        StopInvulnerability();
        invulerabilityCoroutine = StartCoroutine("InvulnerabilityCoroutine");
    }

    public virtual void StopInvulnerability()
    {
        if (invulerabilityCoroutine != null)
            StopCoroutine(invulerabilityCoroutine);
        IsVulnerable = true;
    }

    public virtual IEnumerator InvulnerabilityCoroutine()
    {
        isVulnerable = false;
        Debug.Log("invu");
        yield return new WaitForSeconds(characterHealthData.invulnerabilityTime);
        Debug.Log("notInvu");
        isVulnerable = true;
    }
}
