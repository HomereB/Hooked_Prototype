using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetingState : CameraBaseState
{
    public override void CheckSwitchState()
    {
        if (Context.CurrentTarget == null)
        {
            SwitchState(Manager.GetState<CameraFreeMovementState>());
        }
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
        Context.LookAtPosition(Context.CurrentTarget.position);
        CheckSwitchState();
    }
}
