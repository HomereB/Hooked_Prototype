using UnityEngine;

public abstract class PlayerBaseState : BaseState
{
    private bool isRootState = false;
    private PlayerController context;
    private PlayerStateManager manager;
    private PlayerBaseState currentSuperState;
    private PlayerBaseState currentSubState;

    protected bool IsRootState { get => isRootState; set => isRootState = value; }
    protected PlayerController Context { get => context; set => context = value; }
    protected PlayerStateManager Manager { get => manager; set => manager = value; }

    public PlayerBaseState() {}

    public PlayerBaseState(PlayerController currentContext,PlayerStateManager currentManager)
    {
        context = currentContext;
        manager = currentManager;
    }

    public void SetupState(PlayerController ctx, PlayerStateManager mngr)
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
    protected void SwitchState(PlayerBaseState newState) 
    {
        ExitState();

        newState.EnterState();

        if (isRootState)
            context.CurrentState = newState;
        else if (currentSuperState != null)
            currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) 
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
    
    /*    public void ExitStates()
    {
        ExitState();
        if (_currentSubState != null)
        {
            _currentSubState.ExitStates();
        }
    }*/
}
