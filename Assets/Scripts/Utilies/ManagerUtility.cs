using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(InputData))]
public class ManagerUtility : MonoBehaviour
{
    private InputData ControllerInputData;
    private bool LeftPrimaryIsPressed;
    private bool RightPrimaryIsPressed;
    public StageScriptableObject StageScriptable;

    public static event Action OnRightPrimaryPressed;
    public static event Action OnLeftPrimaryPressed;

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
        bool isLeftPrimaryPressed;
        bool isRightPrimaryPressed;

        ControllerInputData.RightController.TryGetFeatureValue(CommonUsages.primaryButton, out isRightPrimaryPressed);
        ControllerInputData.LeftController.TryGetFeatureValue(CommonUsages.primaryButton, out isLeftPrimaryPressed);

        if (!RightPrimaryIsPressed && isRightPrimaryPressed)
        {
            OnRightPrimaryPressed?.Invoke();
            RightPrimaryIsPressed = true;
        }
        else if (!isRightPrimaryPressed)
        {
            RightPrimaryIsPressed = false;
        }

        if (!LeftPrimaryIsPressed && isLeftPrimaryPressed)
        {
            OnLeftPrimaryPressed?.Invoke();
            LeftPrimaryIsPressed = true;
        }
        else if (!isRightPrimaryPressed)
        {
            LeftPrimaryIsPressed = false;
        }
    }
}
