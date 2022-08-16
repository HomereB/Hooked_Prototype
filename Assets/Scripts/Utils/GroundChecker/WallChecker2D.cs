using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker2D : MonoBehaviour
{
    [SerializeField]
    public List<Vector2> offsetsRight;
    [SerializeField]
    public List<Vector2> offsetsLeft;

    [SerializeField]
    private bool isAgainstWallRight;
    [SerializeField]
    private bool isAgainstWallLeft;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float checkDistance;
    public bool IsAgainstWallRight{ get => isAgainstWallRight; set => isAgainstWallRight = value; }
    public bool IsAgainstWallLeft { get => isAgainstWallLeft; set => isAgainstWallLeft = value; }


    private void FixedUpdate()
    {
        isAgainstWallRight = CheckWalls(true);
        isAgainstWallLeft = CheckWalls(false);        
    }

    private bool CheckWalls(bool rightSide)
    {
        Vector2 hitscanDirection = (rightSide) ? Vector2.right: Vector2.left;

        int hits = 0;
        foreach (Vector2 offset in (rightSide) ? offsetsRight : offsetsLeft)
        {
            // Raycast from the feet of the player directly down (or the origin, doesn't matter)
            RaycastHit2D hit2D = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y), hitscanDirection, checkDistance, layerMask);
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y), hitscanDirection * checkDistance,Color.red);
            // If the raycast hit something
            if (hit2D)
            {
                hits++;
            }
        }
        if (hits > 0)
            return true;
        else
            return false;
    }
}
