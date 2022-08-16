using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpBehaviour : IMovementComponent
{
    void Jump();
    void NextJump();
    void StopJump();
    void ResetJump();
    void AddJumpToCount();
    int GetJumpCount();
}
