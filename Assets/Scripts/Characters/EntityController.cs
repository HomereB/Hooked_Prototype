using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour, IHitable
{
    protected PlayerHealthManager healthManager;
    protected StatusEffectManager statusEffectManager;


    public PlayerHealthManager HealthManager { get => healthManager; set => healthManager = value; }
    public StatusEffectManager StatusEffectManager { get => statusEffectManager; set => statusEffectManager = value; }

    public virtual void Hit(bool downed, Vector2 ejectionForce) { }
}
