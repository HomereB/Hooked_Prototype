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
        if(!Context.IsStunned)
        {
            if (Context.IsDowned)
                SwitchState(Manager.GetState<PlayerDownedState>());
            /*else
                SwitchState(Manager.GetState<PlayerImpairedState>());*/
        }
    }

    public override void EnterState()
    {
        Debug.Log("StunEnter");
    }

    public override void ExitState()
    {
        Debug.Log("StunExit");
        Context.StunTimer = 0;
        Context.HealthManager.StartInvulnerability();
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        Debug.Log("blib");
        Context.MovementValue = Vector2.zero;
        Context.JumpValue = Vector2.zero;

        Context.StunTimer -= Time.deltaTime;
        if(Context.StunTimer < 0)
            Context.IsStunned = false;

        CheckSwitchState();
    }
}
