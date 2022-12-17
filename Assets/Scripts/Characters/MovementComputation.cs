using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComputation : MonoBehaviour
{
    List<IMovementComponent> movementComponents;
    // Start is called before the first frame update
    Vector2 movement;
    Rigidbody2D rb;

    private void Awake()
    {
        movementComponents = new List<IMovementComponent>();
    }

    public void SetRigidBody(Rigidbody2D rigidbody)
    {
        rb = rigidbody;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void AddMovementComponent(IMovementComponent movementComponent)
    {
        movementComponents.Add(movementComponent);
    }

    public void RemoveMovementComponent(IMovementComponent movementComponent)
    {
        movementComponents.Add(movementComponent);
    }

    private void Move()
    {
        movement = Vector3.zero;
        foreach (IMovementComponent movementComponent in movementComponents)
        {
            movement += movementComponent.GetValue() * Time.deltaTime;
        }
        Debug.Log("Movement : " + movement);
        rb.velocity = new Vector2(movement.x,movement.y);
    }
}
