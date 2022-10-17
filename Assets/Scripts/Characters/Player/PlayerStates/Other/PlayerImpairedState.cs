using UnityEngine;

public class PlayerImpairedState : PlayerBaseState, IRootState
{
    public PlayerImpairedState() : base()
    {
        IsRootState = true;
    }

    public PlayerImpairedState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if(!Context.IsStunned && !Context.IsDowned)
        {
            if (Context.IsJumpPressed)
            {
                SwitchState(Manager.GetState<PlayerJumpState>());
            }
            else if(Context.IsGrounded)
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
        Context.MovementValue = Vector2.zero;
        Context.JumpValue = Vector2.zero;
        Context.PlayerAnimator.SetTrigger("isHit");
        ComputeGravity();
        InitializeSubState();
    }

    public override void ExitState()
    {
        Context.PlayerAnimator.SetTrigger("recoverFromHit");
    }

    public override void InitializeSubState()
    {
        if(Context.IsStunned)
        {
            SetSubState(Manager.GetState<PlayerHitStunState>());
        }
        else if(Context.IsDowned)
        {
            SetSubState(Manager.GetState<PlayerDownedState>());
        }
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public void ComputeGravity()
    {
        if (Context.IsStunned)
            Context.GravityValue = Vector2.zero;
        else
            Context.GravityValue = Context.playerGravityData.gravity;
    }
}
