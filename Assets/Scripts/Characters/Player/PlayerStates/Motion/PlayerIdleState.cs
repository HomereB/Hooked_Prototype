using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState() : base()
    {
    }

    public PlayerIdleState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
    }

    public override void CheckSwitchState()
    {
        if (Context.IsMovementPressed)
            SwitchState(Manager.GetState<PlayerRunState>());
    }

    public override void EnterState()
    {
        Context.PlayerAnimator.SetFloat("HorizontalSpeed", 0f);
    }

    public override void ExitState() {}

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        //Add inertia?
        Context.MovementValue = Vector2.zero;
        CheckSwitchState();
    }
}
