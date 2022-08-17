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
        if (Context.IsGrounded)
            SwitchState(Manager.GetState<PlayerGroundedState>());
        else if(Context.CurrentJumpTime > Context.playerJumpData.maxJumpTime || !Context.IsJumpPressed)
            SwitchState(Manager.GetState<PlayerFallState>());
        else if (Context.IsDashPressed && Context.IsMovementPressed && Context.CanDash)
            SwitchState(Manager.GetState<PlayerDashState>());
    }

    public override void EnterState()
    {
        InitializeSubState();
        ComputeGravity();
        Context.JumpValue = Context.playerJumpData.initialJumpVelocity;
        Jump();
    }

    public override void ExitState()
    {
        Context.CurrentJumpTime = 0;
        if(Context.IsJumpPressed)
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
        Context.JumpValue -= Context.playerJumpData.speedDecreaseRate;
        if (Context.JumpValue.y <= Context.playerJumpData.minJumpVelocity)
        {
            Context.JumpValue = new Vector2(Context.JumpValue.x, Context.playerJumpData.minJumpVelocity);
        }
    }

    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }
}
