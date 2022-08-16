using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private static ComboManager instance;
    public static ComboManager Instance { get => instance; }

    [SerializeField]
    private static int currentComboIndex;
    [SerializeField]
    private static float currentComboMultiplier;
    [SerializeField]
    private static float comboStatMultiplier;

    public static float CurrentComboMultiplier { get => currentComboMultiplier; set => currentComboMultiplier = value; }
    public static int CurrentComboIndex { get => currentComboIndex; set => currentComboIndex = value; }
    public static float ComboStatMultiplier { get => comboStatMultiplier; set => comboStatMultiplier = value; }

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
    }

    public static void IncreaseCombo()
    {

    }

    public static void ResetCombo()
    {

    }

    public static void DecreaseCombo()
    {

    }
}
