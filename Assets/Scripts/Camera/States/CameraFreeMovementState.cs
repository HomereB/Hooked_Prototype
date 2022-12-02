using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeMovementState : CameraBaseState
{
    public CameraFreeMovementState() : base()
    {
        IsRootState = true;
    }

    public CameraFreeMovementState(CameraController currentContext, CameraStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
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
        InitializeSubState();
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        Context.MoveCamera(Context.MovementInput);
        CheckSwitchState();
    }
}
