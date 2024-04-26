using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepFour : MonoBehaviour
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

    public bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return StageScriptable.ActiveStageId == 1 && !step.IsDone && step.StepId == 4;
    }
}