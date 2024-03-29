using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepOne : MonoBehaviour
{
    public BuildStage BuildStage;

    private void OnEnable()
    {
        PickedDropped.OnPicked += HandleSelectEntered;
    }
    private void OnDisable()
    {
        PickedDropped.OnPicked -= HandleSelectEntered;
    }

    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return stage.ActiveStepId == 1 && gameObject.tag == "Wafer";
    }

    private void HandleSelectEntered()
    {

        if (!CanPerformStep()) return;

        var stage = BuildStage.GetCurrentStage();
        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
    }
}
