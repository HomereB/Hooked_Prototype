using UnityEngine;

public class PlayerFallState : PlayerBaseState, IRootState
{
    public PlayerFallState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.IsGrounded)
            SwitchState(Manager.Grounded());
        else if (Context.IsJumpPressed && Context.CanJump)
            SwitchState(Manager.Jump());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.Dash());
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
            SetSubState(Manager.Run());
        }
        else
        {
            SetSubState(Manager.Idle());
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
