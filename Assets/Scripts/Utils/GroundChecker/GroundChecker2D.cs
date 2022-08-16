using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    [SerializeField]
    public List<Vector2> offsets;

    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float checkDistance;
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    private void FixedUpdate()
    {
        int hits = 0;
        foreach (Vector2 offset in offsets)
        {
            // Raycast from the feet of the player directly down (or the origin, doesn't matter)
            RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y), Vector2.down, checkDistance, layerMask);
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y), Vector2.down*checkDistance,Color.red);

            // If the raycast hit something
            if (hit2D)
            {
                hits++;
            }
        }
        if (hits > 0)
            isGrounded = true;
        else
            isGrounded = false;
    }
}
