using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : EntityHealthManager
{
    private PlayerController playerController;
    private float currentHealthRegainable;

    private Coroutine regainableHealthCoroutine;

    public float RegainableHealthPercentage { get => (currentHealth + currentHealthRegainable) / characterHealthData.maxHealth; }


    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    public void SetupHealthManager(CharacterHealthData healthData, PlayerController controller)
    {
        base.SetupHealthManager(healthData);
        currentHealthRegainable = 0;
        PlayerController = controller;
    }

    public override void AddToCurrentHealth(int value)
    {
        base.AddToCurrentHealth(value);
    }

    public override void Damage(int value)
    {
        base.Damage(value);
        currentHealthRegainable = value;
        if(regainableHealthCoroutine != null)
            StopCoroutine(regainableHealthCoroutine);
        regainableHealthCoroutine = StartCoroutine("RegainableHealthCoroutine");
    }

    private IEnumerator RegainableHealthCoroutine()
    {      
        yield return new WaitForSeconds(playerController.playerHealthData.regainableHealthTimeLossBegin);
        while(currentHealthRegainable > 0)
        {
            if (currentHealthRegainable >= playerController.playerHealthData.regainableHealthLossRate)
                 currentHealthRegainable -= playerController.playerHealthData.regainableHealthLossRate;
            else
                currentHealthRegainable = 0;
            yield return new WaitForSeconds(0.02f);
            Debug.Log("blub");
        }      
    }
}
