using UnityEngine;

public class PlayerWallRidingState : PlayerBaseState, IRootState
{
    public PlayerWallRidingState() : base()
    {
        IsRootState = true;
    }

    public PlayerWallRidingState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
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
        else if (!Context.IsAgainstWall)
            SwitchState(Manager.GetState<PlayerFallState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        InitializeSubState();
        ComputeGravity();

        //if (Context.IsAgainstWallRight)
        //    Context.JumpValue = new Vector2(Context.playerWallJumpData.initialJumpVelocity.x * -1, Context.playerWallJumpData.initialJumpVelocity.y);
        //else
        //    Context.JumpValue = Context.playerWallJumpData.initialJumpVelocity;

        //Jump();
        //Context.PlayerAnimator.SetBool("isJumping", true);
    }

    public override void ExitState()
    {
        //Context.PlayerAnimator.SetBool("isJumping", false);
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
        Context.GravityBehaviour.ActivateGravity(Context.playerWallRidingData);
    }
}
