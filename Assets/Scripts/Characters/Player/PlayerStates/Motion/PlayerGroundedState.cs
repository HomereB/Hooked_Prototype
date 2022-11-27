using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState() : base()
    {
        IsRootState = true;
    }

    public PlayerGroundedState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.StatusEffectManager.IsStunned || Context.StatusEffectManager.IsDowned)
            SwitchState(Manager.GetState<PlayerImpairedState>());
        else if (Context.IsHookPressed && Context.HookManager.CanStartHook)
            SwitchState(Manager.GetState<PlayerHookStartupState>());
        else if (Context.IsJumpPressed && Context.CanJump)
            SwitchState(Manager.GetState<PlayerJumpState>());
        else if (!Context.IsGrounded)
            SwitchState(Manager.GetState<PlayerFallState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        Context.CurrentJumpAmount = 0;
        Context.JumpBehaviour.ActivateJump(null);
        Context.PlayerAnimator.SetBool("isGrounded", true);
        ComputeGravity();
        InitializeSubState();
    }

    public override void ExitState()
    {
        Context.PlayerAnimator.SetBool("isGrounded", false);
    }

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
        Context.GravityBehaviour.ActivateGravity(null);
    }
}
