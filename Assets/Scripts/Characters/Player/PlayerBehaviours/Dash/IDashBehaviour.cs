using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashBehaviour : IMovementComponent
{
    public void Dash();
    public void StopDash();
    public void CancelDash();
}
