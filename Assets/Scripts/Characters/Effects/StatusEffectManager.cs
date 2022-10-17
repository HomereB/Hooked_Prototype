using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectManager : MonoBehaviour
{
    PlayerController context;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    public StatusEffectManager(PlayerController currentContext)
    {
        context = currentContext;
    }

    void Update()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            effect.HandleEffect();
        }
    }

    public void AddEffect(StatusEffect effect)
    {
        if(statusEffects.Contains(effect))
        {
            statusEffects[statusEffects.IndexOf(effect)].StackEffect(effect);
        }
        else
        {
            statusEffects.Add(effect);
        }
    }

    public void RemoveEffect(StatusEffect effect)
    {
        statusEffects.Remove(effect);
    }
}
