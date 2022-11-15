using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour, IJumpBehaviour
{
    [SerializeField]
    private float initialJumpVelocity;
    [SerializeField]
    private float minJumpVelocity;
    [SerializeField]
    private float currentJumpVelocity;
    [SerializeField]
    private float speedDecreaseRate;
    [SerializeField]
    private int maxJumpAmount;
    [SerializeField]
    private int currentJumpAmount;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private float maxJumpTime;
    [SerializeField]
    private float currentJumpTime;
    [SerializeField]
    private bool canStartJump;

    private Vector3 JumpValue;

    void FixedUpdate()
    {
        if (isJumping && currentJumpTime < maxJumpTime)
        {
            JumpValue = (new Vector3( 0 ,currentJumpVelocity, 0 ));
            currentJumpTime += Time.deltaTime;
            currentJumpVelocity -= speedDecreaseRate;  
            if(currentJumpVelocity < minJumpVelocity)
            {
                currentJumpVelocity = minJumpVelocity;
            } 
        }
        else
        {
            JumpValue = Vector3.zero; 
        }
    }

    public void Jump()
    {
        if( currentJumpTime < maxJumpTime )
        {
            if (currentJumpAmount < maxJumpAmount && isJumping == false && canStartJump)
            {
                currentJumpAmount++;
                isJumping = true;
                canStartJump = false;
                currentJumpVelocity = initialJumpVelocity;             
            }      
        }
        else
        {
            StopJump();
        }
    }

    public void StopJump()
    {
        if (currentJumpAmount > maxJumpAmount)
            currentJumpAmount = maxJumpAmount;
        currentJumpTime = 0;
        currentJumpVelocity = 0;
        isJumping = false;
    }

    public void NextJump()
    {
        isJumping = false;
        currentJumpTime = 0;
        canStartJump = true;
    }

    public void ResetJump()
    {
        currentJumpTime = 0;
        currentJumpAmount = 0;
        currentJumpVelocity = 0;
        isJumping = false;
        canStartJump = true;
    }

    public void AddJumpToCount()
    {
        currentJumpAmount++;
    }

    public int GetJumpCount()
    {
        return currentJumpAmount;
    }

    public Vector2 GetValue()
    {
        return JumpValue;
    }
}
