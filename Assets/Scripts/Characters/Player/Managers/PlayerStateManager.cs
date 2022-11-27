using System;
using System.Collections.Generic;

public class PlayerStateManager
{
    PlayerController context;
    Dictionary<string, PlayerBaseState> states = new Dictionary<string, PlayerBaseState>();

    public PlayerStateManager(PlayerController currentContext)
    {
        context = currentContext;
    }

    public T GetState<T>() where T : PlayerBaseState, new()
    {
        string className = typeof(T).Name;
        bool alreadyCreated = states.ContainsKey(className);
        T resultState = alreadyCreated ? states[className] as T : new T();
        if (!alreadyCreated)
        {
            resultState.SetupState(context, this);
        }
        return resultState;
    }
}
