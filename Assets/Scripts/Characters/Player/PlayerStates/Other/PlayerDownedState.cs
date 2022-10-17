using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownedState : PlayerBaseState
{
    public PlayerDownedState() : base()
    {
    }

    public PlayerDownedState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
    }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Context.PlayerAnimator.SetTrigger("isDowned");
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        if(Context.IsGrounded && Context.MovementInput != Vector2.zero || Context.IsJumpPressed)
        {
            Context.IsDowned = false;
        }
    }
}
