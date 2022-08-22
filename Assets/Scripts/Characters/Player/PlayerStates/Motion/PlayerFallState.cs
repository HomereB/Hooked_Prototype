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
        if (Context.IsGrounded)
            SwitchState(Manager.GetState<PlayerGroundedState>());
        else if (Context.IsJumpPressed && Context.CanJump)
            SwitchState(Manager.GetState<PlayerJumpState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        InitializeSubState();
        Context.JumpValue = Vector2.zero;
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
        Context.GravityValue += Context.playerGravityData.gravity * Context.playerGravityData.gravityScale;
        CheckSwitchState();
    }
    public void ComputeGravity()
    {
        Context.GravityValue = Context.playerGravityData.baseVelocity;
    }  
}