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
        currentVelocity = gravityData != null ? gravityData.baseVelocity : Vector2.zero;
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
        {
            if (currentVelocity.x < data.baseVelocity.x)
                currentVelocity.x = data.baseVelocity.x;
            
            if (currentVelocity.y < data.baseVelocity.y)
                currentVelocity.y = data.baseVelocity.y;
        }
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
