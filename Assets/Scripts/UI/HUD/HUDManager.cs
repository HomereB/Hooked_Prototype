using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private HUDGauge healthBar;
    [SerializeField]
    private HUDGauge dashIndicators;
    [SerializeField]
    private HUDGauge specialBars;
    [SerializeField]
    private HUDGauge hookIndicator;

    private PlayerController playerController;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        healthBar.FillGauge(playerController.HealthManager.HealthPercentage,true,true);
        dashIndicators.FillGauge(playerController.DashManager.DashPercentage,true,true);
        hookIndicator.FillGauge(playerController.HookManager.HookPercentage,true,true);
        specialBars.FillGauge(playerController.SpecialManager.SpecialPercentage,true,true);
    }
}
