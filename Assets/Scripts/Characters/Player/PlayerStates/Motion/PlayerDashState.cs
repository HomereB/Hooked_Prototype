using UnityEngine;

public class PlayerDashState : PlayerBaseState, IRootState
{
    public PlayerDashState() : base()
    {
        IsRootState = true;
    }
    public PlayerDashState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if(Context.CurrentDashTime > Context.playerDashData.maxDashTime || (Context.CurrentDashTime > Context.playerDashData.minDashTime && !Context.IsDashPressed))
        {
            if(Context.IsGrounded)
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
        InitializeSubState();
        Context.DashDirection = Context.MovementInput;
        Context.MovementValue = Context.DashDirection * Context.playerDashData.dashSpeed;
        Context.JumpValue = Vector2.zero;
        ComputeGravity();
    }

    public override void ExitState()
    {
        Context.CurrentDashTime = 0;
        Context.DashManager.CurrentDashCharges--;
        if(Context.IsDashPressed)
            Context.NeedNewDashPressed = true;
    }

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        Context.CurrentDashTime += Time.deltaTime;
        if(Context.CurrentDashTime < Context.playerDashData.minDashTime || (Context.CurrentDashTime < Context.playerDashData.maxDashTime && Context.IsDashPressed))
        {
            Context.MovementValue = Context.DashDirection * Context.playerDashData.dashSpeed;
        }
        CheckSwitchState();       
    }
    
    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }
}
