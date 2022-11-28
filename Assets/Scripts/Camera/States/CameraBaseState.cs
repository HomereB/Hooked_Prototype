using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBaseState : BaseState
{
    private bool isRootState = false;
    private CameraController context;
    private CameraStateManager manager;
    private CameraBaseState currentSuperState;
    private CameraBaseState currentSubState;

    protected bool IsRootState { get => isRootState; set => isRootState = value; }
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
    public void UpdateStates()
    {
        UpdateState();
        if (currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }

    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();
    protected void SwitchState(CameraBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (isRootState)
            context.CurrentState = newState;
        else if (currentSuperState != null)
            currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(CameraBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }
    protected void SetSubState(CameraBaseState newSubState)
    {
        currentSubState = newSubState;
        currentSubState.SetSuperState(this);
    }
}
