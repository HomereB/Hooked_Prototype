using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookStartupState : PlayerBaseState, IRootState
{
    public PlayerHookStartupState() : base()
    {
        IsRootState = true;

    }

    public PlayerHookStartupState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.IsStunned || Context.IsDowned)
            SwitchState(Manager.GetState<PlayerImpairedState>());
        else if (Context.HookManager.hookStatus == HookStatus.Travel)
        {
            SwitchState(Manager.GetState<PlayerHookTravelState>());
        }
        else if(Context.HookManager.hookStatus == HookStatus.Cooldown)
        {
            Context.HookManager.EndHook(Context.playerHookData.hookCooldownOnMiss);
            if (Context.IsGrounded)
            {
                SwitchState(Manager.GetState<PlayerGroundedState>());
            }
            else
            {
                SwitchState(Manager.GetState<PlayerFallState>());
            }
        }
    }

    public override void EnterState()
    {
        Context.HookManager.InitializeHookThrow();
        Context.JumpValue = Vector2.zero;
        Context.MovementValue = Vector2.zero;
        ComputeGravity();
    }

    public override void ExitState() 
    {
        Context.HookManager.CurrentTravelTimer = 0;
        if (Context.IsHookPressed)
            Context.NeedNewHookPressed = true;
    }

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        if (Context.HookManager.CurrentTravelTimer < Context.playerHookData.travelTime)
        {
            Context.HookManager.CurrentTravelTimer += Time.deltaTime;
            float speed = Context.playerHookData.maxHookDistance / Context.playerHookData.travelTime;
            Context.HookManager.transform.position += Context.HookManager.HookDirection * speed * Time.deltaTime;
        }
        else
        {
            Context.HookManager.hookStatus = HookStatus.Cooldown;
        }
        Context.JumpValue = Vector2.zero;
        Context.MovementValue = Vector2.zero;
        CheckSwitchState();
    }
    
    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }
}