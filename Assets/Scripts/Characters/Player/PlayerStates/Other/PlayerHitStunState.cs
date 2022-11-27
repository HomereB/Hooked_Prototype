using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitStunState : PlayerBaseState
{
    public PlayerHitStunState() : base()
    {

    }

    public PlayerHitStunState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {

    }

    public override void CheckSwitchState()
    {
        if (!Context.StatusEffectManager.IsStunned)
        {
            if (Context.StatusEffectManager.IsDowned)
                SwitchState(Manager.GetState<PlayerDownedState>());
            /*else
                SwitchState(Manager.GetState<PlayerImpairedState>());*/
        }
    }

    public override void EnterState()
    {
        Context.JumpBehaviour.ActivateJump(null); //TODO : Both ? 
    }

    public override void ExitState()
    {
        Context.HealthManager.StartInvulnerability();
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Context.MovementValue = Vector2.zero;

        CheckSwitchState();
    }
}
