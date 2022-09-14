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

    //Create UI template : SimpleBar / MultipleBar / Circle / Other

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
        //healthBar.Fill(playerController.playerHealthManager.getHealthPercentage);
        dashIndicators.FillGauge(playerController.DashManager.DashPercentage);
        hookIndicator.FillGauge(playerController.HookManager.HookPercentage);
        specialBars.FillGauge(playerController.SpecialManager.SpecialPercentage);
    }
}
