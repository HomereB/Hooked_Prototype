using UnityEngine;

public class PlayerFallState : PlayerBaseState, IRootState
{
    public PlayerFallState() : base()
    {
        IsRootState = true;
    }

    public PlayerFallState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.IsAgainstWall)
            SwitchState(Manager.GetState<PlayerWallRidingState>());
        else if (Context.StatusEffectManager.IsStunned || Context.StatusEffectManager.IsDowned)
            SwitchState(Manager.GetState<PlayerImpairedState>());
        else if (Context.IsHookPressed && Context.HookManager.CanStartHook)
        {
            SwitchState(Manager.GetState<PlayerHookStartupState>());
            Context.CurrentJumpAmount--;
        }
        else if (Context.IsGrounded)
            SwitchState(Manager.GetState<PlayerGroundedState>());
        else if (Context.IsJumpPressed && Context.CanJump)
            SwitchState(Manager.GetState<PlayerJumpState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
        {
            SwitchState(Manager.GetState<PlayerDashState>());
            Context.CurrentJumpAmount--;
        }
    }

    public override void EnterState()
    {
        InitializeSubState();
        Context.JumpBehaviour.ActivateJump(null);
        Context.CurrentJumpAmount++;
        ComputeGravity();
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {
        if (Context.IsMovementPressed)
        {
            SetSubState(Manager.GetState<PlayerRunState>());
        }
        else
        {
            SetSubState(Manager.GetState<PlayerIdleState>());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public void ComputeGravity()
    {
        Context.GravityBehaviour.ActivateGravity(Context.playerGravityData);
    }  
}
