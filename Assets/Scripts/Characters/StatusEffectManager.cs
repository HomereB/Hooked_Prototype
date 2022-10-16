using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public HashSet<StatusEffect> statusEffects;
    // Start is called before the first frame update
    void Start()
    {
        statusEffects = new HashSet<StatusEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            effect.HandleEffect();
        }
    }
}
