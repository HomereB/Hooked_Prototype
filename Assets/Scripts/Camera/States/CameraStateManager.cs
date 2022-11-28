using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateManager
{
    CameraController context;
    Dictionary<string, CameraBaseState> states = new Dictionary<string, CameraBaseState>();

    public CameraStateManager(CameraController currentContext)
    {
        context = currentContext;
    }

    public T GetState<T>() where T : CameraBaseState, new()
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
