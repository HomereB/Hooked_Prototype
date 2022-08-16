using System;
using System.Collections.Generic;

public enum PlayerStates
{
    Idle,
    Run,
    Grounded,
    Jump,
    Fall,
    Dash,
    Hook,
    Grab,
}

public class PlayerStateManager
{
    PlayerController context;
    Dictionary<PlayerStates, PlayerBaseState> states = new Dictionary<PlayerStates, PlayerBaseState>();
    public PlayerStateManager(PlayerController currentContext)
    {
        context = currentContext;
        states[PlayerStates.Idle] = new PlayerIdleState(context, this);
        states[PlayerStates.Run] = new PlayerRunState(context, this);
        states[PlayerStates.Grounded] = new PlayerGroundedState(context, this);
        states[PlayerStates.Jump] = new PlayerJumpState(context, this);
        states[PlayerStates.Fall] = new PlayerFallState(context, this);
        states[PlayerStates.Dash] = new PlayerDashState(context, this);
        states[PlayerStates.Hook] = new PlayerHookState(context, this);
        states[PlayerStates.Grab] = new PlayerGrabState(context, this);
    }

    public PlayerBaseState Idle()
    {
        return states[PlayerStates.Idle];
    }
    public PlayerBaseState Run() 
    {
        return states[PlayerStates.Run];
    }
    public PlayerBaseState Jump() 
    {
        return states[PlayerStates.Jump];
    }
    public PlayerBaseState Grounded() 
    {
        return states[PlayerStates.Grounded];
    }
    public PlayerBaseState Dash() 
    {
        return states[PlayerStates.Dash];
    }
    public PlayerBaseState Hook() 
    {
        return states[PlayerStates.Hook];
    }
    public PlayerBaseState Fall()
    {
        return states[PlayerStates.Fall];
    }
    public PlayerBaseState Grab()
    {
        return states[PlayerStates.Grab];
    }
}
