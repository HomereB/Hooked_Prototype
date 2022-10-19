using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour, IHitable
{
    protected PlayerHealthManager healthManager;
    protected StatusEffectManager statusEffectManager;
    [SerializeField]
    protected bool isStunned = false;
    [SerializeField]
    protected bool isDowned = false;

    public PlayerHealthManager HealthManager { get => healthManager; set => healthManager = value; }
    public StatusEffectManager StatusEffectManager { get => statusEffectManager; set => statusEffectManager = value; }
    public bool IsStunned { get => isStunned; set => isStunned = value; }
    public bool IsDowned { get => isDowned; set => isDowned = value; }
    public virtual void Hit(bool downed, Vector2 ejectionForce) { }
}
