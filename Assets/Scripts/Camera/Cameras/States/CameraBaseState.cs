using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBaseState : BaseState
{
    private CameraController context;
    private CameraStateManager manager;


    protected CameraController Context { get => context; set => context = value; }
    protected CameraStateManager Manager { get => manager; set => manager = value; }

    public CameraBaseState() { }

    public CameraBaseState(CameraController currentContext, CameraStateManager currentManager)
    {
        context = currentContext;
        manager = currentManager;
    }

    public void SetupState(CameraController ctx, CameraStateManager mngr)
    {
        context = ctx;
        manager = mngr;
    }

    public abstract override void EnterState();

    public abstract override void ExitState();

    public abstract override void UpdateState();

    public abstract void CheckSwitchState();

    protected void SwitchState(CameraBaseState newState)
    {
        ExitState();
        newState.EnterState();
        context.CurrentState = newState;
    }
}
