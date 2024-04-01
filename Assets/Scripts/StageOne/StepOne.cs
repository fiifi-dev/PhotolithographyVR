using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepOne : MonoBehaviour
{
    public StageScriptableObject Stage;
    private bool IsDone;



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
        var stage = Stage.GetCurrentStage();

        return !IsDone && stage.ActiveStepId == 1;
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {

        if (!CanPerformStep()) return;

        var stage = Stage.GetCurrentStage();
        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        IsDone = true;
    }
}
