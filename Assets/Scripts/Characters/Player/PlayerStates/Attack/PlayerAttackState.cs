using UnityEngine;

public class PlayerAttackState : PlayerBaseState,IRootState
{
    public PlayerAttackState() : base()
    {
        IsRootState = true;
    }

    public PlayerAttackState(PlayerController currentContext, PlayerStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {

    }

    public void ComputeGravity()
    {
        Context.GravityValue = Vector2.zero;
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }


}
