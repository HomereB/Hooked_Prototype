using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    public override void ApplyEffect(StatusEffectManager manager)
    {
        base.ApplyEffect(manager);
        if (entityController.StatusEffectManager.IsDowned == false)
        {
            currentStatusDuration = EffectData.maxEffectDuration;
            entityController.StatusEffectManager.IsStunned = true;
        }
        else
        {
            Destroy(this);
        }
    }

    public override void HandleEffect()
    {
        currentStatusDuration -= Time.deltaTime;
        if(currentStatusDuration <= 0)
        {
            effectManager.RemoveEffect(this);
        }
    }

    public override void RemoveEffect()
    {
        currentStatusDuration = 0;
        entityController.StatusEffectManager.IsStunned = false;
        Destroy(this);
    }

    public override void StackEffect(StatusEffect effect)
    {
        if (currentStatusDuration < effect.EffectData.maxEffectDuration && entityController.StatusEffectManager.IsDowned == false)
        {
            currentStatusDuration = effectData.maxEffectDuration;
        }
        Destroy(effect);
    }
}
