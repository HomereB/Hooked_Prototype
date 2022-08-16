using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (Context.IsJumpPressed && Context.CanJump)
            SwitchState(Manager.Jump());
        else if (!Context.IsGrounded)
            SwitchState(Manager.Fall());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.Dash());
    }

    public override void EnterState()
    {
        Context.CurrentJumpAmount = 0;
        Context.JumpValue = Vector2.zero;
        InitializeSubState();
        ComputeGravity();
    }

    public override void ExitState() {}

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
        CheckSwitchState();
    }
    
    public void ComputeGravity()
    {
        Context.GravityValue = Context.playerGravityData.groundedGravity;
    }
}
