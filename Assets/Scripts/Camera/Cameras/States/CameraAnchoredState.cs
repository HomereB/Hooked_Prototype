using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchoredState : CameraBaseState
{
    public CameraAnchoredState() : base()
    {
    }

    public CameraAnchoredState(CameraController currentContext, CameraStateManager currentManager) : base(currentContext, currentManager)
    {
    }
    public override void CheckSwitchState()
    {
        if(Context.IsTraveling)
        {
            SwitchState(Manager.GetState<CameraCutsceneState>());
        }
        if (Context.CurrentAnchor == null)
        {
            SwitchState(Manager.GetState<CameraFreeMovementState>());
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
        Context.FollowAnchor();
        Context.CurrentAnchor.ComputeAnchorMovement();
        CheckSwitchState();
    }
}
