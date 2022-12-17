using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeMovementState : CameraBaseState
{
    public CameraFreeMovementState() : base()
    {
    }

    public CameraFreeMovementState(CameraController currentContext, CameraStateManager currentManager) : base(currentContext, currentManager)
    {
    }
    public override void CheckSwitchState()
    {
        Debug.Log(Context.CurrentAnchor);
        if (Context.IsTraveling)
        {
            SwitchState(Manager.GetState<CameraCutsceneState>());
        }
        if (Context.CurrentAnchor != null)
        {
            SwitchState(Manager.GetState<CameraAnchoredState>());
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        Context.MoveCamera(Context.MovementInput);
        CheckSwitchState();
    }
}
