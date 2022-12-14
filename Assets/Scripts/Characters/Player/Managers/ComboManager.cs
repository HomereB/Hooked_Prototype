using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private static ComboManager instance;
    public static ComboManager Instance { get => instance; }

    [SerializeField]
    private static int currentComboCount;
    [SerializeField]
    private static float currentComboStatMultiplier;
    [SerializeField]
    private static float comboStatMultiplier;
    [SerializeField]
    private static bool listeningForAttackInput;
    private static float maxStatMultiplier;
    private static float timeBeforeReset;

    //Swap for state? input register in controller and bool to check if registering
    private static AttackType lastRegisteredAttackInput;

    public static float CurrentComboStatMultiplier { get => currentComboStatMultiplier; set => currentComboStatMultiplier = value; }
    public static int CurrentComboCount { get => currentComboCount; set => currentComboCount = value; }
    public static float ComboStatMultiplier { get => comboStatMultiplier; set => comboStatMultiplier = value; }
    public static bool ListeningForAttackInput { get => listeningForAttackInput; set => listeningForAttackInput = value; }
    public static AttackType LastRegisteredAttackInput { get => lastRegisteredAttackInput; set => lastRegisteredAttackInput = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        currentComboCount = 0;
        currentComboStatMultiplier = 0;
        timeBeforeReset = 0;
    }

    public static void IncreaseCombo(float timeIncrease)
    {
        currentComboCount++;
        currentComboStatMultiplier = currentComboStatMultiplier + (comboStatMultiplier * currentComboCount);
        currentComboStatMultiplier = Mathf.Min(currentComboStatMultiplier , maxStatMultiplier);
        timeBeforeReset += timeIncrease;
    }

    public static void ResetCombo()
    {
        currentComboCount = 0;
        currentComboStatMultiplier = 0f;
        timeBeforeReset = 0f;
    }

    private void Update()
    {
        timeBeforeReset -= Time.deltaTime;
        if(timeBeforeReset <= 0f)
        {
            ResetCombo();
        }

    }

    private void RegisterAttackInput(AttackType attackType)
    {
        if(listeningForAttackInput)
        {
            lastRegisteredAttackInput = attackType;
        }
    }

    public static void DecreaseCombo()
    {

    }
}
