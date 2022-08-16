using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindingDisplay : MonoBehaviour
{
    [SerializeField] private InputActionReference playerAction = null;
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private Text bindingDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void StartRebinding()
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        playerController.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = playerAction.action.PerformInteractiveRebinding()
            /*.WithControlsExcluding("Mouse")*/
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        SetRebindingDisplayText();
        rebindingOperation.Dispose();
        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
        playerController.PlayerInput.SwitchCurrentActionMap("Player");

    }

    internal void SetRebindingDisplayText()
    {
        int bindingIndex = playerAction.action.GetBindingIndexForControl(playerAction.action.controls[0]);
        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            playerAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
