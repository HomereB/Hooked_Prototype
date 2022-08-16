using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMove : IMovementComponent 
{
    public void SetInput(Vector3 movementInput);
}
