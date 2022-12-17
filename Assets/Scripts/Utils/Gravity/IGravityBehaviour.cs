using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravityBehaviour : IMovementComponent
{
    void ActivateGravity(PlayerGravityData data);
}
