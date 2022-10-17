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
        if (Context.IsStunned || Context.IsDowned)
            SwitchState(Manager.GetState<PlayerImpairedState>());
        else if (Context.DashManager.CurrentDashTime > Context.playerDashData.maxDashTime || (Context.DashManager.CurrentDashTime > Context.playerDashData.minDashTime && !Context.IsDashPressed))
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
        Context.DashManager.DashDirection = Context.MovementInput;
        Context.MovementValue = Context.DashManager.DashDirection * Context.playerDashData.dashSpeed;
        Context.JumpValue = Vector2.zero;
        ComputeGravity();
        Context.PlayerAnimator.SetBool("isDashing",true);
    }

    public override void ExitState()
    {
        Context.DashManager.CurrentDashTime = 0;
        Context.DashManager.CurrentDashCharges--;
        Context.PlayerAnimator.SetBool("isDashing", false);
        if (Context.IsDashPressed)
            Context.NeedNewDashPressed = true;
    }

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        Context.DashManager.CurrentDashTime += Time.deltaTime;
        if(Context.DashManager.CurrentDashTime < Context.playerDashData.minDashTime || (Context.DashManager.CurrentDashTime < Context.playerDashData.maxDashTime && Context.IsDashPressed))
        {
            Context.MovementValue = Context.DashManager.DashDirection * Context.playerDashData.dashSpeed;
        }
        CheckSwitchState();       
    }
    
    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }
}
