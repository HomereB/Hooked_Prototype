using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialManager : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerSpecialData playerSpecialData;
    private float currentSpecialCharge;

    public float SpecialPercentage { get => currentSpecialCharge / playerSpecialData.ChargeValue; }
    public void PlayerSpecialManagerSetup(PlayerSpecialData specialData, PlayerController controller)
    {
        playerController = controller;
        playerSpecialData = specialData;
        currentSpecialCharge = playerSpecialData.initialSpecialValue;
    }

    public bool CanConsumeSpecial { get => currentSpecialCharge > playerSpecialData.ChargeValue; }

    public void SpecialCharge(float value)
    {
        currentSpecialCharge = Mathf.Clamp(currentSpecialCharge, 0f, playerSpecialData.MaxSpecialValue);
    }

    public void ConsumeSpecialCharge()
    {
        currentSpecialCharge -= playerSpecialData.ChargeValue;        
    }
}
