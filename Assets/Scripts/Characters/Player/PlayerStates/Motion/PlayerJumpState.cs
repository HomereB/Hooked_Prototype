using UnityEngine;

public class PlayerJumpState : PlayerBaseState, IRootState
{
    public PlayerJumpState() : base()
    {
        IsRootState = true;
    }

    public PlayerJumpState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.StatusEffectManager.IsStunned || Context.StatusEffectManager.IsDowned)
            SwitchState(Manager.GetState<PlayerImpairedState>());
        else if (Context.IsHookPressed && Context.HookManager.CanStartHook)
            SwitchState(Manager.GetState<PlayerHookStartupState>());
        else if (Context.IsGrounded)
            SwitchState(Manager.GetState<PlayerGroundedState>());
        else if (Context.JumpBehaviour.IsJumpFinished || !Context.IsJumpPressed)
            SwitchState(Manager.GetState<PlayerFallState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        InitializeSubState();
        ComputeGravity();

        if (Context.IsAgainstWall && !Context.IsGrounded)
        {
            if (Context.IsAgainstWallRight)
                Context.JumpBehaviour.ScaleX = -1;

            Context.JumpBehaviour.ActivateJump(Context.playerWallJumpData);
        }
        else
            Context.JumpBehaviour.ActivateJump(Context.playerJumpData);

        Context.PlayerAnimator.SetBool("isJumping", true);
        Context.PlayerAnimator.SetInteger("JumpAmount", Context.CurrentJumpAmount);
    }

    public override void ExitState()
    {
        Context.PlayerAnimator.SetBool("isJumping", false);

        if (Context.IsJumpPressed)
            Context.NeedNewJumpPressed = true;
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
