using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, ICharacterMove
{
    private Vector3 input;
    private Vector2 currentVelocity;


    void FixedUpdate()
    {
        ComputeCurrentVelocity();
    }

    void ComputeCurrentVelocity()
    {
        currentVelocity = input;
    }

    public Vector3 GetValue()
    {
        return currentVelocity;
    }

    public void SetInput(Vector3 movementInput)
    {
        input = movementInput;
    }
}
