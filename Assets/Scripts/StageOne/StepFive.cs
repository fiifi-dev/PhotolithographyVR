using PhotolithographyVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFive : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    private bool IsDone;


    void HandleRaycastHit()
    {
        if (IsDone) return;

        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        if (!CanPerformStep()) return;

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        IsDone = true;
    }

    private bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 5;
    }
}
