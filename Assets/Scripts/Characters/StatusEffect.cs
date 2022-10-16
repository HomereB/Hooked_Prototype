using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected float effectDuration;

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
    public abstract void HandleEffect();
}
