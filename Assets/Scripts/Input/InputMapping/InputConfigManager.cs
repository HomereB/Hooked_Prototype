using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputConfigManager : MonoBehaviour
{
    [SerializeField]
    List<RebindingDisplay> rebindingDisplays;

    [SerializeField] private PlayerController playerController = null;

    // Start is called before the first frame update
    void Start()
    {
        LoadInputConfig();
    }

    public void LoadInputConfig()
    {
        string rebinds = PlayerPrefs.GetString("rebinds", string.Empty);

        if (string.IsNullOrEmpty(rebinds)) { return; }

        playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);

        foreach(RebindingDisplay rebindingDisplay in rebindingDisplays)
        {
            rebindingDisplay.SetRebindingDisplayText();
        }
    }

    public void SaveInputConfig()
    {
        string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
}
