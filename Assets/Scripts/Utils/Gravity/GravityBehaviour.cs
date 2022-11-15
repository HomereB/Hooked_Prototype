using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : MonoBehaviour, IGravityBehaviour
{
    private bool HasGravity
    {
        get => gravityData != null;
    }

    private Vector2 currentVelocity;

    [SerializeField]
    private PlayerGravityData gravityData;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = gravityData != null ? gravityData.baseVelocity : new Vector2(0, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasGravity)
        {
            currentVelocity += gravityData.gravityScale * Time.fixedDeltaTime * gravityData.gravity;

            if (Mathf.Abs(currentVelocity.x) > Mathf.Abs(gravityData.maximumVelocity.x))
            {
                currentVelocity.x = gravityData.maximumVelocity.x;
            }
            if (Mathf.Abs(currentVelocity.y) > Mathf.Abs(gravityData.maximumVelocity.y))
            {
                currentVelocity.y = gravityData.maximumVelocity.y;
            }
        }
        else
        {
            ResetGravity();
        }
    }

    public void ActivateGravity(PlayerGravityData data)
    {
        gravityData = data;

        if (data != null)
            currentVelocity = data.baseVelocity;
    }

    public Vector2 GetValue()
    {
        return currentVelocity;
    }

    private void ResetGravity()
    {
        currentVelocity = Vector2.zero;
    }
}
