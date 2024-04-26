using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public StageScriptableObject StageScriptable;

    public virtual void HandleStep()
    {
        if (!CanPerformStep()) return;

        // Runs if wafer is dropped in petri 
        var step = StageScriptable.GetCurrentStep();
        step.IsDone = true;
        StageScriptable.SetNextStep();
    }
    private  bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return StageScriptable.ActiveStageId == 1 && !step.IsDone && step.StepId == 2;
    }
}
