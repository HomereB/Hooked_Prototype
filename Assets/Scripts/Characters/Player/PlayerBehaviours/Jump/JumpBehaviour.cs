using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour, IJumpBehaviour
{
    private float currentJumpTime = 0.0f;

    private bool IsJumping
    {
        get => jumpData != null;
    }

    public bool IsJumpFinished
    {
        get => currentJumpTime >= jumpData.maxJumpTime;
    }

    private Vector2 currentVelocity;

    public float ScaleX { get; set; }

    [SerializeField]
    private PlayerJumpData jumpData;

    void Start()
    {
        currentVelocity = jumpData != null ? jumpData.initialJumpVelocity : Vector2.zero;
    }

    void FixedUpdate()
    {
        if (IsJumping)
        {
            currentJumpTime += Time.fixedDeltaTime;
            currentVelocity -= jumpData.speedDecreaseRate;

            if (currentVelocity.x < jumpData.minJumpVelocity.x)
            {
                currentVelocity.x = jumpData.minJumpVelocity.x;
            }
            
            if (currentVelocity.y < jumpData.minJumpVelocity.y)
            {
                currentVelocity.y = jumpData.minJumpVelocity.y;
            }
        }
        else
        {
            ResetJump();
        }
    }

    public void ResetJump()
    {
        currentJumpTime = 0;
        currentVelocity = Vector2.zero;
        ScaleX = 1f;
    }

    public Vector2 GetValue()
    {
        Vector2 scaledVelocity = new Vector2(currentVelocity.x * ScaleX, currentVelocity.y);

        return scaledVelocity;
    }

    public void ActivateJump(PlayerJumpData data)
    {
        jumpData = data;

        if (data != null)
        {
            //TODO : Add check to compare with previous velocity and act in consideration
            currentVelocity = data.initialJumpVelocity;
        }
    }
}
