using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookTravelState : PlayerBaseState, IRootState
{
    public PlayerHookTravelState() : base()
    {
        IsRootState = true;
    }

    public PlayerHookTravelState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.HookManager.hookStatus == HookStatus.Cooldown)
        {
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
    }


    public override void ExitState()
    {
        Context.HookManager.EndHook(Context.playerHookData.hookCooldown);
        if (Context.IsHookPressed)
            Context.NeedNewHookPressed = true;
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        if (Vector3.Distance(Context.HookManager.HitPosition, Context.Position) > Context.playerHookData.cutoffDistance)
        {
            Vector2 movementDirection = new Vector2(Context.HookManager.HookDirection.x, Context.HookManager.HookDirection.y);
            Context.MovementValue = movementDirection * Context.playerHookData.playerTravelSpeed * Time.deltaTime;
        }
        else
        {
            Context.HookManager.EndHook(Context.playerHookData.hookCooldown);
        }
        CheckSwitchState();
    }

    public void ComputeGravity()
    {
        Context.GravityBehaviour.ActivateGravity(null);
    }
}