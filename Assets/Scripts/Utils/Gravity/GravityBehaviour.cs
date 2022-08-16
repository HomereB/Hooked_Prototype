using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : MonoBehaviour, IGravityBehaviour
{
    private bool hasGravity;
    public Vector2 baseVelocity;
    private Vector2 previousVelocity;
    public Vector2 maximumVelocity;
    [SerializeField]
    private Vector2 currentVelocity;
    public Vector2 gravity;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2(0, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasGravity)
        {
            currentVelocity += gravity * gravityScale * Time.deltaTime;
            if(Mathf.Abs(currentVelocity.x) > Mathf.Abs(maximumVelocity.x))
            {
                currentVelocity.x = maximumVelocity.x;
            }
            if (Mathf.Abs(currentVelocity.y) > Mathf.Abs(maximumVelocity.y))
            {
                currentVelocity.y = maximumVelocity.y;
            }
        }
        else
        {
            ResetGravity();
        }
    }

    public void ActivateGravity(bool activationStatus)
    {
        hasGravity = activationStatus;
    }

    public Vector3 GetValue()
    {
        return currentVelocity;
    }

    private void ResetGravity()
    {
        currentVelocity = baseVelocity;
        previousVelocity = Vector3.zero;
    }
}
