using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected EffectData effectData;
    protected StatusEffectManager effectManager;
    protected PlayerController entityController; //TODO : Switch to EntityController / CharacterController
    [SerializeField]
    protected float currentStatusDuration;

    public float CurrentStatusDuration { get => currentStatusDuration; set => currentStatusDuration = value; }
    public EffectData EffectData { get => effectData; set => effectData = value; }

    public virtual void ApplyEffect(StatusEffectManager manager)
    {
        effectManager = manager;
        entityController = manager.Context;
    }
    public abstract void RemoveEffect();
    public abstract void HandleEffect();
    public abstract void StackEffect(StatusEffect effect);
}
