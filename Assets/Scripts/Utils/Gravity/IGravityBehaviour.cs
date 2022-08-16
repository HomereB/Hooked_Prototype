using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravityBehaviour : IMovementComponent
{
    public void ActivateGravity(bool activationStatus);

}
