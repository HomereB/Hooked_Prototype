using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState() : base()
    {
    }
    public PlayerRunState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {}

    public override void CheckSwitchState()
    {
        if (!Context.IsMovementPressed)
            SwitchState(Manager.GetState<PlayerIdleState>());
    }

    public override void EnterState()
    {
        Context.MovementValue = Context.MovementInput * Context.PlayerSpeed * Time.deltaTime;
    }

    public override void ExitState() {}

    public override void InitializeSubState() {}

    public override void UpdateState()
    {
        Context.MovementValue = Context.MovementInput * Context.PlayerSpeed * Time.deltaTime;
        if ((Context.IsAgainstWallLeft && Context.MovementValue.x < 0) || (Context.IsAgainstWallRight && Context.MovementValue.x > 0))
            Context.MovementValue = Vector2.zero;
        Context.PlayerAnimator.SetFloat("HorizontalSpeed", Mathf.Abs(Context.MovementInput.x));
        CheckSwitchState();
    }
}
