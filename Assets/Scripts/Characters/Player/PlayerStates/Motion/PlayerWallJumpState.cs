using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState, IRootState
{
    public PlayerWallJumpState() : base()
    {
        IsRootState = true;
    }

    public PlayerWallJumpState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
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
        else if (Context.CurrentJumpTime > Context.playerWallJumpData.maxJumpTime || !Context.IsJumpPressed)
            SwitchState(Manager.GetState<PlayerFallState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        InitializeSubState();
        ComputeGravity();

        if (Context.IsAgainstWallRight)
            Context.JumpValue = new Vector2(Context.playerWallJumpData.initialJumpVelocity.x * -1, Context.playerWallJumpData.initialJumpVelocity.y);
        else
            Context.JumpValue = Context.playerWallJumpData.initialJumpVelocity;

        Jump();
        Context.PlayerAnimator.SetBool("isJumping", true);
        Context.PlayerAnimator.SetInteger("JumpAmount", Context.CurrentJumpAmount);
    }

    public override void ExitState()
    {
        Context.CurrentJumpTime = 0;
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
        Jump();
        Context.CurrentJumpTime += Time.deltaTime;
        CheckSwitchState();
    }

    void Jump()
    {
        Context.JumpValue -= Context.playerWallJumpData.speedDecreaseRate;
        if (Context.JumpValue.y <= Context.playerWallJumpData.minJumpVelocity)
        {
            Context.JumpValue = new Vector2(Context.JumpValue.x, Context.playerWallJumpData.minJumpVelocity);
        }
    }

    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }
}
