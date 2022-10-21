using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectManager : MonoBehaviour
{
    PlayerController context;

    protected bool isStunned = false;
    protected bool isDowned = false;

    public Dictionary<string,StatusEffect> statusEffects;
    private List<string> statusToRemove;
    public PlayerController Context { get => context; set => context = value; }
    public bool IsStunned { get => isStunned; set => isStunned = value; }
    public bool IsDowned { get => isDowned; set => isDowned = value; }

    private void Start()
    {
        statusEffects = new Dictionary<string,StatusEffect>();
        statusToRemove = new List<string>();

    }

void Update()
    {
        foreach(KeyValuePair<string, StatusEffect> effect in statusEffects)
            effect.Value.HandleEffect();
        RemoveObsoleteEffects();
    }

    public void AddEffect(StatusEffect effect)
    {
        string key = effect.EffectData.effectKey;
        if (!statusEffects.ContainsKey(key))
        {
            effect.ApplyEffect(this);
            statusEffects.Add(key,effect);      
        }
        else
        {
            statusEffects[key].StackEffect(effect);
        }
    }

    public void RemoveEffect(StatusEffect effect)
    {
        statusToRemove.Add(effect.EffectData.effectKey);
    }

    private void RemoveObsoleteEffects()
    {
        foreach(string key in statusToRemove)
        {
            statusEffects[key].RemoveEffect();
            statusEffects.Remove(key);
        }
        statusToRemove.Clear();
    }

    public void RemoveAllEffects()
    {
        foreach (KeyValuePair<string, StatusEffect> effect in statusEffects)
            effect.Value.RemoveEffect();
        statusEffects.Clear();
        statusToRemove.Clear();
    }
}
