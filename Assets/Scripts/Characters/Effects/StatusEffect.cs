using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected EffectData effectData;

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
    public abstract void HandleEffect();
    public abstract void StackEffect(StatusEffect effect);
}
