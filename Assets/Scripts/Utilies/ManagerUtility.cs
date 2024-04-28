using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(InputData))]
public class ManagerUtility : MonoBehaviour
{
    private InputData ControllerInputData;
    private bool LeftPrimaryIsPressed;
    private bool RightPrimaryIsPressed;
    private bool LeftSecondaryIsPressed;
    private bool RightSecondaryIsPressed;

    public StageScriptableObject StageScriptable;

    public static event Action OnRightPrimaryPressed;
    public static event Action OnLeftPrimaryPressed;
    public static event Action OnRightSecondaryPressed;
    public static event Action OnLeftSecondaryPressed;

    private void Start()
    {
        ControllerInputData = GetComponent<InputData>();
    }

    void CleanUpResources()
    {
        StageScriptable.ResetStages();
    }

    private void OnApplicationQuit()
    {
        CleanUpResources();
    }

    private void Update()
    {
        bool isLeftPrimaryPressed = GetButtonState(ControllerInputData.LeftController, CommonUsages.primaryButton);
        bool isRightPrimaryPressed = GetButtonState(ControllerInputData.RightController, CommonUsages.primaryButton);
        bool isLeftSecondaryPressed = GetButtonState(ControllerInputData.LeftController, CommonUsages.secondaryButton);
        bool isRightSecondaryPressed = GetButtonState(ControllerInputData.RightController, CommonUsages.secondaryButton);

        CheckAndInvoke(ref RightPrimaryIsPressed, isRightPrimaryPressed, OnRightPrimaryPressed);
        CheckAndInvoke(ref LeftPrimaryIsPressed, isLeftPrimaryPressed, OnLeftPrimaryPressed);
        CheckAndInvoke(ref RightSecondaryIsPressed, isRightSecondaryPressed, OnRightSecondaryPressed);
        CheckAndInvoke(ref LeftSecondaryIsPressed, isLeftSecondaryPressed, OnLeftSecondaryPressed);
    }

    private bool GetButtonState(InputDevice device, InputFeatureUsage<bool> usage)
    {
        return device.TryGetFeatureValue(usage, out var value) && value;
    }

    private void CheckAndInvoke(ref bool previousState, bool currentState, Action action)
    {
        if (!previousState && currentState)
        {
            action?.Invoke();
            previousState = true;
        }
        else if (previousState && !currentState)
        {
            previousState = false;
        }
    }
}
