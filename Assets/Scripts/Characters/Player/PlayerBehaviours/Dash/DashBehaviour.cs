using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour, IDashBehaviour
{
    [SerializeField]
    private float maxDuration;
    [SerializeField]
    private float minDuration;
    [SerializeField]
    private float initialSpeed;
    [SerializeField]
    private Vector2 initialDirection;
    [SerializeField]
    private float cooldown;

    private DashState dashState = DashState.Ready;
    private float currentDuration;
    private float currentSpeed;
    private Vector2 currentDirection;
    private Vector2 currentDashValue;


    public Vector2 MaxDirection { get => initialDirection; set => initialDirection = value; }
    public float InitialDuration { get => maxDuration; set => maxDuration = value; }
    public float InitialSpeed { get => initialSpeed; set => initialSpeed = value; }
    public float MinDuration { get => minDuration; set => minDuration = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    private bool isDashing;

    void Start()
    {
        
    }

    void FixedUpdate()
    {   
        if (dashState == DashState.Ready && isDashing == true)
        {
            dashState = DashState.Dashing;
            currentDuration = 0;
            currentSpeed = initialSpeed;
        }
        if(dashState == DashState.Dashing)
        {
            //Add movement computation
            currentDuration += Time.fixedDeltaTime;
            if(currentDuration >= maxDuration || (isDashing == false && currentDuration >= minDuration))
            {
                currentDuration = 0;
                dashState = DashState.OnCooldown;
            }
        }
        if (dashState == DashState.OnCooldown)
        {
            currentDuration += Time.fixedDeltaTime;
            if (currentDuration >= cooldown)
            {
                dashState = DashState.Ready;
                currentDuration = 0;
            }
        }
    }

    public void Dash()
    {
        isDashing = true;
    }    
    
    public Vector2 GetValue()
    {
        return currentDashValue;
    }

    public void StopDash()
    {
        isDashing = false;
    }

    public void CancelDash()
    {
        if(dashState == DashState.Dashing)
        {
            dashState = DashState.OnCooldown;
            isDashing = false;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    OnCooldown
}
