using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneState : CameraBaseState
{
    public CameraCutsceneState() : base()
    {
        IsRootState = true;
    }

    public CameraCutsceneState(CameraController currentContext, CameraStateManager currentManager) : base(currentContext, currentManager)
    {
        IsRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (!Context.IsTraveling)
        {
            if (Context.CurrentAnchor != null)
            {
                SwitchState(Manager.GetState<CameraAnchoredState>());
            }
            else
            {
                SwitchState(Manager.GetState<CameraFreeMovementState>());
            }
        }
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {
        Context.IsTraveling = false;
    }

    public override void InitializeSubState()
    {
        SetSubState(null);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
}
