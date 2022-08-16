using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {}

    public override void CheckSwitchState()
    {
        if (!Context.IsMovementPressed)
            SwitchState(Manager.Idle());
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
        CheckSwitchState();
    }
}
