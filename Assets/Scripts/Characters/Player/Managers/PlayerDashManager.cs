using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashManager : MonoBehaviour
{
    private PlayerDashData playerDashData;
    private PlayerController playerController;

    [SerializeField]
    private bool needNewDashPressed;
    [SerializeField]
    private float currentCooldownTimer;
    [SerializeField]
    private int currentDashCharges;

    private float currentDashTime;
    private Vector2 dashDirection;

    public bool CanDash { get => (currentDashCharges > 0 && !needNewDashPressed) ? true : false; }
    public bool NeedNewDashPressed { get => needNewDashPressed; set => needNewDashPressed = value; }
    public int CurrentDashCharges { get => currentDashCharges; set => currentDashCharges = value; }
    public float CurrentDashTime { get => currentDashTime; set => currentDashTime = value; }
    public Vector2 DashDirection { get => dashDirection; set => dashDirection = value; }
    public float CurrentCooldownTimer { get => currentCooldownTimer; set => currentCooldownTimer = value; }
    public float DashPercentage { get =>  CurrentDashCharges + CurrentCooldownTimer / playerDashData.dashCooldown; }

    private void Start()
    {
        needNewDashPressed = false;
        currentCooldownTimer = 0;
    }

    public void PlayerDashManagerSetup(PlayerDashData dashData, PlayerController controller)
    {
        playerController = controller;
        playerDashData = dashData;
        currentDashCharges = playerDashData.maxDashCharges;
    }


    // Update is called once per frame
    void Update()
    {
        if(currentDashCharges < playerDashData.maxDashCharges)
        {
            currentCooldownTimer += Time.deltaTime;
            if(currentCooldownTimer > playerDashData.dashCooldown)
            {
                currentCooldownTimer = 0;
                currentDashCharges++;
            }
        }
    }
}
