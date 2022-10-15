using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpairedState : PlayerBaseState
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
        throw new System.NotImplementedException();
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
