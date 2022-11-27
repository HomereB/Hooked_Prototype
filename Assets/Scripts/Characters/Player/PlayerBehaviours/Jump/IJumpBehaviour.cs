using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpBehaviour : IMovementComponent
{
    bool IsJumpFinished { get; }
    float ScaleX { get; set; }

    void ActivateJump(PlayerJumpData data);
}
