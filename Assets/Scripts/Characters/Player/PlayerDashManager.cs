using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashManager : MonoBehaviour
{
    private PlayerDashData playerDashData;
    private PlayerController playerControler;

    [SerializeField]
    private bool needNewDashPressed;
    [SerializeField]
    private float currentCooldownTimer;
    [SerializeField]
    private int currentDashCharges;
    public bool CanDash { get => (currentDashCharges > 0 && !needNewDashPressed) ? true : false; }
    public bool NeedNewDashPressed { get => needNewDashPressed; set => needNewDashPressed = value; }
    public int CurrentDashCharges { get => currentDashCharges; set => currentDashCharges = value; }


    public void PlayerDashManagerSetup(PlayerDashData dashData, PlayerController controler)
    {
        playerControler = controler;
        playerDashData = dashData;
        needNewDashPressed = false;
        currentCooldownTimer = 0;
        currentDashCharges = playerDashData.maxDashCharges;
    }


    // Update is called once per frame
    void Update()
    {
        if(currentDashCharges < playerDashData.maxDashCharges)
        {
            currentCooldownTimer += Time.deltaTime;
            if(currentCooldownTimer>playerDashData.dashCooldown)
            {
                currentCooldownTimer = 0;
                currentDashCharges++;
            }
        }
    }
}
