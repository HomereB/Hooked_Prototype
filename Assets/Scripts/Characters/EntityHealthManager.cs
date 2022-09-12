using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealthManager : MonoBehaviour
{
    private int currentHealth;
    [SerializeField]
    private int maxHealth;

    private HealthBar healthBar;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void AddToCurrentHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if(healthBar != null)
        {
            healthBar.Fill(GetHealthPercentage());
        }
    }

    public float GetHealthPercentage()
    {
        return (float) currentHealth / maxHealth;
    }
}
