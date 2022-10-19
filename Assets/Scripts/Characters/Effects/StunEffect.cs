using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect
{
    public override void ApplyEffect(StatusEffectManager manager)
    {
        base.ApplyEffect(manager);
        Debug.Log(entityController.IsDowned);
        if (entityController.IsDowned == false)
        {
            currentStatusDuration = EffectData.maxEffectDuration;
            entityController.IsStunned = true;
            Debug.Log("STUNNED");
        }
        else
        {
            Destroy(this);
        }
    }

    public override void HandleEffect()
    {
        //Debug.Log("handleEffect : " + currentStatusDuration);

        currentStatusDuration -= Time.deltaTime;
        if(currentStatusDuration <= 0)
        {
            effectManager.RemoveEffect(this);
        }
    }

    public override void RemoveEffect()
    {
        currentStatusDuration = 0;
        entityController.IsStunned = false;
        Destroy(this);
    }

    public override void StackEffect(StatusEffect effect)
    {
        if (currentStatusDuration < effect.EffectData.maxEffectDuration && entityController.IsDowned == false)
        {
            currentStatusDuration = effectData.maxEffectDuration;
        }
        Destroy(effect);
    }
}
